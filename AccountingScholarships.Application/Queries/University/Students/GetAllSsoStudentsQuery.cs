using MediatR;
using AccountingScholarships.Domain.DTO.University;

namespace AccountingScholarships.Application.Queries.University.Students;


public class GetAllSsoStudentsQuery : IRequest<IReadOnlyList<StudentWithUserDto>>
{
    
}
