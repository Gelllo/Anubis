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
    public class DeletePatientCommand : ICommandHandler<DeletePatientRequest, DeletePatientResponse>
    {
        private readonly IAnubisUnitOfWork<AnubisContext> _unitOfWork;
        private readonly IMapper _mapper;

        public DeletePatientCommand(IAnubisUnitOfWork<AnubisContext> uniOfWork, IMapper mapper)
        {
            _unitOfWork = uniOfWork;
            _mapper = mapper;
        }

        public async Task<DeletePatientResponse> Handle(DeletePatientRequest request, CancellationToken cancellation)
        {
            _unitOfWork.UserRepository.DeletePatient(request.MedicID, request.PatientID);

            return new DeletePatientResponse();
        }
    }
}