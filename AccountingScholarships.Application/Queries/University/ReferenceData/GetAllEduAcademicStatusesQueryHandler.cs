using AccountingScholarships.Application.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Application.Interfaces;
using MediatR;
namespace AccountingScholarships.Application.Queries.University.ReferenceData;
public class GetAllEduAcademicStatusesQueryHandler : IRequestHandler<GetAllEduAcademicStatusesQuery, IReadOnlyList<Edu_AcademicStatusesDto>>
{
    private readonly ISsoRepository<Edu_AcademicStatuses> _repository;
    public GetAllEduAcademicStatusesQueryHandler(ISsoRepository<Edu_AcademicStatuses> repository) 
    { 
        _repository = repository;
    }
    public async Task<IReadOnlyList<Edu_AcademicStatusesDto>> Handle(GetAllEduAcademicStatusesQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(cancellationToken);
        return entities.Select(e => new Edu_AcademicStatusesDto 
        { 
            ID = e.ID, Title = e.Title 
        }).ToList().AsReadOnly();
    }
}
