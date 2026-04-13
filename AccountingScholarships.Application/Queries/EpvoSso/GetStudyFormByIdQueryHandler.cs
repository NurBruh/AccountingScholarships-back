using AccountingScholarships.Application.DTO.EpvoSso;
using AccountingScholarships.Domain.Entities.Real.epvosso;
using AccountingScholarships.Application.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.EpvoSso;

public class GetStudyFormByIdQueryHandler
    : IRequestHandler<GetStudyFormByIdQuery, EpvoStudyFormsDto?>
{
    private readonly IEpvoSsoRepository<Study_forms> _repository;

    public GetStudyFormByIdQueryHandler(IEpvoSsoRepository<Study_forms> repository)
    {
        _repository = repository;
    }

    public async Task<EpvoStudyFormsDto?> Handle(
        GetStudyFormByIdQuery request, CancellationToken cancellationToken)
    {
        var s = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (s is null) return null;

        return new EpvoStudyFormsDto
        {
            Id = s.Id,
            UniversityId = s.UniversityId,
            DegreeId = s.DegreeId,
            NameRu = s.NameRu,
            NameKz = s.NameKz,
            NameEn = s.NameEn,
            CourseCount = s.CourseCount,
            CreditsCount = s.CreditsCount,
            TermsCount = s.TermsCount,
            DepartmentId = s.DepartmentId,
            BaseEducationId = s.BaseEducationId,
            DistanceLearning = s.DistanceLearning,
            TrainingCompletionTerm = s.TrainingCompletionTerm,
            InUse = s.InUse,
            TypeCode = s.TypeCode,
        };
    }
}
