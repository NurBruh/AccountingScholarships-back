using AccountingScholarships.Application.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Application.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.ReferenceData;

public class GetAllEduUserDocumentTypesQueryHandler : IRequestHandler<GetAllEduUserDocumentTypesQuery, IReadOnlyList<Edu_UserDocumentTypesDto>>
{
    private readonly ISsoRepository<Edu_UserDocumentTypes> _repository;

    public GetAllEduUserDocumentTypesQueryHandler(ISsoRepository<Edu_UserDocumentTypes> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<Edu_UserDocumentTypesDto>> Handle(GetAllEduUserDocumentTypesQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(cancellationToken);
        return entities.Select(e => new Edu_UserDocumentTypesDto { ID = e.ID, Title = e.Title }).ToList().AsReadOnly();
    }
}
