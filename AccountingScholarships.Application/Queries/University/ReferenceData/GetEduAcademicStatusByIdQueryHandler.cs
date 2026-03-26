using AccountingScholarships.Domain.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Domain.Interfaces;
using MediatR;
namespace AccountingScholarships.Application.Queries.University.ReferenceData;
public class GetEduAcademicStatusByIdQueryHandler : IRequestHandler<GetEduAcademicStatusByIdQuery, Edu_AcademicStatusesDto?>
{
    private readonly ISsoRepository<Edu_AcademicStatuses> _repository;
    public GetEduAcademicStatusByIdQueryHandler(ISsoRepository<Edu_AcademicStatuses> repository) { _repository = repository; }
    public async Task<Edu_AcademicStatusesDto?> Handle(GetEduAcademicStatusByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity is null) return null;
        return new Edu_AcademicStatusesDto { ID = entity.ID, Title = entity.Title };
    }
}
