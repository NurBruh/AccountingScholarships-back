namespace AccountingScholarships.Domain.DTO.EpvoSso;

public class EpvoStudyFormsDto
{
    public int? Id { get; set; }
    public int? UniversityId { get; set; }
    public int? DegreeId { get; set; }
    public string? NameRu { get; set; }
    public string? NameKz { get; set; }
    public string? NameEn { get; set; }
    public string? CourseCount { get; set; }
    public string? CreditsCount { get; set; }
    public string? TermsCount { get; set; }
    public int? DepartmentId { get; set; }
    public int? BaseEducationId { get; set; }
    public bool? DistanceLearning { get; set; }
    public int? TrainingCompletionTerm { get; set; }
    public bool? InUse { get; set; }
    public string? TypeCode { get; set; }
}
