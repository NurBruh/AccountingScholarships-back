using AccountingScholarships.Application.DTOs;
using AccountingScholarships.Application.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Features.Students.Queries;

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

        return new StudentDto
        {
            Id = student.Id,
            FirstName = student.FirstName,
            LastName = student.LastName,
            MiddleName = student.MiddleName,
            IIN = student.IIN,
            DateOfBirth = student.DateOfBirth,
            Email = student.Email,
            Phone = student.Phone,
            GroupName = student.GroupName,
            Faculty = student.Faculty,
            Speciality = student.Speciality,
            Course = student.Course,
            EducationForm = student.EducationForm,
            IsActive = student.IsActive,
            CreatedAt = student.CreatedAt,
            UpdatedAt = student.UpdatedAt,
            Grants = student.Grants.Select(g => new GrantDto
            {
                Id = g.Id,
                Name = g.Name,
                Type = g.Type,
                Amount = g.Amount,
                StartDate = g.StartDate,
                EndDate = g.EndDate,
                IsActive = g.IsActive,
                StudentId = g.StudentId
            }).ToList(),
            Scholarships = student.Scholarships.Select(s => new ScholarshipDto
            {
                Id = s.Id,
                Name = s.Name,
                Type = s.Type,
                Amount = s.Amount,
                StartDate = s.StartDate,
                EndDate = s.EndDate,
                IsActive = s.IsActive,
                StudentId = s.StudentId
            }).ToList()
        };
    }
}
