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
            var query =
            from ss  in _context.Student_Sso
            join sf  in _context.Study_forms      on ss.StudyFormId               equals sf.Id
            join sl  in _context.StudyLanguages   on ss.StudyLanguageId            equals (int?)sl.Id
            join pe  in _context.Professions      on ss.ProfessionId               equals (int?)pe.ProfessionId
            join fac in _context.Faculties        on ss.FacultyId                  equals (int?)fac.FacultyId
            join se  in _context.SpecialitiesEpvo on (float?)ss.SpecializationId   equals se.Id
            join si  in _context.Student_Info     on ss.StudentId                  equals si.StudentId
            select new StudentSsoDetailDto
            {
                UniversityId   = ss.UniversityId,
                StudentId      = ss.StudentId,
                FullName       = (ss.LastName + " " + ss.FirstName + " " + ss.Patronymic).Trim(),
                IinPlt         = ss.IinPlt,
                CourseNumber   = ss.CourseNumber,
                StudyForm      = sf.NameRu,
                PaymentType    = ss.PaymentFormId == 1 ? "Платник"
                               : ss.PaymentFormId == 2 ? "Стипендия" : null,
                Gpa            = ss.Gpa,
                StudyLanguage  = sl.NameRu,
                ProfessionName = pe.ProfessionNameRu,
                Specialization = se.NameRu,
                FacultyName    = fac.FacultyNameRu,
                Sex            = ss.SexId == 2 ? "Мужского пола"
                               : ss.SexId == 1 ? "Женского пола" : null,
                GrantType      = ss.GrantType == -4 ? "Государственный грант"
                               : ss.GrantType == -7 ? "Из собственных средств"
                               : ss.GrantType == -6 ? "Трехсторонняя форма обучения" : null,
                Iic            = si.Iic,
            };

        return await query.Distinct().ToListAsync(ct);
    }
}
