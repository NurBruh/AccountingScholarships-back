using AccountingScholarships.Domain.DTO;
using AccountingScholarships.Domain.Interfaces;
using AccountingScholarships.Application.Commands.Students;
using MediatR;

namespace AccountingScholarships.Application.Queries.Students;

public class GetAllStudentsQueryHandler : IRequestHandler<GetAllStudentsQuery, IReadOnlyList<StudentDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllStudentsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyList<StudentDto>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
    {
        var students = await _unitOfWork.Students.GetAllWithDetailsAsync(cancellationToken);
        return students.Select(CreateStudentCommandHandler.MapToDto).ToList().AsReadOnly();
    }
}