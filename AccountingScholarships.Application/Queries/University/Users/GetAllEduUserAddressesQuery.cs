using AccountingScholarships.Domain.DTO.University;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.Users;

public record GetAllEduUserAddressesQuery : IRequest<IReadOnlyList<Edu_UserAddressesDto>>;
