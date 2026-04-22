using AccountingScholarships.Application.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Application.Interfaces;
using MediatR;
namespace AccountingScholarships.Application.Queries.University.ReferenceData;
public class GetEduControlTypeByIdQueryHandler : IRequestHandler<GetEduControlTypeByIdQuery, Edu_ControlTypesDto?>
{
    private readonly ISsoRepository<Edu_ControlTypes> _repository;
    public GetEduControlTypeByIdQueryHandler(ISsoRepository<Edu_ControlTypes> repository) { _repository = repository; }
    public async Task<Edu_ControlTypesDto?> Handle(GetEduControlTypeByIdQuery request, CancellationToken cancellationToken)
    {
        var e = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (e is null) return null;
        return new Edu_ControlTypesDto { ID = e.ID, Title = e.Title, ShortTitle = e.ShortTitle };
    }
}
