using Anubis.Application.Requests.Login;
using Anubis.Application.Responses.Login;
using Anubis.Application;
using Anubis.Application.Requests.ExternalLogin;
using Anubis.Domain;
using Anubis.Infrastructure.Services;
using AutoMapper;
using Google.Apis.Auth;
using Microsoft.Extensions.Options;
using Anubis.Domain.UsersDomain;
using Anubis.Infrastructure;
using Anubis.Web.Shared;
using Microsoft.AspNetCore.Mvc.Formatters;

public class GoogleLoginCommand : ICommandHandler<GoogleLoginRequest, LoginResponse>
{
    private readonly IAnubisUnitOfWork<AnubisContext> _unitOfWork;
    private readonly IMapper _mapper;
    private readonly AppSettings _appSettings;

    public GoogleLoginCommand(IAnubisUnitOfWork<AnubisContext> uniOfWork, IMapper mapper, IOptions<AppSettings> appSettings)
    {
        _unitOfWork = uniOfWork;
        _mapper = mapper;
        _appSettings = appSettings.Value;
    }

    public async Task<LoginResponse> Handle(GoogleLoginRequest req, CancellationToken cancellation)
    {
        var settings = new GoogleJsonWebSignature.ValidationSettings()
        {
            Audience = new List<string> { this._appSettings.GoogleCloudId }
        };

        var payload = await GoogleJsonWebSignature.ValidateAsync(req.Credentials, settings);

        var user = await _unitOfWork.UserRepository.GetUserByUserId(payload.Subject);

        if (user == null && req.UserIsRegistered)
        {
            throw new Exception(ErrorMessages.INVALID_GOOGLE);
        }
        else if (user == null && !req.UserIsRegistered)
        {
            user = new User()
            {
                UserID = payload.Subject,
                Email = payload.Email,
                FirstName = payload.FamilyName,
                LastName = payload.GivenName,
                Role = UserRoles.USER
            };

            await _unitOfWork.UserRepository.InsertUserAsync(user);

            return new LoginResponse()
            {
                UserID = user.UserID,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role,
                Email = user.Email
            };
        }
        else
        {
            return new LoginResponse()
            {
                UserID = user.UserID,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role,
                Email = user.Email
            };
        }
    }
}