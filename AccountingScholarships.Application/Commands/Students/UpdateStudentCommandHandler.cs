
using AccountingScholarships.Domain.DTO;
using AccountingScholarships.Domain.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Commands.Students;

public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand, StudentDto?>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateStudentCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<StudentDto?> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
    {
        var student = await _unitOfWork.Students.GetByIdAsync(request.Id, cancellationToken);

        if (student is null)
            return null;

        var dto = request.Student;

        student.FirstName = dto.FirstName;
        student.LastName = dto.LastName;
        student.MiddleName = dto.MiddleName;
        student.IIN = dto.IIN;
        student.DateOfBirth = dto.DateOfBirth;
        student.Email = dto.Email;
        student.Phone = dto.Phone;
        student.GroupName = dto.GroupName;
        student.Faculty = dto.Faculty;
        student.Speciality = dto.Speciality;
        student.Course = dto.Course;
        student.EducationForm = dto.EducationForm;
        student.iban = dto.iban;
        student.IsActive = dto.IsActive;
        student.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.Students.UpdateAsync(student, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

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
            iban = student.iban,
            IsActive = student.IsActive,
            CreatedAt = student.CreatedAt,
            UpdatedAt = student.UpdatedAt
        };
    }
}
