using AccountingScholarships.Application.DTO.University;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.ReferenceData;

public record GetAllEduAddressTypesQuery : IRequest<IReadOnlyList<Edu_AddressTypesDto>>;
