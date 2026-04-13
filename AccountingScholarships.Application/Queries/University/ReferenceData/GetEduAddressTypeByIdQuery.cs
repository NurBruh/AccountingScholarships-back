using AccountingScholarships.Application.DTO.University;
using MediatR;
namespace AccountingScholarships.Application.Queries.University.ReferenceData;
public record GetEduAddressTypeByIdQuery(int Id) : IRequest<Edu_AddressTypesDto?>;
