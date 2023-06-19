using Anubis.Application.Requests.Users;
using Anubis.Application.Responses.Users;
using Anubis.Application;
using Anubis.Domain;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Anubis.Application.Requests.Login;
using Anubis.Application.Responses.Login;
using Anubis.Infrastracture.Services;
using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Anubis.Infrastracture.Commands.Authentication
{
    public class LoginCommand : ICommandHandler<LoginRequest, LoginResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;

        public LoginCommand(IUnitOfWork uniOfWork, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _unitOfWork = uniOfWork;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        public async Task<LoginResponse> Handle(LoginRequest command, CancellationToken cancellation)
        {
            var user = await _unitOfWork.UserRepository.GetUserByUserId(command.UserName);

            if (user == null)
            {
                throw new Exception("User does not exit in the database for the login");
            }

            var match = EncryptionService.CheckPasswordForUser(user, command.Password);

            if (!match)
            {
                throw new Exception("Username or password was invalid");
            }

            return EncryptionService.GenerateJwtForUser(user, this._appSettings.Secret);
        }
    }
}
