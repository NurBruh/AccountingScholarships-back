using AccountingScholarships.Application.DTO.University;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.Users;

public record GetEduUserAddressByIdQuery(int Id) : IRequest<Edu_UserAddressesDto?>;
