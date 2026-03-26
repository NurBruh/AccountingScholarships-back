using AccountingScholarships.Domain.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Domain.Interfaces;
using MediatR;
namespace AccountingScholarships.Application.Queries.University.ReferenceData;
public class GetEduAddressTypeByIdQueryHandler : IRequestHandler<GetEduAddressTypeByIdQuery, Edu_AddressTypesDto?>
{
    private readonly ISsoRepository<Edu_AddressTypes> _repository;
    public GetEduAddressTypeByIdQueryHandler(ISsoRepository<Edu_AddressTypes> repository) { _repository = repository; }
    public async Task<Edu_AddressTypesDto?> Handle(GetEduAddressTypeByIdQuery request, CancellationToken cancellationToken)
    {
        var e = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (e is null) return null;
        return new Edu_AddressTypesDto { ID = e.ID, Title = e.Title };
    }
}
