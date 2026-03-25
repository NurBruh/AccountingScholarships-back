using AccountingScholarships.Domain.DTO;
using AccountingScholarships.Domain.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;

namespace AccountingScholarships.Domain.Interfaces;

public interface IEduStudentRepository : ISsoRepository<Edu_Students>
{
    Task<Edu_Students?> GetWithDetailsAsync(int studentId, CancellationToken cancellationToken = default);
    Task<Edu_Students?> GetByIINAsync(string iin, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Edu_Students>> GetAllWithDetailsAsync(CancellationToken cancellationToken = default);
    Task<EduStudentDto?> GetAsDtoAsync(int studentId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<EduStudentDto>> GetAllAsDtoAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<StudentWithUserDto>> GetAllSsoStudents(CancellationToken cancellationToken = default);
}
