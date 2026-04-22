using AccountingScholarships.Application.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Application.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.ReferenceData;

public class GetAllEduAddressTypesQueryHandler : IRequestHandler<GetAllEduAddressTypesQuery, IReadOnlyList<Edu_AddressTypesDto>>
{
    private readonly ISsoRepository<Edu_AddressTypes> _repository;

    public GetAllEduAddressTypesQueryHandler(ISsoRepository<Edu_AddressTypes> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<Edu_AddressTypesDto>> Handle(GetAllEduAddressTypesQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(cancellationToken);

        return entities
            .Select(e => new Edu_AddressTypesDto
            {
                ID = e.ID,
                Title = e.Title
            })
            .ToList()
            .AsReadOnly();
    }
}
