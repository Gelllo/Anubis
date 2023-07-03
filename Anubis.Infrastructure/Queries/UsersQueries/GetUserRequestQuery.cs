using Anubis.Application.Requests.Users;
using Anubis.Application.Responses.Users;
using Anubis.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Anubis.Infrastructure.Queries.UsersQueries
{
    public class GetUserQuery : IQueryHandler<GetUserRequest, GetUserResponse>
    {
        private readonly IAnubisUnitOfWork<AnubisContext> _unitOfWork;
        private readonly IMapper _mapper;

        public GetUserQuery(IAnubisUnitOfWork<AnubisContext> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetUserResponse> Handle(GetUserRequest query, CancellationToken cancellation)
        {
            var user = await _unitOfWork.UserRepository.GetUserByUserId(query.UserID);
            return new GetUserResponse() {User = _mapper.Map<UserDTO>(user)};
        }
    }
}
