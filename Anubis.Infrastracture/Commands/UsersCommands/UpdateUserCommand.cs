using AutoMapper;
using Anubis.Application.Requests.Users;
using Anubis.Application.Responses.Users;
using Anubis.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anubis.Domain;

namespace Anubis.Infrastracture.Commands.UsersCommands
{
    public class UpdateUserCommand : ICommandHandler<UpdateUserRequest, UpdateUserResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateUserCommand(IUnitOfWork uniOfWork, IMapper mapper)
        {
            _unitOfWork = uniOfWork;
            _mapper = mapper;
        }

        public async Task<UpdateUserResponse> Handle(UpdateUserRequest request, CancellationToken cancellation)
        {
            return _mapper.Map<UpdateUserResponse>(await _unitOfWork.UserRepository.UpdateUserAsync(_mapper.Map<User>(request)));
        }
    }
}
