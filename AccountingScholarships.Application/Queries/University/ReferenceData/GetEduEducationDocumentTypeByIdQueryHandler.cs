using AccountingScholarships.Domain.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Domain.Interfaces;
using MediatR;
namespace AccountingScholarships.Application.Queries.University.ReferenceData;
public class GetEduEducationDocumentTypeByIdQueryHandler : IRequestHandler<GetEduEducationDocumentTypeByIdQuery, Edu_EducationDocumentTypesDto?>
{
    private readonly ISsoRepository<Edu_EducationDocumentTypes> _repository;
    public GetEduEducationDocumentTypeByIdQueryHandler(ISsoRepository<Edu_EducationDocumentTypes> repository) { _repository = repository; }
    public async Task<Edu_EducationDocumentTypesDto?> Handle(GetEduEducationDocumentTypeByIdQuery request, CancellationToken cancellationToken)
    {
        var e = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (e is null) return null;
        return new Edu_EducationDocumentTypesDto { ID = e.ID, Title = e.Title };
    }
}
