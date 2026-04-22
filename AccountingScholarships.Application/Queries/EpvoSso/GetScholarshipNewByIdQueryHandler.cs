using AccountingScholarships.Application.DTO.EpvoSso;
using AccountingScholarships.Domain.Entities.Real.epvosso;
using AccountingScholarships.Application.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.EpvoSso;

public class GetScholarshipNewByIdQueryHandler
    : IRequestHandler<GetScholarshipNewByIdQuery, EpvoScholarshipNewDto?>
{
    private readonly IEpvoSsoRepository<Scholarship_new> _repository;

    public GetScholarshipNewByIdQueryHandler(IEpvoSsoRepository<Scholarship_new> repository)
    {
        _repository = repository;
    }

    public async Task<EpvoScholarshipNewDto?> Handle(
        GetScholarshipNewByIdQuery request, CancellationToken cancellationToken)
    {
        var s = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (s is null) return null;

        return new EpvoScholarshipNewDto
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
        };
    }
}
