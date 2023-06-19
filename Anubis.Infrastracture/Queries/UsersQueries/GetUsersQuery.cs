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


namespace Anubis.Infrastracture.Queries.UsersQueries
{
    public class GetUsersQuery : IQueryHandler<GetUsersRequest, GetUsersResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUsersQuery(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetUsersResponse> Handle(GetUsersRequest query, CancellationToken cancellation)
        {
            return new GetUsersResponse() { UsersList = await _unitOfWork.UserRepository.GetUsersAsync() };
        }
    }
}
