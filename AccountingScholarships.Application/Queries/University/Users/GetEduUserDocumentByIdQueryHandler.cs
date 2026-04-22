using AccountingScholarships.Application.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Application.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.Users;

public class GetEduUserDocumentByIdQueryHandler : IRequestHandler<GetEduUserDocumentByIdQuery, Edu_UserDocumentsDto?>
{
    private readonly ISsoRepository<Edu_UserDocuments> _repository;

    public GetEduUserDocumentByIdQueryHandler(ISsoRepository<Edu_UserDocuments> repository)
    {
        _repository = repository;
    }

    public async Task<Edu_UserDocumentsDto?> Handle(GetEduUserDocumentByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.FindFirstWithIncludesAsync(
            x => x.ID == request.Id,
            new[] { "User", "DocumentType", "IssuedByOrg" },
            cancellationToken);
        if (entity is null) return null;
        return new Edu_UserDocumentsDto
        {
            ID = entity.ID,
            UserID = entity.UserID,
            DocumentTypeID = entity.DocumentTypeID,
            IssuedByID = entity.IssuedByID,
            IssuedByText = entity.IssuedByText,
            IssuedOn = entity.IssuedOn,
            Number = entity.Number,
            Description = entity.Description,
            FileName = entity.FileName,
            DescriptionText = entity.DescriptionText,
            User = entity.User == null ? null : new Edu_UserDocumentsDto.UserRefDto
            {
                ID = entity.User.ID,
                LastName = entity.User.LastName,
                FirstName = entity.User.FirstName
            },
            DocumentType = entity.DocumentType == null ? null : new Edu_UserDocumentsDto.SimpleRefDto
            {
                ID = entity.DocumentType.ID,
                Title = entity.DocumentType.Title
            },
            IssuedByOrg = entity.IssuedByOrg == null ? null : new Edu_UserDocumentsDto.SimpleRefDto
            {
                ID = entity.IssuedByOrg.ID,
                Title = entity.IssuedByOrg.Title
            }
        };
    }
}
