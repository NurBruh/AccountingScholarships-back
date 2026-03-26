using AccountingScholarships.Domain.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Domain.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.Users;

public class GetAllEduUserDocumentsQueryHandler : IRequestHandler<GetAllEduUserDocumentsQuery, IReadOnlyList<Edu_UserDocumentsDto>>
{
    private readonly ISsoRepository<Edu_UserDocuments> _repository;

    public GetAllEduUserDocumentsQueryHandler(ISsoRepository<Edu_UserDocuments> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<Edu_UserDocumentsDto>> Handle(GetAllEduUserDocumentsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllWithIncludesAsync(new[] { "User", "DocumentType", "IssuedByOrg" }, cancellationToken);
        return entities.Select(e => new Edu_UserDocumentsDto
        {
            ID = e.ID,
            UserID = e.UserID,
            DocumentTypeID = e.DocumentTypeID,
            IssuedByID = e.IssuedByID,
            IssuedByText = e.IssuedByText,
            IssuedOn = e.IssuedOn,
            Number = e.Number,
            Description = e.Description,
            FileName = e.FileName,
            DescriptionText = e.DescriptionText,
            User = e.User == null ? null : new Edu_UserDocumentsDto.UserRefDto
            {
                ID = e.User.ID,
                LastName = e.User.LastName,
                FirstName = e.User.FirstName
            },
            DocumentType = e.DocumentType == null ? null : new Edu_UserDocumentsDto.SimpleRefDto
            {
                ID = e.DocumentType.ID,
                Title = e.DocumentType.Title
            },
            IssuedByOrg = e.IssuedByOrg == null ? null : new Edu_UserDocumentsDto.SimpleRefDto
            {
                ID = e.IssuedByOrg.ID,
                Title = e.IssuedByOrg.Title
            }
        }).ToList().AsReadOnly();
    }
}
