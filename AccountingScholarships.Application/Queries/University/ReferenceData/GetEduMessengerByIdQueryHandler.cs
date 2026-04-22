using AccountingScholarships.Application.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Application.Interfaces;
using MediatR;
namespace AccountingScholarships.Application.Queries.University.ReferenceData;
public class GetEduMessengerByIdQueryHandler : IRequestHandler<GetEduMessengerByIdQuery, Edu_MessengersDto?>
{
    private readonly ISsoRepository<Edu_Messengers> _repository;
    public GetEduMessengerByIdQueryHandler(ISsoRepository<Edu_Messengers> repository) { _repository = repository; }
    public async Task<Edu_MessengersDto?> Handle(GetEduMessengerByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity is null) return null;
        return new Edu_MessengersDto { ID = entity.ID, Title = entity.Title };
    }
}
