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
using Anubis.Web.Shared;
using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http;

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
                throw new Exception(ErrorMessages.INVALID_CREDENTIALS);
            }

            var match = EncryptionService.CheckPasswordForUser(user, command.Password);

            if (!match)
            {
                throw new Exception(ErrorMessages.INVALID_CREDENTIALS);
            }

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
