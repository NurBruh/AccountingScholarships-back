using AccountingScholarships.Application.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Application.Interfaces;
using MediatR;
namespace AccountingScholarships.Application.Queries.University.ReferenceData;
public class GetEduEducationDocumentSubTypeByIdQueryHandler : IRequestHandler<GetEduEducationDocumentSubTypeByIdQuery, Edu_EducationDocumentSubTypesDto?>
{
    private readonly ISsoRepository<Edu_EducationDocumentSubTypes> _repository;
    public GetEduEducationDocumentSubTypeByIdQueryHandler(ISsoRepository<Edu_EducationDocumentSubTypes> repository) { _repository = repository; }
    public async Task<Edu_EducationDocumentSubTypesDto?> Handle(GetEduEducationDocumentSubTypeByIdQuery request, CancellationToken cancellationToken)
    {
        var e = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (e is null) return null;
        return new Edu_EducationDocumentSubTypesDto { ID = e.ID, Title = e.Title };
    }
}
