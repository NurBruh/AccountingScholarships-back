using AccountingScholarships.Domain.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Domain.Interfaces;
using MediatR;
namespace AccountingScholarships.Application.Queries.University.ReferenceData;
public class GetEduEducationTypeByIdQueryHandler : IRequestHandler<GetEduEducationTypeByIdQuery, Edu_EducationTypesDto?>
{
    private readonly ISsoRepository<Edu_EducationTypes> _repository;
    public GetEduEducationTypeByIdQueryHandler(ISsoRepository<Edu_EducationTypes> repository) { _repository = repository; }
    public async Task<Edu_EducationTypesDto?> Handle(GetEduEducationTypeByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity is null) return null;
        return new Edu_EducationTypesDto { ID = entity.ID, Title = entity.Title, NoBDID = entity.NoBDID };
    }
}
