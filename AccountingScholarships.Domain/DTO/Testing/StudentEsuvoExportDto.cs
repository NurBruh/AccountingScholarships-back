namespace AccountingScholarships.Domain.DTO.Testing;

/// <summary>
/// DTO для экспорта данных студентов в формат ЕСУ|ВО
/// Соответствует структуре таблицы STUDENT_SSO
/// </summary>
public class StudentEsuvoExportDto
{
    public int UniversityId { get; set; } = 29; // КазНИТУ
    public long StudentId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Patronymic { get; set; }
    public DateTime? BirthDate { get; set; }
    public DateTime? StartDate { get; set; }
    public string? Address { get; set; }
    public int? NationId { get; set; }
    public int? StudyFormId { get; set; }
    public int? PaymentFormId { get; set; }
    public int? StudyLanguageId { get; set; }
    public string? Photo { get; set; }
    public int? ProfessionId { get; set; }
    public int? CourseNumber { get; set; }
    public string? TranscriptNumber { get; set; }
    public string? TranscriptSeries { get; set; }
    public int? IsMarried { get; set; }
    public string? IcNumber { get; set; }
    public DateTime? IcDate { get; set; }
    public string? Education { get; set; }
    public int? HasExcellent { get; set; }
    public string? StartOrder { get; set; }
    public int? IsStudent { get; set; }
    public string? Certificate { get; set; }
    public string? GrantNumber { get; set; }
    public decimal? Gpa { get; set; }
    public decimal? CurrentCreditsSum { get; set; }
    public int? Residence { get; set; }
    public int? SitizenshipId { get; set; }
    public int? DormState { get; set; }
    public int? IsInRetire { get; set; }
    public int? FromId { get; set; }
    public int? Local { get; set; }
    public string? City { get; set; }
    public int? ContractId { get; set; }
    public int? SpecializationId { get; set; }
    public string? IinPlt { get; set; }
    public bool? AltynBelgi { get; set; }
    public DateTime? DataVydachiAttestata { get; set; }
    public DateTime? DataVydachiDiploma { get; set; }
    public DateTime? DateDocEducation { get; set; }
    public int? EndCollege { get; set; }
    public int? EndHighSchool { get; set; }
    public int? EndSchool { get; set; }
    public string? IcSeries { get; set; }
    public int? IcType { get; set; }
    public string? LivingAddress { get; set; }
    public string? NomerAttestata { get; set; }
    public string? OtherBirthPlace { get; set; }
    public string? SeriesNumberDocEducation { get; set; }
    public string? SeriyaAttestata { get; set; }
    public string? SeriyaDiploma { get; set; }
    public string? SchoolName { get; set; }
    public int? FacultyId { get; set; }
    public int? SexId { get; set; }
    public string? Mail { get; set; }
    public string? Phone { get; set; }
    public int? SumPoints { get; set; }
    public int? SumPointsCreative { get; set; }
    public DateTime? EnrollOrderDate { get; set; }
    public string? MobilePhone { get; set; }
    public int? GrantType { get; set; }
    public int? AcademicMobility { get; set; }
    public int? IncorrectIin { get; set; }
    public string? BirthPlaceCatoId { get; set; }
    public int? LivingPlaceCatoId { get; set; }
    public int? RegistrationPlaceCatoId { get; set; }
    public int? NaselPunktAttestataCatoId { get; set; }
    public int? FundingId { get; set; }
    public string TypeCode { get; set; } = "STUDENT";
}
