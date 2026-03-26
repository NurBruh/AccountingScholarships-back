namespace AccountingScholarships.Domain.DTO.University;

public class Edu_SpecializationsDto
{
    public int Id { get; set; }
    public string? TitleRu { get; set; }
    public string? TitleKz { get; set; }
    public string? TitleEn { get; set; }
    public string? ShortTitleRu { get; set; }
    public string? ShortTitleKz { get; set; }
    public string? ShortTitleEn { get; set; }
    public string? DescriptionRu { get; set; }
    public string? DescriptionKz { get; set; }
    public string? DescriptionEn { get; set; }
    public int? EducationalProgramType { get; set; }
    public int? EducationalProgramStatus { get; set; }
    public bool? IsEducationalProgram { get; set; }
    public string? Code { get; set; }
    public int? LevelId { get; set; }
    public int? RupEditorOrgUnitId { get; set; }
    public int? ChairId { get; set; }
    public int? Classifier { get; set; }
    public int? ESUVOID { get; set; }
    public string? NoBDID { get; set; }
}
