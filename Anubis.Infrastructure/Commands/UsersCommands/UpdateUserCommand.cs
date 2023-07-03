using AutoMapper;
using Anubis.Application.Requests.Users;
using Anubis.Application.Responses.Users;
using Anubis.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anubis.Domain.UsersDomain;
using Anubis.Web.Shared;

namespace Anubis.Infrastructure.Commands.UsersCommands
{
    public class UpdateUserCommand : ICommandHandler<UpdateUserRequest, UpdateUserResponse>
    {
        private readonly IAnubisUnitOfWork<AnubisContext> _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateUserCommand(IAnubisUnitOfWork<AnubisContext> uniOfWork, IMapper mapper)
        {
            _unitOfWork = uniOfWork;
            _mapper = mapper;
        }

        public async Task<UpdateUserResponse> Handle(UpdateUserRequest request, CancellationToken cancellation)
        {
            var user = await _unitOfWork.UserRepository.GetUserByUserId(request.UserID);

            if (user == null)
            {
                throw new Exception(ErrorMessages.INVALID_USER);
            }

            user.Email = request.Email;
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Role = request.Role;

            return _mapper.Map<UpdateUserResponse>(await _unitOfWork.UserRepository.UpdateUserAsync(user));
        }
    }
}
