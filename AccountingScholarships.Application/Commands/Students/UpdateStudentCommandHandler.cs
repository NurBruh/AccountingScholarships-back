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
        student.Course = dto.Course;
        student.iban = dto.iban;
        student.Description = dto.Description;
        student.Sex = dto.Sex;
        student.IsActive = dto.IsActive;
        student.SpecialityId = dto.SpecialityId;
        student.StudyFormId = dto.StudyFormId;
        student.DegreeLevelId = dto.DegreeLevelId;
        student.BankId = dto.BankId;
        student.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.Students.UpdateAsync(student, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var loaded = await _unitOfWork.Students.GetWithDetailsAsync(student.Id, cancellationToken);
        return CreateStudentCommandHandler.MapToDto(loaded ?? student);
    }
}
