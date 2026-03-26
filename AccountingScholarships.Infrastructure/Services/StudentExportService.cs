using AccountingScholarships.Domain.DTO.Testing;
using AccountingScholarships.Domain.Interfaces;
using AccountingScholarships.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AccountingScholarships.Infrastructure.Services;

/// <summary>
/// Сервис экспорта данных студентов в формат ЕСУ|ВО
/// Реализует сложный SQL запрос аналогичный хранимой процедуре Reload_STUDENT
/// </summary>
public class StudentExportService : IStudentExportService
{
    private readonly SsoDbContext _context;

    public StudentExportService(SsoDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<StudentEsuvoExportDto>> GetStudentsForExportAsync(CancellationToken cancellationToken = default)
    {
        var sql = GetExportSql();
        
        var result = await _context.Database
            .SqlQueryRaw<StudentEsuvoExportDto>(sql)
            .ToListAsync(cancellationToken);

        return result;
    }

    private static string GetExportSql()
    {
        return @"
;WITH addr1CTE AS(
  SELECT TOP 1 WITH TIES
    UserID,
    CONCAT(cnt.Title,+', '+reg.Title,+', '+locArea.Title,', '+loc.Title,', '+addr.LocalityText,', '+addr.AddressText) FullAddress, 
    cnt.ESUVOCitizenshipCountryID, loc.ESUVOCenterKatoCode,
    loc.TypeID,
    reg.Title as Oblast,
    locArea.Title as Raion,
    locAreaS.Title as Area,
    addr.AddressText as Ulica
  FROM dbo.Edu_UserAddresses addr
  LEFT JOIN dbo.Edu_Localities loc ON addr.LocalityID=loc.ID
  LEFT JOIN dbo.Obsolete_Edu_Regions reg ON addr.Region=reg.ID
  LEFT JOIN dbo.Edu_Localities locArea ON loc.ParentID=locArea.ID
  LEFT JOIN dbo.Edu_Countries cnt ON addr.CountryID=cnt.ID
  LEFT JOIN dbo.Edu_Localities locAreaS ON addr.Area=locAreaS.ID
  WHERE addr.AddressTypeID=1
  ORDER BY ROW_NUMBER()OVER(PARTITION BY addr.UserID ORDER BY addr.ID DESC)
),
addr2CTE AS(
  SELECT TOP 1 WITH TIES
    UserID,
    CONCAT(cnt.Title,', '+loc.Title,', '+addr.LocalityText,', '+addr.AddressText) FullAddress, 
    loc.ESUVOCenterKatoCode
  FROM dbo.Edu_UserAddresses addr
  LEFT JOIN dbo.Edu_Localities loc ON addr.LocalityID=loc.ID
  LEFT JOIN dbo.Edu_Countries cnt ON addr.CountryID=cnt.ID
  WHERE addr.AddressTypeID=2
  ORDER BY ROW_NUMBER()OVER(PARTITION BY addr.UserID ORDER BY addr.ID DESC)
),
attestCTE AS(
  SELECT TOP 1 WITH TIES
    UserID, Number, IssuedOn, Series, DocumentSubTypeID
  FROM dbo.Edu_UserEducation
  WHERE DocumentTypeID=1
  ORDER BY ROW_NUMBER()OVER(PARTITION BY UserID ORDER BY IssuedOn DESC)
),
diplomCTE AS(
  SELECT TOP 1 WITH TIES
    UserID, Number, IssuedOn, DocumentSubTypeID
  FROM dbo.Edu_UserEducation
  WHERE DocumentTypeID IN(2,3)
  ORDER BY ROW_NUMBER()OVER(PARTITION BY UserID ORDER BY DocumentTypeID DESC,IssuedOn DESC)
),
grantCTE AS(
  SELECT TOP 1 WITH TIES
    UserID, Number
  FROM dbo.Edu_UserDocuments
  WHERE DocumentTypeID = 16
  ORDER BY ROW_NUMBER()OVER(PARTITION BY UserID ORDER BY DocumentTypeID DESC,IssuedOn DESC)
),
udlCTE AS(
  SELECT TOP 1 WITH TIES
    d.UserID, d.Number, d.IssuedOn,
    CASE d.DocumentTypeID
      WHEN 1 THEN 1
      WHEN 2 THEN 2
      WHEN 3 THEN 5
      WHEN 4 THEN 3
    END ESUVO_DocumentTypeID,
    ISNULL(d.IssuedByText,o.Title) IssuedByText
  FROM dbo.Edu_UserDocuments d
  LEFT JOIN dbo.Edu_DocumentIssueOrgs o ON d.IssuedByID=o.ID
  WHERE d.DocumentTypeID IN(1,2,3,4)
  ORDER BY ROW_NUMBER()OVER(PARTITION BY UserID ORDER BY DocumentTypeID,IssuedOn DESC)
),
prevDocCTE AS(
  SELECT TOP 1 WITH TIES 
    d.UserID, 
    CONCAT(dt.Title,N' '+d.Number,N' от '+FORMAT(d.IssuedOn,'dd.MM.yyyy')) DocNumber,
    d.DocumentTypeID
  FROM dbo.Edu_UserEducation d
  JOIN dbo.Edu_EducationDocumentTypes dt ON d.DocumentTypeID=dt.ID
  ORDER BY ROW_NUMBER()OVER(PARTITION BY d.UserID ORDER BY d.DocumentTypeID DESC)
),
studentCreditsSum AS(
  SELECT stc.StudentID, SUM(sc.EctsCredits) as EctsCredits
  FROM dbo.Edu_StudentCourses stc
  JOIN dbo.Edu_SemesterCourses sc on sc.ID = stc.SemesterCourseID
  GROUP BY stc.StudentID
)
SELECT 
  29 AS UniversityId,
  ISNULL(u.esuvoid, 60000000 + stud.StudentID) AS StudentId,
  u.FirstName AS FirstName,
  u.LastName AS LastName,
  u.MiddleName AS Patronymic,
  u.DOB AS BirthDate,
  ISNULL(stud.EntryDate, IIF(stud.Year > 1,'2022-08-22','2023-08-22')) AS StartDate,
  ISNULL(addr1.FullAddress, N'N/A') AS [Address],
  nat.center_nationalitiesid AS NationId,
  CASE 
    WHEN spec.LevelID=1 AND stud.EducationTypeID=1 AND (r.SemesterCount = 8 OR r.SemesterCount = 7) THEN 1
    WHEN spec.LevelID=3 THEN 2
    WHEN spec.LevelID=2 AND r.SemesterCount >= 3 THEN 3
    WHEN spec.LevelID=1 AND stud.EducationTypeID = 2 AND (r.SemesterCount = 5 OR r.SemesterCount = 6) THEN 4
    WHEN spec.LevelID=2 AND r.SemesterCount < 3 THEN 5
    WHEN spec.LevelID=1 AND stud.EducationTypeID = 1 AND (r.SemesterCount = 5 OR r.SemesterCount = 6) THEN 6
    WHEN spec.LevelID=1 AND stud.EducationTypeID = 1 AND (r.SemesterCount = 10 OR r.SemesterCount = 9) THEN 7
    WHEN spec.LevelID=1 AND stud.EducationTypeID = 2 AND (r.SemesterCount = 4 OR r.SemesterCount = 3) THEN 8
    WHEN spec.LevelID=1 AND stud.EducationTypeID = 2 AND r.SemesterCount = 7 THEN 10
    ELSE 1
  END AS StudyFormId,
  IIF(stud.EducationPaymentTypeID=1,2,1) AS PaymentFormId,
  ISNULL((SELECT l.center_studylang_id FROM studylanguages l WHERE id = stud.StudyLanguageID),1) AS StudyLanguageId,
  NULL AS Photo,
  IIF(spec.Classifier = 1, specz.ESUVOID, spec.esuvoid) AS ProfessionId,
  stud.Year AS CourseNumber,
  NULL AS TranscriptNumber,
  NULL AS TranscriptSeries,
  IIF(u.MaritalStatusID=1,1,2) AS IsMarried,
  IIF(u.IIN LIKE N'[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]',ISNULL(udl.Number,N'N/A'),u.IIN) AS IcNumber,
  ISNULL(udl.IssuedOn, '2023-01-01') AS IcDate,
  N'NA' AS Education,
  ISNULL(IIF(attest.DocumentSubTypeID IN (1,2,4),1,0),0) AS HasExcellent,
  N'NA' AS StartOrder,
  CASE WHEN stud.StatusID=2 THEN 3 ELSE 1 END AS IsStudent,
  N'NA' AS Certificate,
  grants.Number AS GrantNumber,
  ISNULL(stud.GPA,0) AS Gpa,
  ISNULL(CredSum.EctsCredits,0) AS CurrentCreditsSum,
  IIF(addr1.TypeID = 3,1,ISNULL(addr1.TypeID,2)) AS Residence,
  ISNULL(Epvo_Countr.id,113) AS SitizenshipId,
  IIF(stud.NeedsDorm=1,3,1) AS DormState,
  IIF(stud.StatusID IN (4,5),1,0) AS IsInRetire,
  countr.ESUVOCitizenshipCountryID AS FromId,
  IIF(addr1.FullAddress LIKE N'%алматы%',1,0) AS [Local],
  addr1.FullAddress AS City,
  NULL AS ContractId,
  IIF(spec.Classifier = 1, ISNULL(spec.EsuvoID, spc.ID), spc.ID) AS SpecializationId,
  IIF(u.IIN LIKE N'[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]',u.IIN,NULL) AS IinPlt,
  stud.AltynBelgi AS AltynBelgi,
  attest.IssuedOn AS DataVydachiAttestata,
  diplom.IssuedOn AS DataVydachiDiploma,
  NULL AS DateDocEducation,
  0 AS EndCollege,
  IIF(prevDoc.DocumentTypeID IN(2,3),1,0) AS EndHighSchool,
  IIF(prevDoc.DocumentTypeID=1,1,0) AS EndSchool,
  NULL AS IcSeries,
  IIF(u.IIN LIKE N'[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]',ISNULL(udl.ESUVO_DocumentTypeID,1),2) AS IcType,
  addr2.FullAddress AS LivingAddress,
  attest.Number AS NomerAttestata,
  u.PlaceOfBirth AS OtherBirthPlace,
  NULL AS SeriesNumberDocEducation,
  attest.Series AS SeriyaAttestata,
  diplom.Number AS SeriyaDiploma,
  NULL AS SchoolName,
  ISNULL(fuc.facultyid, ouFuc.ID) AS FacultyId,
  CASE u.Male WHEN 1 THEN 2 WHEN 0 THEN 1 END AS SexId,
  ISNULL(u.PersonalEmail, u.Email) AS Mail,
  u.HomePhone AS Phone,
  NULL AS SumPoints,
  NULL AS SumPointsCreative,
  ISNULL(stud.EntryDate, entr.RegisteredOn) AS EnrollOrderDate,
  u.MobilePhone AS MobilePhone,
  ISNULL(IIF(ept.ESUVOGrantTypeId = 1, gtype.ESUVOGrantTypeId, ept.ESUVOGrantTypeId),0) AS GrantType,
  NULL AS AcademicMobility,
  IIF(u.IIN LIKE N'[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]',1,0) AS IncorrectIin,
  kato.ESUVOCenterKatoCode AS BirthPlaceCatoId,
  ck.id AS LivingPlaceCatoId,
  ckReg.id AS RegistrationPlaceCatoId,
  NULL AS NaselPunktAttestataCatoId,
  stud.fundingId AS FundingId,
  'STUDENT' AS TypeCode
FROM dbo.Edu_Students stud
LEFT JOIN dbo.Edu_Users u ON stud.StudentID=u.ID
JOIN dbo.Edu_Rups r ON stud.RupID=r.ID
LEFT JOIN studentCreditsSum CredSum ON CredSum.StudentID = stud.StudentID
LEFT JOIN dbo.Edu_EducationPaymentTypes ept ON stud.EducationPaymentTypeID = ept.ID
LEFT JOIN dbo.Edu_Specialities spec ON spec.ID=stud.SpecialityID
LEFT JOIN dbo.Edu_OrgUnits ou ON ou.id = spec.RupEditorOrgUnitID
LEFT JOIN dbo.Edu_OrgUnits ouFuc ON ouFuc.ID = ou.ParentID AND ouFuc.TypeID = 2
LEFT JOIN dbo.faculties fuc ON fuc.facultyid = ouFuc.ID
OUTER APPLY (
  SELECT TOP 1 sss.SpecializationId, sss.SpecialityId 
  FROM dbo.Edu_SpecialitySpecializations sss 
  WHERE sss.SpecializationId = spec.ID AND sss.SpecialityId < 398
) spz
OUTER APPLY (
  SELECT TOP 1 sss.ESUVOID 
  FROM dbo.Edu_Specializations sss 
  WHERE sss.Id = spz.SpecialityId 
  ORDER BY sss.ESUVOID
) specz
LEFT JOIN addr1CTE addr1 ON stud.StudentID=addr1.UserID
LEFT JOIN addr2CTE addr2 ON stud.StudentID=addr2.UserID
LEFT JOIN attestCTE attest ON stud.StudentID=attest.UserID
LEFT JOIN diplomCTE diplom ON stud.StudentID=diplom.UserID
LEFT JOIN grantCTE grants ON stud.StudentID=grants.UserID
LEFT JOIN dbo.Edu_GrantTypes gtype ON stud.GrantTypeID = gtype.ID
LEFT JOIN udlCTE udl ON stud.StudentID=udl.UserID
OUTER APPLY (
  SELECT TOP 1 * FROM dbo.Edu_Localities WHERE Title = u.PlaceOfBirth
) kato
LEFT JOIN prevDocCTE prevDoc ON prevDoc.UserID=stud.StudentID
LEFT JOIN dbo.Edu_Entrants entr ON entr.EntrantID=stud.StudentID AND entr.StatusID IN(3,4)
LEFT JOIN dbo.Edu_Countries countr ON countr.ID = u.CitizenshipCountryID
LEFT JOIN dbo.Edu_Countries cnt ON u.CitizenshipCountryID=cnt.ID
LEFT JOIN dbo.center_countries Epvo_Countr ON Epvo_Countr.country_code = cnt.ESUVOCitizenshipCountryID
LEFT JOIN dbo.nationalities nat ON nat.id = u.NationalityID
OUTER APPLY (
  SELECT TOP 1 * FROM dbo.SPECIALITIES_EPVO_2025 
  WHERE specializationCode COLLATE SQL_Latin1_General_CP1_CI_AS = spec.Code COLLATE SQL_Latin1_General_CP1_CI_AS
) spc
LEFT JOIN dbo.CENTER_KATO ck ON ck.code COLLATE SQL_Latin1_General_CP1_CI_AS = addr2.ESUVOCenterKatoCode COLLATE SQL_Latin1_General_CP1_CI_AS
LEFT JOIN dbo.CENTER_KATO ckReg ON ckReg.code COLLATE SQL_Latin1_General_CP1_CI_AS = addr1.ESUVOCenterKatoCode COLLATE SQL_Latin1_General_CP1_CI_AS
WHERE stud.StatusID != 2 
  AND stud.CategoryID = 1
  AND stud.StatusID IS NOT NULL
  AND u.DOB IS NOT NULL
";
    }
}
