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
    public class AddPatientCommand : ICommandHandler<AddPatientRequest, AddPatientResponse>
    {
        private readonly IAnubisUnitOfWork<AnubisContext> _unitOfWork;
        private readonly IMapper _mapper;

        public AddPatientCommand(IAnubisUnitOfWork<AnubisContext> uniOfWork, IMapper mapper)
        {
            _unitOfWork = uniOfWork;
            _mapper = mapper;
        }

        public async Task<AddPatientResponse> Handle(AddPatientRequest request, CancellationToken cancellation)
        {
            await _unitOfWork.UserRepository.AddPatient(request.MedicUsername, request.PatientEmail);

            return new AddPatientResponse();
        }
    }
}