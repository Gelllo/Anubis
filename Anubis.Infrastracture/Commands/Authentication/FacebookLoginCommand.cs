using Anubis.Application.Requests.Login;
using Anubis.Application.Responses.Login;
using Anubis.Application;
using Anubis.Application.Requests.ExternalLogin;
using Anubis.Domain;
using Anubis.Infrastracture.Services;
using AutoMapper;
using Google.Apis.Auth;
using Microsoft.Extensions.Options;
using Anubis.Domain.UsersDomain;
using Anubis.Web.Shared;
using Newtonsoft.Json;

public class FacebookLoginCommand : ICommandHandler<FacebookLoginRequest, LoginResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly AppSettings _appSettings;
    private readonly HttpClient _httpClient;

    public FacebookLoginCommand(IUnitOfWork uniOfWork, IMapper mapper, IOptions<AppSettings> appSettings, HttpClient httpClient)
    {
        _unitOfWork = uniOfWork;
        _mapper = mapper;
        _appSettings = appSettings.Value;
        _httpClient = httpClient;
    }

    public async Task<LoginResponse> Handle(FacebookLoginRequest req, CancellationToken cancellation)
    {
        HttpResponseMessage response = await _httpClient.GetAsync(
            "https://graph.facebook.com/debug_token?input_token=" + req.Credentials +
            $"&access_token={this._appSettings.FacebookAppId}|{this._appSettings.FacebookAppSecret}");
        var stringResponse = await response.Content.ReadAsStringAsync();
        var userObj = JsonConvert.DeserializeObject<FBUser>(stringResponse);

        HttpResponseMessage meResponse = await _httpClient.GetAsync(
            "https://graph.facebook.com/me?fields=first_name,last_name,email,id&access_token=" + req.Credentials);
        var stringMeResponse = await meResponse.Content.ReadAsStringAsync();
        var userMeObj = JsonConvert.DeserializeObject<FBUserInfo>(stringMeResponse);

        if (!userObj.Data.IsValid)
        {
            throw new Exception(ErrorMessages.INVALID_CREDENTIALS);
        }

        var user = await _unitOfWork.UserRepository.GetUserByEmail(userMeObj.Email);

        if (user == null && !req.UserIsRegistered)
        {
            user = new User()
            {
                UserID = userObj.Data.UserId,
                Email = userMeObj.Email,
                FirstName = userMeObj.FirstName,
                LastName = userMeObj.LastName,
                Role = UserRoles.USER
            };

            await _unitOfWork.UserRepository.InsertUserAsync(user);

            return new LoginResponse()
            {
                UserID = user.UserID,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role
            };
        }
        else if (user == null && req.UserIsRegistered)
        {
            throw new Exception(ErrorMessages.INVALID_FACEBOOK);
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