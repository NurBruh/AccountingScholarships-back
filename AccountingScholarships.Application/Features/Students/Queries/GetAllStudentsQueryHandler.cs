using AccountingScholarships.Application.DTOs;
using AccountingScholarships.Application.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Features.Students.Queries;

public class GetAllStudentsQueryHandler : IRequestHandler<GetAllStudentsQuery, IReadOnlyList<StudentDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllStudentsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyList<StudentDto>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
    {
        var students = await _unitOfWork.Students.GetAllAsync(cancellationToken);

        return students.Select(s => new StudentDto
        {
            Id = s.Id,
            FirstName = s.FirstName,
            LastName = s.LastName,
            MiddleName = s.MiddleName,
            IIN = s.IIN,
            DateOfBirth = s.DateOfBirth,
            Email = s.Email,
            Phone = s.Phone,
            GroupName = s.GroupName,
            Faculty = s.Faculty,
            Speciality = s.Speciality,
            Course = s.Course,
            EducationForm = s.EducationForm,
            IsActive = s.IsActive,
            CreatedAt = s.CreatedAt,
            UpdatedAt = s.UpdatedAt
        }).ToList().AsReadOnly();
    }
}
