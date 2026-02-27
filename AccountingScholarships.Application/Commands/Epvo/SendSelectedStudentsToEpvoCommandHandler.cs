using AccountingScholarships.Domain.DTO;
using AccountingScholarships.Domain.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Commands.Epvo
{
    public class SendSelectedStudentsToEpvoCommandHandler : IRequestHandler<SendSelectedStudentsToEpvoCommand, int>
    {
        private readonly IPosrednikRepository _posrednikRepository;
        private readonly IEpvoApiClient _epvoApiClient;
        public SendSelectedStudentsToEpvoCommandHandler(IPosrednikRepository posrednikRepository, IEpvoApiClient epvoApiClient)
        {
            _posrednikRepository = posrednikRepository;
            _epvoApiClient = epvoApiClient;
        }

        public async Task<int> Handle(SendSelectedStudentsToEpvoCommand request, CancellationToken cancellationToken) 
        {
            if (request.IINs == null || request.IINs.Count == 0) 
            {
                return 0;
            }

            // Загружаем все нужные записи одним запросом (вместо N запросов по одному)
            var posrednikStudents = await _posrednikRepository.FindByIINsAsync(request.IINs, cancellationToken);

            var payload = posrednikStudents.Select(posrednik => new EpvoSendPayloadDto
            {
                IIN = posrednik.IIN,
                FirstName = posrednik.FirstName,
                LastName = posrednik.LastName,
                MiddleName = posrednik.MiddleName,
                Faculty = posrednik.Faculty,
                Speciality = posrednik.Speciality,
                Course = posrednik.Course,
                GrantName = posrednik.GrantName,
                GrantAmount = posrednik.GrantAmount ?? 0,
                ScholarshipName = posrednik.ScholarshipName,
                ScholarshipAmount = posrednik.ScholarshipAmount,
                ScholarshipLostDate = posrednik.ScholarshipLostDate,
                ScholarshipOrderLostDate = posrednik.ScholarshipOrderLostDate,
                ScholarshipOrderCandidateDate = posrednik.ScholarshipOrderCandidateDate,
                ScholarshipNotes = posrednik.ScholarshipNotes,
                iban = posrednik.iban,
                isActive = posrednik.IsActive
            }).ToList();

            if(payload.Count == 0) 
            {
                return 0;
            }

            //один запрос с массивом всех выбранных студентов 
            await _epvoApiClient.SendStudentsAsync(payload, cancellationToken);

            return payload.Count;
        }
    }
}
