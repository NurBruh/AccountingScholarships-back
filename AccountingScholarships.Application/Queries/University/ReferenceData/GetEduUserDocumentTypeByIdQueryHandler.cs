using AccountingScholarships.Application.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Application.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.ReferenceData;

public class GetEduUserDocumentTypeByIdQueryHandler : IRequestHandler<GetEduUserDocumentTypeByIdQuery, Edu_UserDocumentTypesDto?>
{
    private readonly ISsoRepository<Edu_UserDocumentTypes> _repository;

    public GetEduUserDocumentTypeByIdQueryHandler(ISsoRepository<Edu_UserDocumentTypes> repository)
    {
        _repository = repository;
    }

    public async Task<Edu_UserDocumentTypesDto?> Handle(GetEduUserDocumentTypeByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity is null) return null;
        return new Edu_UserDocumentTypesDto { ID = entity.ID, Title = entity.Title };
    }
}
