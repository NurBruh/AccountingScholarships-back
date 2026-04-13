using AccountingScholarships.Application.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Application.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.ReferenceData;

public class GetAllEduEducationDocumentSubTypesQueryHandler : IRequestHandler<GetAllEduEducationDocumentSubTypesQuery, IReadOnlyList<Edu_EducationDocumentSubTypesDto>>
{
    private readonly ISsoRepository<Edu_EducationDocumentSubTypes> _repository;

    public GetAllEduEducationDocumentSubTypesQueryHandler(ISsoRepository<Edu_EducationDocumentSubTypes> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<Edu_EducationDocumentSubTypesDto>> Handle(GetAllEduEducationDocumentSubTypesQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(cancellationToken);

        return entities
            .Select(e => new Edu_EducationDocumentSubTypesDto
            {
                ID = e.ID,
                Title = e.Title
            })
            .ToList()
            .AsReadOnly();
    }
}
