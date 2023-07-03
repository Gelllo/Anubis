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
    public class GetMyPatientsQuery : IQueryHandler<GetMyPatientsRequest, GetMyPatientsResponse>
    {
        private readonly IAnubisUnitOfWork<AnubisContext> _unitOfWork;
        private readonly IMapper _mapper;

        public GetMyPatientsQuery(IAnubisUnitOfWork<AnubisContext> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetMyPatientsResponse> Handle(GetMyPatientsRequest query, CancellationToken cancellation)
        {
            var users = await _unitOfWork.UserRepository.GetPatientsForMedic(query.MedicId);
            return new GetMyPatientsResponse(){Patients = _mapper.Map<IEnumerable<UserDTO>>(users)};
        }
    }
}