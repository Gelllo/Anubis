using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Anubis.Application;
using Anubis.Application.Requests.Users;
using Anubis.Application.Responses.Users;
using Anubis.Domain.UsersDomain;

namespace Anubis.Infrastracture.Commands.UsersCommands
{
    public class CreateUserCommand : ICommandHandler<CreateUserRequest, CreateUserResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateUserCommand(IUnitOfWork uniOfWork, IMapper mapper)
        {
            _unitOfWork = uniOfWork;
            _mapper = mapper;
        }

        public async Task<CreateUserResponse> Handle(CreateUserRequest request, CancellationToken cancellation)
        {
            var userId = await _unitOfWork.UserRepository.InsertUserAsync(_mapper.Map<User>(request));
            return _mapper.Map<CreateUserResponse>(await _unitOfWork.UserRepository.GetUserByID(userId));
        }
    }
}
