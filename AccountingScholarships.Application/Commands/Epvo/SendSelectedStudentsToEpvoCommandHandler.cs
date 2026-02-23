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
            var payload = new List<EpvoSendPayloadDto>();

            foreach (var iin in request.IINs)
            {
                var posrednik = await _posrednikRepository.GetByIINAsync(iin);
                if (posrednik is null) continue;

                payload.Add(new EpvoSendPayloadDto
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
                    iban = posrednik.iban,
                    isActive = posrednik.IsActive
                });
            }

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
