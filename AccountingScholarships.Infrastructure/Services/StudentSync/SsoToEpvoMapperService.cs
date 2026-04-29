using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AccountingScholarships.Domain.Entities.Real.epvosso;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AccountingScholarships.Infrastructure.Services.StudentSync;

public class SsoToEpvoMapperService : ISsoToEpvoMapperService
{
    private readonly SsoDbContext _ssoContext;
    private readonly EpvoSsoDbContext _epvoContext;

    public SsoToEpvoMapperService(SsoDbContext ssoContext, EpvoSsoDbContext epvoContext)
    {
        _ssoContext = ssoContext;
        _epvoContext = epvoContext;
    }

    public async Task<List<Student_Temp>> MapStudentsAsync(List<string> iinPlts, CancellationToken ct = default)
    {
        var result = new List<Student_Temp>();

        if (iinPlts == null || !iinPlts.Any())
            return result;

        var iinList = iinPlts.Where(x => !string.IsNullOrWhiteSpace(x)).ToList();

        // 1. Fetch Students from SSO with all required includes
        var students = await _ssoContext.Edu_Students
            .Include(s => s.User)
                .ThenInclude(u => u.Addresses)
            .Include(s => s.User)
                .ThenInclude(u => u.Documents)
            .Include(s => s.User)
                .ThenInclude(u => u.Education)
            .Include(s => s.Rup)
            .Include(s => s.EducationPaymentType)
            .Include(s => s.Speciality)
            .Where(s => s.User != null && iinList.Contains(s.User.IIN) && s.StatusID != 2 && s.CategoryID == 1)
            .ToListAsync(ct);

        if (!students.Any())
            return result;

        // 2. Fetch required reference data from EPVO
        var epvoNationalities = await _epvoContext.Nationalities.AsNoTracking().ToListAsync(ct);
        var epvoCountries = await _epvoContext.Center_Countries.AsNoTracking().ToListAsync(ct);
        var epvoKatos = await _epvoContext.Center_Kato.AsNoTracking().ToListAsync(ct);
        var epvoFaculties = await _epvoContext.Faculties.AsNoTracking().ToListAsync(ct);

        // 3. Additional references from SSO (lazy loaded into dictionaries if needed)
        var studentIds = students.Select(s => s.StudentID).ToList();

        var creditsDict = await _ssoContext.Edu_StudentCourses
            .Where(sc => studentIds.Contains(sc.StudentID))
            .Join(_ssoContext.Edu_SemesterCourses, 
                  stc => stc.SemesterCourseID, 
                  sc => sc.ID, 
                  (stc, sc) => new { stc.StudentID, sc.EctsCredits })
            .GroupBy(x => x.StudentID)
            .Select(g => new { StudentID = g.Key, EctsCredits = g.Sum(x => (float?)x.EctsCredits) })
            .ToDictionaryAsync(x => x.StudentID, x => x.EctsCredits, ct);

        // Map each student
        foreach (var stud in students)
        {
            var u = stud.User;
            var r = stud.Rup;
            var spec = stud.Speciality;
            var ept = stud.EducationPaymentType;

            var addr1 = u.Addresses?.OrderByDescending(a => a.ID).FirstOrDefault(a => a.AddressTypeID == 1);
            var addr2 = u.Addresses?.OrderByDescending(a => a.ID).FirstOrDefault(a => a.AddressTypeID == 2);
            
            var attest = u.Education?.OrderByDescending(e => e.IssuedOn).FirstOrDefault(e => e.DocumentTypeID == 1);
            var diplom = u.Education?.OrderByDescending(e => e.DocumentTypeID).ThenByDescending(e => e.IssuedOn).FirstOrDefault(e => e.DocumentTypeID == 2 || e.DocumentTypeID == 3);
            
            var prevDoc = u.Education?.OrderByDescending(e => e.DocumentTypeID).FirstOrDefault();
            var udl = u.Documents?.OrderBy(d => d.DocumentTypeID).ThenByDescending(d => d.IssuedOn)
                        .FirstOrDefault(d => d.DocumentTypeID == 1 || d.DocumentTypeID == 2 || d.DocumentTypeID == 3 || d.DocumentTypeID == 4);
            
            var grants = u.Documents?.OrderByDescending(d => d.DocumentTypeID).ThenByDescending(d => d.IssuedOn).FirstOrDefault(d => d.DocumentTypeID == 16);

            var temp = new Student_Temp
            {
                UniversityId = 29,
                StudentId = u.ESUVOID ?? (60000000 + stud.StudentID),
                FirstName = u.FirstName,
                LastName = u.LastName,
                Patronymic = u.MiddleName,
                BirthDate = u.DOB,
                StartDate = stud.EntryDate ?? (stud.Year > 1 ? new DateOnly(2022, 8, 22) : new DateOnly(2023, 8, 22)),
                Address = addr1 != null ? $"{addr1.LocalityText}, {addr1.AddressText}" : "N/A",
                NationId = u.NationalityID,
                StudyFormId = CalculateStudyForm(spec?.LevelID, stud.EducationTypeID, r?.SemesterCount),
                PaymentFormId = stud.EducationPaymentTypeID == 1 ? 2 : 1,
                StudyLanguageId = stud.StudyLanguageID,
                Photo = null,
                ProfessionId = spec?.ESUVOID ?? 0, 
                CourseNumber = stud.Year,
                IsMarried = u.MaritalStatusID == 1 ? 1 : 2,
                IcNumber = (u.IIN?.Length == 12) ? (udl?.Number ?? "N/A") : u.IIN,
                IcDate = udl?.IssuedOn ?? new DateOnly(2023, 1, 1),
                Education = "NA",
                HasExcellent = (attest?.DocumentSubTypeID == 1 || attest?.DocumentSubTypeID == 2 || attest?.DocumentSubTypeID == 4),
                StartOrder = "NA",
                IsStudent = stud.StatusID == 2 ? 3 : 1,
                Certificate = "NA",
                GrantNumber = grants?.Number,
                Gpa = (decimal?)stud.GPA ?? 0,
                CurrentCreditsSum = creditsDict.GetValueOrDefault(stud.StudentID) ?? 0,
                Residence = 1, // Simplified
                SitizenshipId = 113, // Default KZ
                DormState = stud.NeedsDorm ? 3 : 1,
                IsInRetire = (stud.StatusID == 4 || stud.StatusID == 5),
                FromId = null,
                Local = false,
                City = addr1?.LocalityText ?? "г.Алматы",
                SpecializationId = spec?.ESUVOID, 
                IinPlt = u.IIN?.Length == 12 ? u.IIN : null,
                AltynBelgi = stud.AltynBelgi,
                DataVydachiAttestata = attest?.IssuedOn != null ? DateOnly.FromDateTime(attest.IssuedOn.Value) : null,
                DataVydachiDiploma = diplom?.IssuedOn != null ? DateOnly.FromDateTime(diplom.IssuedOn.Value) : null,
                EndCollege = false,
                EndHighSchool = (prevDoc?.DocumentTypeID == 2 || prevDoc?.DocumentTypeID == 3),
                EndSchool = (prevDoc?.DocumentTypeID == 1),
                IcType = (u.IIN?.Length == 12) ? 1 : 2,
                LivingAddress = addr2 != null ? $"{addr2.LocalityText}, {addr2.AddressText}" : null,
                NomerAttestata = attest?.Number,
                OtherBirthPlace = u.PlaceOfBirth,
                SeriyaAttestata = attest?.Series,
                SexId = u.Male == true ? 2 : 1,
                Mail = u.PersonalEmail ?? u.Email,
                Phone = u.HomePhone,
                EnrollOrderDate = stud.EntryDate,
                MobilePhone = u.MobilePhone,
                GrantType = stud.GrantTypeID,
                IncorrectIin = u.IIN?.Length == 12,
                FundingId = stud.FundingID,
                TypeCode = "STUDENT"
            };

            result.Add(temp);
        }

        return result;
    }

    private int CalculateStudyForm(int? levelId, int? eduTypeId, int? semesterCount)
    {
        if (levelId == 1 && eduTypeId == 1 && (semesterCount == 8 || semesterCount == 7)) return 1;
        if (levelId == 3) return 2;
        if (levelId == 2 && semesterCount >= 3) return 3;
        if (levelId == 1 && eduTypeId == 2 && (semesterCount == 5 || semesterCount == 6)) return 4;
        if (levelId == 2 && semesterCount < 3) return 5;
        if (levelId == 1 && eduTypeId == 1 && (semesterCount == 5 || semesterCount == 6)) return 6;
        if (levelId == 1 && eduTypeId == 1 && (semesterCount == 10 || semesterCount == 9)) return 7;
        if (levelId == 1 && eduTypeId == 2 && (semesterCount == 4 || semesterCount == 3)) return 8;
        if (levelId == 1 && eduTypeId == 2 && semesterCount == 7) return 10;
        return 1;
    }
}
