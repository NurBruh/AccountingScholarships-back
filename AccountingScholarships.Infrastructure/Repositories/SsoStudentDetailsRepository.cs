using AccountingScholarships.Domain.DTO.EpvoSso.EpvoJoin;
using AccountingScholarships.Domain.Interfaces;
using AccountingScholarships.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AccountingScholarships.Infrastructure.Repositories;

public class SsoStudentDetailsRepository : ISsoStudentDetailsRepository
{
    private readonly EpvoSsoDbContext _context;

    public SsoStudentDetailsRepository(EpvoSsoDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<StudentSsoDetailDto>> GetAllAsync(CancellationToken ct = default)
    {
        const string sql = """
            SELECT DISTINCT
                ss.universityId,
                ss.studentId,
                CONCAT(ss.firstName, ' ', ss.lastName, ' ', ss.patronymic) AS FullName,
                ss.iinPlt,
                ss.courseNumber,
                sf.nameRu   AS StudyForm,
                CASE
                    WHEN ss.paymentFormId = 1 THEN N'Платник'
                    WHEN ss.paymentFormId = 2 THEN N'Стипендия'
                END AS PaymentType,
                ss.gpa,
                sl.nameRu   AS StudyLanguage,
                pe.professionNameRu AS ProfessionName,
                se.nameRu   AS Specialization,
                fac.facultyNameRu   AS FacultyName,
                CASE
                    WHEN ss.sexId = 2 THEN N'Мужского пола'
                    WHEN ss.sexId = 1 THEN N'Женского пола'
                END AS Sex,
                CASE
                    WHEN ss.grantType = -4 THEN N'Государственный грант'
                    WHEN ss.grantType = -7 THEN N'Из собственных средств'
                    WHEN ss.grantType = -6 THEN N'Трехсторонняя форма обучения'
                END AS GrantType,
                si.iic
            FROM STUDENT_SSO ss
            JOIN STUDY_FORMS           sf  ON sf.id          = ss.studyFormId
            JOIN STUDYLANGUAGES         sl  ON sl.id          = ss.studyLanguageId
            JOIN PROFESSION  pe  ON pe.professionId = ss.professionid
            JOIN FACULTIES              fac ON fac.facultyId  = ss.facultyId
            JOIN SPECIALITIES_EPVO_2025 se  ON se.id          = ss.specializationId
            JOIN STUDENT_INFO           si  ON si.studentId   = ss.studentId
            """;

        return await _context.Database
            .SqlQueryRaw<StudentSsoDetailDto>(sql)
            .ToListAsync(ct);
    }
}
