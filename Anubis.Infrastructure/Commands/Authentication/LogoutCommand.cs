using Anubis.Application.Requests.Login;
using Anubis.Application.Responses.Login;
using Anubis.Application;
using Anubis.Application.Requests.ExternalLogin;
using Anubis.Application.Requests.Logout;
using Anubis.Application.Responses.Logout;
using Anubis.Domain;
using Anubis.Infrastructure.Services;
using AutoMapper;
using Google.Apis.Auth;
using Microsoft.Extensions.Options;
using Anubis.Domain.UsersDomain;
using Anubis.Infrastructure;
using Anubis.Web.Shared;
using Newtonsoft.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

public class LogoutCommand : ICommandHandler<LogoutRequest, LogoutResponse>
{
    private readonly IAnubisUnitOfWork<AnubisContext> _unitOfWork;
    private readonly IMapper _mapper;
    private readonly AppSettings _appSettings;
    private readonly HttpClient _httpClient;

    public LogoutCommand(IAnubisUnitOfWork<AnubisContext> uniOfWork, IMapper mapper, IOptions<AppSettings> appSettings, HttpClient httpClient)
    {
        _unitOfWork = uniOfWork;
        _mapper = mapper;
        _appSettings = appSettings.Value;
        _httpClient = httpClient;
    }

    public async Task<LogoutResponse> Handle(LogoutRequest req, CancellationToken cancellation)
    {
        var user = await _unitOfWork.UserRepository.GetUserByUserId(req.UserID);

        if (user == null)
        {
            throw new Exception(ErrorMessages.INVALID_USER);
        }

        var refreshToken = user.RefreshToken;

        if (refreshToken == null)
        {
            throw new Exception(ErrorMessages.NO_REFRESH_TOKEN);
        }

        _unitOfWork.UserRefreshTokenRepository.DeleteRefreshTokenByUserId(user.UserID);

        return new LogoutResponse();
    }
}