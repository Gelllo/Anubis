using AutoMapper;
using Anubis.Application.Requests.Users;
using Anubis.Application.Responses.Users;
using Anubis.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anubis.Infrastructure.Commands.UsersCommands
{
    public class DeleteUserCommand : ICommandHandler<DeleteUserRequest, DeleteUserResponse>
    {
        private readonly IAnubisUnitOfWork<AnubisContext> _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteUserCommand(IAnubisUnitOfWork<AnubisContext> uniOfWork, IMapper mapper)
        {
            _unitOfWork = uniOfWork;
            _mapper = mapper;
        }

        public async Task<DeleteUserResponse> Handle(DeleteUserRequest request, CancellationToken cancellation)
        {
            _unitOfWork.UserRepository.DeleteUser(request.Id);
            return new DeleteUserResponse();
        }
    }
}
