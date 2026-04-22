using AccountingScholarships.Application.DTO.EpvoSso;
using AccountingScholarships.Application.Interfaces;
using MediatR;
using EpvoScholarship = AccountingScholarships.Domain.Entities.Real.epvosso.Scholarship;

namespace AccountingScholarships.Application.Queries.EpvoSso;

public class GetEpvoSsoScholarshipByIdQueryHandler
    : IRequestHandler<GetEpvoSsoScholarshipByIdQuery, EpvoScholarshipSsoDto?>
{
    private readonly IEpvoSsoRepository<EpvoScholarship> _repository;

    public GetEpvoSsoScholarshipByIdQueryHandler(IEpvoSsoRepository<EpvoScholarship> repository)
    {
        _repository = repository;
    }

    public async Task<EpvoScholarshipSsoDto?> Handle(
        GetEpvoSsoScholarshipByIdQuery request, CancellationToken cancellationToken)
    {
        var s = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (s is null) return null;

        return new EpvoScholarshipSsoDto
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
