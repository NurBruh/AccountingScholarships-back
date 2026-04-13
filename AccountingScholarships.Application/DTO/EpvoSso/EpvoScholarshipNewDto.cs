namespace AccountingScholarships.Application.DTO.EpvoSso;

public class EpvoScholarshipNewDto
{
    public int UniversityId { get; set; }
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int ScholarshipYear { get; set; }
    public int ScholarshipMonth { get; set; }
    public DateOnly? ScholarshipPayDate { get; set; }
    public double? ScholarshipMoney { get; set; }
    public int? ScholarshipTypeId { get; set; }
    public DateOnly? TerminationDate { get; set; }
    public bool? AdditionalPayment { get; set; }
    public int SectionId { get; set; }
    public int ScholarshipAwardYear { get; set; }
    public int ScholarshipAwardTerm { get; set; }
    public int OverallPerformance { get; set; }
    public string? TypeCode { get; set; }
}
