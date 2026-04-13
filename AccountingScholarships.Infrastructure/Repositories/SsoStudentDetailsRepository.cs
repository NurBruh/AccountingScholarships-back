using AccountingScholarships.Application.DTO.EpvoSso.EpvoJoin;
using AccountingScholarships.Application.Interfaces;
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

            join sf  in _context.Study_forms      on ss.StudyFormId          equals sf.Id           into sfG
            from sf  in sfG.DefaultIfEmpty()

            join sl  in _context.StudyLanguages   on ss.StudyLanguageId      equals (int?)sl.Id     into slG
            from sl  in slG.DefaultIfEmpty()

            join pe  in _context.Professions      on ss.ProfessionId         equals (int?)pe.ProfessionId into peG
            from pe  in peG.DefaultIfEmpty()

            join fac in _context.Faculties        on ss.FacultyId            equals (int?)fac.FacultyId   into facG
            from fac in facG.DefaultIfEmpty()

            join se  in _context.SpecialitiesEpvo on (float?)ss.SpecializationId equals se.Id        into seG
            from se  in seG.DefaultIfEmpty()

            join si  in _context.Student_Info     on ss.StudentId            equals si.StudentId     into siG
            from si  in siG.DefaultIfEmpty()

            select new StudentSsoDetailDto
            {
                UniversityId   = ss.UniversityId,
                StudentId      = ss.StudentId,
                FullName       = (ss.LastName + " " + ss.FirstName + " " + ss.Patronymic).Trim(),
                IinPlt         = ss.IinPlt,
                CourseNumber   = ss.CourseNumber,
                StudyForm      = sf != null ? sf.NameRu : null,
                PaymentType    = ss.PaymentFormId == 1 ? "Платник"
                               : ss.PaymentFormId == 2 ? "Стипендия" : null,
                Gpa            = ss.Gpa,
                StudyLanguage  = sl != null ? sl.NameRu : null,
                ProfessionName = pe != null ? pe.ProfessionNameRu : null,
                Specialization = se != null ? se.NameRu : null,
                FacultyName    = fac != null ? fac.FacultyNameRu : null,
                Sex            = ss.SexId == 2 ? "Мужского пола"
                               : ss.SexId == 1 ? "Женского пола" : null,
                GrantType      = ss.GrantType == -4 ? "Государственный грант"
                               : ss.GrantType == -7 ? "Из собственных средств"
                               : ss.GrantType == -6 ? "Трехсторонняя форма обучения" : null,
                Iic            = si != null ? si.Iic : null,
                UpdateDate     = si != null ? si.UpdateDate : null
            };

        return await query.Distinct().ToListAsync(ct);
    }
}
