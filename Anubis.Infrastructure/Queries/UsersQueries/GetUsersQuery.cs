using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Anubis.Application;
using Anubis.Application.Repository;
using Anubis.Application.Requests.Users;
using Anubis.Application.Responses.Users;


namespace Anubis.Infrastructure.Queries.UsersQueries
{
    public class GetUsersQuery : IQueryHandler<GetUsersRequest, GetUsersResponse>
    {
        private readonly IAnubisUnitOfWork<AnubisContext> _unitOfWork;
        private readonly IMapper _mapper;

        public GetUsersQuery(IAnubisUnitOfWork<AnubisContext> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetUsersResponse> Handle(GetUsersRequest query, CancellationToken cancellation)
        {
            var users = await _unitOfWork.UserRepository.GetUsersAsync(query.sort, query.order, query.page);
            var usersCount = await _unitOfWork.UserRepository.GetUsersCount();
            return new GetUsersResponse(){UsersList = _mapper.Map<IEnumerable<UserDTO>>(users), TotalCount = usersCount};
        }
    }
}
