using Anubis.Application.Requests.ForgotPassword;
using Anubis.Application.Responses.ForgotPassword;
using Anubis.Application.Services;
using Anubis.Application;
using Anubis.Infrastructure;
using Anubis.Web.Shared;
using FastEndpoints;

namespace Anubis.Web.Endpoints.Authentication
{
    public class ForgotPassword : Endpoint<ForgotPasswordRequest, ForgotPasswordResponse>
    {
        private ICommandDispatcher _dispatcher;
        private ILogger _logger;
        private IWebSecurityService _webSecurityService;
        private IAnubisUnitOfWork<AnubisContext> _unitOfWork;
        public ForgotPassword(ICommandDispatcher dispatcher, ILogger logger, IWebSecurityService securityService, IAnubisUnitOfWork<AnubisContext> unitOfWork)
        {
            _dispatcher = dispatcher;
            _logger = logger;
            _webSecurityService = securityService;
            _unitOfWork = unitOfWork;
        }

        public override void Configure()
        {
            Post("authentication/forgotpassword");
            AllowAnonymous();
        }

        public override async Task HandleAsync(ForgotPasswordRequest req, CancellationToken ct)
        {
            try
            {
                var user = await _unitOfWork.UserRepository.GetUserByEmail(req.Email);

                if (user == null)
                {
                    ThrowError(ErrorMessages.INVALID_CREDENTIALS);
                }

                user.PasswordResetToken = Guid.NewGuid().ToString();
                user.ResetTokenExpires = DateTime.Now.AddDays(1);

                await _unitOfWork.UserRepository.UpdateUserAsync(user);

                SendAsync(new ForgotPasswordResponse() {Token = user.PasswordResetToken, Email = user.Email});
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
