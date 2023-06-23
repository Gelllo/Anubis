using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anubis.Application;
using Anubis.Application.Requests.Register;
using Anubis.Application.Responses.Register;
using Anubis.Domain;
using Anubis.Domain.UsersDomain;
using Anubis.Infrastracture.Services;
using AutoMapper;
using Microsoft.Extensions.Options;


namespace Anubis.Infrastracture.Commands.Authentication
{
    public class RegisterCommand : ICommandHandler<RegisterRequest, RegisterResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RegisterCommand(IUnitOfWork uniOfWork, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _unitOfWork = uniOfWork;
            _mapper = mapper;
        }

        public async Task<RegisterResponse> Handle(RegisterRequest command, CancellationToken cancellation)
        {
            var user = _mapper.Map<User>(command);
            
            user = user.EncryptPasswordForUser(command.Password);

            await _unitOfWork.UserRepository.InsertUserAsync(user);

            var response = new RegisterResponse { user = user };

            return await Task.FromResult(response);
        }
    }
}
