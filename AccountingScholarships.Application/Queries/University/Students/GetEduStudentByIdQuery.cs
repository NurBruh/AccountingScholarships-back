using MediatR;
using AccountingScholarships.Application.DTO;

namespace AccountingScholarships.Application.Queries.University.Students;

public class GetEduStudentByIdQuery : IRequest<EduStudentDto?>
{
    public int StudentId { get; init; }

    public GetEduStudentByIdQuery(int studentId)
    {
        StudentId = studentId;
    }
}
