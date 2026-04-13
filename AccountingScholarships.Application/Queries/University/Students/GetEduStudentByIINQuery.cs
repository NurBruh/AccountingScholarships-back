using MediatR;
using AccountingScholarships.Application.DTO;

namespace AccountingScholarships.Application.Queries.University.Students;

public class GetEduStudentByIINQuery : IRequest<EduStudentDto?>
{
    public string IIN { get; init; }

    public GetEduStudentByIINQuery(string iin)
    {
        IIN = iin;
    }
}
