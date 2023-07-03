using Anubis.Application.Requests.Users;
using Anubis.Application.Responses.Users;
using Anubis.Application;
using Anubis.Domain.UsersDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anubis.Web.Shared;
using AutoMapper;

namespace Anubis.Infrastructure.Commands.UsersCommands
{
    public class UpdateMyProfileCommand : ICommandHandler<UpdateMyProfileRequest, UpdateMyProfileResponse>
    {
        private readonly IAnubisUnitOfWork<AnubisContext> _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateMyProfileCommand(IAnubisUnitOfWork<AnubisContext> uniOfWork, IMapper mapper)
        {
            _unitOfWork = uniOfWork;
            _mapper = mapper;
        }

        public async Task<UpdateMyProfileResponse> Handle(UpdateMyProfileRequest request, CancellationToken cancellation)
        {
            var user = await _unitOfWork.UserRepository.GetUserByUserId(request.UserID);

            if (user == null)
            {
                throw new Exception(ErrorMessages.INVALID_USER);
            }

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Email = request.Email;

            await _unitOfWork.UserRepository.UpdateUserAsync(user);

            return _mapper.Map<UpdateMyProfileResponse>(user);
        }
    }
}
