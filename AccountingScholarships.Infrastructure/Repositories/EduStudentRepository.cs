using AccountingScholarships.Domain.DTO;
using AccountingScholarships.Domain.Entities.university;
using AccountingScholarships.Domain.Interfaces;
using AccountingScholarships.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AccountingScholarships.Infrastructure.Repositories;

public class EduStudentRepository : SsoRepository<EduStudents>, IEduStudentRepository
{
    public EduStudentRepository(SsoDbContext context) : base(context) { }

    public async Task<EduStudents?> GetWithDetailsAsync(int studentId, CancellationToken cancellationToken = default)
    {
        return await _context.EduStudents
            .Include(s => s.User)
                .ThenInclude(u => u!.Nationality)
            .Include(s => s.User)
                .ThenInclude(u => u!.CitizenshipCountry)
            .Include(s => s.EducationType)
            .Include(s => s.EducationPaymentType)
            .Include(s => s.GrantType)
            .Include(s => s.EducationDuration)
                .ThenInclude(d => d!.Level)
            .Include(s => s.StudyLanguage)
            .Include(s => s.AcademicStatus)
            .Include(s => s.Advisor)
                .ThenInclude(a => a!.User)
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.StudentID == studentId, cancellationToken);
    }

    public async Task<EduStudents?> GetByIINAsync(string iin, CancellationToken cancellationToken = default)
    {
        return await _context.EduStudents
            .Include(s => s.User)
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.User.IIN == iin, cancellationToken);
    }

    public async Task<IReadOnlyList<EduStudents>> GetAllWithDetailsAsync(CancellationToken cancellationToken = default)
    {
        return await _context.EduStudents
            .Include(s => s.User)
                .ThenInclude(u => u!.Nationality)
            .Include(s => s.User)
                .ThenInclude(u => u!.CitizenshipCountry)
            .Include(s => s.EducationType)
            .Include(s => s.EducationPaymentType)
            .Include(s => s.GrantType)
            .Include(s => s.EducationDuration)
            .Include(s => s.StudyLanguage)
            .Include(s => s.AcademicStatus)
            .AsSplitQuery()
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<EduStudentDto?> GetAsDtoAsync(int studentId, CancellationToken cancellationToken = default)
    {
        var s = await GetWithDetailsAsync(studentId, cancellationToken);
        return s is null ? null : MapToDto(s);
    }

    public async Task<IReadOnlyList<EduStudentDto>> GetAllAsDtoAsync(CancellationToken cancellationToken = default)
    {
        var students = await GetAllWithDetailsAsync(cancellationToken);
        return students.Select(MapToDto).ToList();
    }

    private static EduStudentDto MapToDto(EduStudents s)
    {
        var u = s.User;
        return new EduStudentDto
        {
            StudentID            = s.StudentID,
            LastName             = u?.LastName ?? string.Empty,
            FirstName            = u?.FirstName,
            MiddleName           = u?.MiddleName,
            FullName             = u is not null ? $"{u.LastName} {u.FirstName} {u.MiddleName}".Trim() : string.Empty,
            ShortName            = u?.ShortName ?? string.Empty,
            Email                = u?.Email,
            IIN                  = u?.IIN,
            DOB                  = u?.DOB,
            Male                 = u?.Male,
            Resident             = u?.Resident ?? false,
            MobilePhone          = u?.MobilePhone,

            Year                 = s.Year,
            GPA                  = s.GPA,
            GPA_Y                = s.GPA_Y,
            EctsGPA              = s.EctsGPA,
            EctsGPA_Y            = s.EctsGPA_Y,
            NeedsDorm            = s.NeedsDorm,
            AltynBelgi           = s.AltynBelgi,
            IsScholarship        = s.IsScholarship,
            IsKNB                = s.IsKNB,
            EntryDate            = s.EntryDate,
            GraduatedOn          = s.GraduatedOn,
            AcademicStatusEndsOn   = s.AcademicStatusEndsOn,
            AcademicStatusStartsOn = s.AcademicStatusStartsOn,

            ScholarshipOrderNumber = s.ScholarshipOrderNumber,
            ScholarshipOrderDate   = s.ScholarshipOrderDate,
            ScholarshipDateStart   = s.ScholarshipDateStart,
            ScholarshipDateEnd     = s.ScholarshipDateEnd,

            EducationType        = s.EducationType?.Title,
            EducationPaymentType = s.EducationPaymentType?.Title,
            GrantType            = s.GrantType?.Title,
            EducationDuration    = s.EducationDuration?.Title,
            StudyLanguage        = s.StudyLanguage?.Title,
            AcademicStatus       = s.AcademicStatus?.Title,
            AdvisorFullName      = s.Advisor?.User is { } adv
                                    ? $"{adv.LastName} {adv.FirstName} {adv.MiddleName}".Trim()
                                    : null,
            Nationality          = u?.Nationality?.Title,
            CitizenshipCountry   = u?.CitizenshipCountry?.Title,
        };
    }
}
