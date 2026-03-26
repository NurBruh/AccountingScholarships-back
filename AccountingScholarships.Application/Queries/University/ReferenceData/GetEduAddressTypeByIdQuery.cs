using AccountingScholarships.Domain.DTO.University;
using MediatR;
namespace AccountingScholarships.Application.Queries.University.ReferenceData;
public record GetEduAddressTypeByIdQuery(int Id) : IRequest<Edu_AddressTypesDto?>;
