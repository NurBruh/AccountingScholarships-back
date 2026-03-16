using AccountingScholarships.Domain.DTO.EpvoSso;
using AccountingScholarships.Domain.Entities.epvosso;
using AccountingScholarships.Domain.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.EpvoSso;

public class GetAllScholarshipsNewQueryHandler
    : IRequestHandler<GetAllScholarshipsNewQuery, IReadOnlyList<EpvoScholarshipNewDto>>
{
    private readonly IEpvoSsoRepository<Scholarship_new> _repository;

    public GetAllScholarshipsNewQueryHandler(IEpvoSsoRepository<Scholarship_new> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<EpvoScholarshipNewDto>> Handle(
        GetAllScholarshipsNewQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(cancellationToken);
        return entities.Select(s => new EpvoScholarshipNewDto
        {
            UniversityId = s.UniversityId,
            Id = s.Id,
            StudentId = s.StudentId,
            ScholarshipYear = s.ScholarshipYear,
            ScholarshipMonth = s.ScholarshipMonth,
            ScholarshipPayDate = s.ScholarshipPayDate,
            ScholarshipMoney = s.ScholarshipMoney,
            ScholarshipTypeId = s.ScholarshipTypeId,
            TerminationDate = s.TerminationDate,
            AdditionalPayment = s.AdditionalPayment,
            SectionId = s.SectionId,
            ScholarshipAwardYear = s.ScholarshipAwardYear,
            ScholarshipAwardTerm = s.ScholarshipAwardTerm,
            OverallPerformance = s.OverallPerformance,
            TypeCode = s.TypeCode,
        }).ToList().AsReadOnly();
    }
}
