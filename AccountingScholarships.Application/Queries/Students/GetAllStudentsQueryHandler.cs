using AccountingScholarships.Domain.DTO;
using AccountingScholarships.Domain.Interfaces;
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
            UpdatedAt = s.UpdatedAt,
            Grants = s.Grants.Select(g => new GrantDto
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
            Scholarships = s.Scholarships.Select(sc => new ScholarshipDto
            {
                Id = sc.Id,
                Name = sc.Name,
                Type = sc.Type,
                Amount = sc.Amount,
                StartDate = sc.StartDate,
                EndDate = sc.EndDate,
                IsActive = sc.IsActive,
                StudentId = sc.StudentId
            }).ToList()
        }).ToList().AsReadOnly();
    }
}