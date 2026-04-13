using MediatR;
using AccountingScholarships.Application.DTO;

namespace AccountingScholarships.Application.Queries.University.Students;

public class GetAllEduStudentsQuery : IRequest<IReadOnlyList<EduStudentDto>>
{
}
