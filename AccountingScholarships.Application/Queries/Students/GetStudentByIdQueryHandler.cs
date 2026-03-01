using AccountingScholarships.Domain.DTO;
using AccountingScholarships.Domain.Interfaces;
using AccountingScholarships.Application.Commands.Students;
using MediatR;

namespace AccountingScholarships.Application.Queries.Students;

public class GetStudentByIdQueryHandler : IRequestHandler<GetStudentByIdQuery, StudentDto?>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetStudentByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<StudentDto?> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
    {
        var student = await _unitOfWork.Students.GetWithDetailsAsync(request.Id, cancellationToken);

        if (student is null)
            return null;

        return CreateStudentCommandHandler.MapToDto(student);
    }
}
