using AccountingScholarships.Domain.DTO;
using AccountingScholarships.Domain.Entities.university;

namespace AccountingScholarships.Domain.Interfaces;

public interface IEduStudentRepository : ISsoRepository<EduStudents>
{
    Task<EduStudents?> GetWithDetailsAsync(int studentId, CancellationToken cancellationToken = default);
    Task<EduStudents?> GetByIINAsync(string iin, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<EduStudents>> GetAllWithDetailsAsync(CancellationToken cancellationToken = default);
    Task<EduStudentDto?> GetAsDtoAsync(int studentId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<EduStudentDto>> GetAllAsDtoAsync(CancellationToken cancellationToken = default);
    Task<List<AccountingScholarships.Domain.DTO.University.StudentWithUserDto>> GetTopStudentsWithUserAsync(int count, CancellationToken cancellationToken = default);
}
