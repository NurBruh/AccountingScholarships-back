using AccountingScholarships.Domain.DTO;
using AccountingScholarships.Domain.Interfaces;
using AccountingScholarships.Domain.Entities.Students;
using MediatR;

namespace AccountingScholarships.Application.Commands.Students;

public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, StudentDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateStudentCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<StudentDto> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Student;

        var student = new Student
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            MiddleName = dto.MiddleName,
            IIN = dto.IIN,
            DateOfBirth = dto.DateOfBirth,
            Email = dto.Email,
            Phone = dto.Phone,
            GroupName = dto.GroupName,
            Course = dto.Course,
            iban = dto.iban,
            Description = dto.Description,
            Sex = dto.Sex,
            SpecialityId = dto.SpecialityId,
            StudyFormId = dto.StudyFormId,
            DegreeLevelId = dto.DegreeLevelId,
            BankId = dto.BankId,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        await _unitOfWork.Students.AddAsync(student, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Перезагружаем с навигационными свойствами
        var loaded = await _unitOfWork.Students.GetWithDetailsAsync(student.Id, cancellationToken);
        return MapToDto(loaded ?? student);
    }

    internal static StudentDto MapToDto(Student s)
    {
        return new StudentDto
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
            Course = s.Course,
            IsActive = s.IsActive,
            iban = s.iban,
            Description = s.Description,
            Sex = s.Sex,
            CreatedAt = s.CreatedAt,
            UpdatedAt = s.UpdatedAt,
            // Resolved names
            Faculty = s.Speciality?.Department?.Institute?.InstituteName,
            Speciality = s.Speciality?.SpecialityName,
            DepartmentName = s.Speciality?.Department?.DepartmentName,
            EducationForm = s.StudyForm?.StudyFormName,
            DegreeLevel = s.DegreeLevel?.DegreeName,
            BankName = s.Bank?.RecipientBank,
            // FK IDs
            SpecialityId = s.SpecialityId,
            StudyFormId = s.StudyFormId,
            DegreeLevelId = s.DegreeLevelId,
            BankId = s.BankId,
            Grants = s.Grants?.Select(g => new GrantDto
            {
                Id = g.Id,
                Name = g.Name,
                Type = g.Type,
                Amount = g.Amount,
                StartDate = g.StartDate,
                EndDate = g.EndDate,
                IsActive = g.IsActive,
                StudentId = g.StudentId
            }).ToList() ?? new(),
            Scholarships = s.Scholarships?.Select(sc => new ScholarshipDto
            {
                Id = sc.Id,
                Name = sc.Name,
                Type = sc.Type,
                Amount = sc.Amount,
                StartDate = sc.StartDate,
                LostDate = sc.LostDate,
                OrderLostDate = sc.OrderLostDate,
                OrderCandidateDate = sc.OrderCandidateDate,
                Notes = sc.Notes,
                IsActive = sc.IsActive,
                StudentId = sc.StudentId
            }).ToList() ?? new()
        };
    }
}
