
using Anubis.Application.Services;
using Anubis.Application;
using Anubis.Application.Requests.ForgotPassword;
using Anubis.Application.Responses.ForgotPassword;
using Anubis.Infrastructure;
using Anubis.Infrastructure.Services;
using Anubis.Web.Shared;
using FastEndpoints;

namespace Anubis.Web.Endpoints.Authentication
{
    public class ResetPassword : Endpoint<ResetPasswordRequest, ResetPasswordResponse>
    {
        private ICommandDispatcher _dispatcher;
        private ILogger _logger;
        private IWebSecurityService _webSecurityService;
        private IAnubisUnitOfWork<AnubisContext> _unitOfWork;
        public ResetPassword(ICommandDispatcher dispatcher, ILogger logger, IWebSecurityService securityService, IAnubisUnitOfWork<AnubisContext> unitOfWork)
        {
            _dispatcher = dispatcher;
            _logger = logger;
            _webSecurityService = securityService;
            _unitOfWork = unitOfWork;
        }

        public override void Configure()
        {
            Post("authentication/resetpassword");
            AllowAnonymous();
        }

        public override async Task HandleAsync(ResetPasswordRequest req, CancellationToken ct)
        {
            try
            {
                var user = await _unitOfWork.UserRepository.GetUserByResetToken(req.Token);

                if (user == null || user.ResetTokenExpires < DateTime.Now)
                {
                    ThrowError(ErrorMessages.EXPIRED_TOKEN);
                }

                if (req.Password != req.ConfirmPassword)
                {
                    ThrowError(ErrorMessages.INVALID_CREDENTIALS);
                }

                user.EncryptPasswordForUser(req.Password);
                user.ResetTokenExpires = null;
                user.PasswordResetToken = null;

                await _unitOfWork.UserRepository.UpdateUserAsync(user);

            }
            catch (Exception ex)
            {
                if (ex.Message.Equals(ErrorMessages.INVALID_CREDENTIALS))
                {
                    ThrowError(ErrorMessages.INVALID_CREDENTIALS);
                }

                _logger.Error(ex.Message);
            }
        }
    }
}
