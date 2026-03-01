using AccountingScholarships.Domain.DTO;
using AccountingScholarships.Domain.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Commands.Epvo
{
    public class SendSelectedStudentsToEpvoCommandHandler : IRequestHandler<SendSelectedStudentsToEpvoCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEpvoApiClient _epvoApiClient;

        public SendSelectedStudentsToEpvoCommandHandler(IUnitOfWork unitOfWork, IEpvoApiClient epvoApiClient)
        {
            _unitOfWork = unitOfWork;
            _epvoApiClient = epvoApiClient;
        }

        public async Task<int> Handle(SendSelectedStudentsToEpvoCommand request, CancellationToken cancellationToken) 
        {
            if (request.IINs == null || request.IINs.Count == 0) 
            {
                return 0;
            }

            var ssoStudents = await _unitOfWork.Students.FindByIINsAsync(request.IINs, cancellationToken);

            var payload = ssoStudents.Select(sso =>
            {
                var activeGrant = sso.Grants?.FirstOrDefault(g => g.IsActive);
                var activeScholarship = sso.Scholarships?.FirstOrDefault(s => s.IsActive);
                var latestScholarship = sso.Scholarships?.OrderByDescending(s => s.CreatedAt).FirstOrDefault();

                return new EpvoSendPayloadDto
                {
                    IIN = sso.IIN,
                    FirstName = sso.FirstName,
                    LastName = sso.LastName,
                    MiddleName = sso.MiddleName,
                    Faculty = sso.Speciality?.Department?.Institute?.InstituteName,
                    Speciality = sso.Speciality?.SpecialityName,
                    Course = sso.Course,
                    GrantName = activeGrant?.Name,
                    GrantAmount = activeGrant?.Amount ?? 0,
                    ScholarshipName = activeScholarship?.Name,
                    ScholarshipAmount = activeScholarship?.Amount,
                    ScholarshipLostDate = latestScholarship?.LostDate,
                    ScholarshipOrderLostDate = latestScholarship?.OrderLostDate,
                    ScholarshipOrderCandidateDate = latestScholarship?.OrderCandidateDate,
                    ScholarshipNotes = latestScholarship?.Notes,
                    iban = sso.iban,
                    isActive = sso.IsActive
                };
            }).ToList();

            if (payload.Count == 0) 
            {
                return 0;
            }

            await _epvoApiClient.SendStudentsAsync(payload, cancellationToken);

            return payload.Count;
        }
    }
}
