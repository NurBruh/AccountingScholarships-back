using MediatR;
using AccountingScholarships.Application.DTO.University;

namespace AccountingScholarships.Application.Queries.University.Students;


public class GetAllSsoStudentsQuery : IRequest<IReadOnlyList<StudentWithUserDto>>
{
    
}
