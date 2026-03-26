using AccountingScholarships.Domain.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Domain.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.ReferenceData;

public class GetAllEduEducationDocumentTypesQueryHandler : IRequestHandler<GetAllEduEducationDocumentTypesQuery, IReadOnlyList<Edu_EducationDocumentTypesDto>>
{
    private readonly ISsoRepository<Edu_EducationDocumentTypes> _repository;

    public GetAllEduEducationDocumentTypesQueryHandler(ISsoRepository<Edu_EducationDocumentTypes> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<Edu_EducationDocumentTypesDto>> Handle(GetAllEduEducationDocumentTypesQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(cancellationToken);

        return entities
            .Select(e => new Edu_EducationDocumentTypesDto
            {
                ID = e.ID,
                Title = e.Title
            })
            .ToList()
            .AsReadOnly();
    }
}
