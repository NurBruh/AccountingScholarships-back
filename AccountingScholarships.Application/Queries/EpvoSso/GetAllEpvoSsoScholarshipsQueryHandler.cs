using AccountingScholarships.Application.DTO.EpvoSso;
using AccountingScholarships.Application.Interfaces;
using MediatR;
using EpvoScholarship = AccountingScholarships.Domain.Entities.Real.epvosso.Scholarship;

namespace AccountingScholarships.Application.Queries.EpvoSso;

public class GetAllEpvoSsoScholarshipsQueryHandler
    : IRequestHandler<GetAllEpvoSsoScholarshipsQuery, IReadOnlyList<EpvoScholarshipSsoDto>>
{
    private readonly IEpvoSsoRepository<EpvoScholarship> _repository;

    public GetAllEpvoSsoScholarshipsQueryHandler(IEpvoSsoRepository<EpvoScholarship> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<EpvoScholarshipSsoDto>> Handle(
        GetAllEpvoSsoScholarshipsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(cancellationToken);
        return entities.Select(s => new EpvoScholarshipSsoDto
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
