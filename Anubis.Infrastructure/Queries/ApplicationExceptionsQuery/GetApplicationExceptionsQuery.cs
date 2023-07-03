using Anubis.Application.Requests.Users;
using Anubis.Application.Responses.Users;
using Anubis.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anubis.Application.Repository;
using Anubis.Application.Requests.ApplicationExceptions;
using Anubis.Application.Responses.ApplicationExceptions;
using AutoMapper;

namespace Anubis.Infrastructure.Queries.ApplicationExceptionsQuery
{
    public class GetApplicationExceptionsQuery : IQueryHandler<GetExceptionsRequest, GetExceptionsResponse>
    {
        private readonly IApplicationExceptionsRepository _applicationExceptionsRepository;
        private readonly IMapper _mapper;

        public GetApplicationExceptionsQuery(IApplicationExceptionsRepository applicationExceptionsRepository, IMapper mapper)
        {
            _applicationExceptionsRepository = applicationExceptionsRepository;
            _mapper = mapper;
        }

        public async Task<GetExceptionsResponse> Handle(GetExceptionsRequest query, CancellationToken cancellation)
        {
            var exceptions = await _applicationExceptionsRepository.GetApplicationExceptions(query.sort, query.order, query.page);
            var exceptionsCount = await _applicationExceptionsRepository.GetExceptionsCount();
            return new GetExceptionsResponse() { ApplicationExceptions = exceptions, total_count = exceptionsCount};
        }
    }
}
