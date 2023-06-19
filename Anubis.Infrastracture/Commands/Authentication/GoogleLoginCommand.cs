using Anubis.Application.Requests.Login;
using Anubis.Application.Responses.Login;
using Anubis.Application;
using Anubis.Application.Requests.ExternalLogin;
using Anubis.Domain;
using Anubis.Infrastracture.Services;
using AutoMapper;
using Google.Apis.Auth;
using Microsoft.Extensions.Options;

public class GoogleLoginCommand : ICommandHandler<GoogleLoginRequest, LoginResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly AppSettings _appSettings;

    public GoogleLoginCommand(IUnitOfWork uniOfWork, IMapper mapper, IOptions<AppSettings> appSettings)
    {
        _unitOfWork = uniOfWork;
        _mapper = mapper;
        _appSettings = appSettings.Value;
    }

    public async Task<LoginResponse> Handle(GoogleLoginRequest command, CancellationToken cancellation)
    {
        var settings = new GoogleJsonWebSignature.ValidationSettings()
        {
            Audience = new List<string> { this._appSettings.GoogleCloudId }
        };

        var payload = await GoogleJsonWebSignature.ValidateAsync(command.Credentials, settings);

        var user = await _unitOfWork.UserRepository.GetUserByUserId(payload.Name);

        if (user == null)
        {
            throw new Exception("User does not exit in the database for the login");
        }

        return EncryptionService.GenerateJwtForUser(user, this._appSettings.Secret);
    }
}