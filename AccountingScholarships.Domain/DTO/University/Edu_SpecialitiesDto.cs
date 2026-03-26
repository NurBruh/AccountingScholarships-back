namespace AccountingScholarships.Domain.DTO.University;

public class Edu_SpecialitiesDto
{
    public int ID { get; set; }
    public string? Code { get; set; }
    public string? Title { get; set; }
    public int? YearsOfStudy { get; set; }
    public bool? Diaspora { get; set; }
    public bool? VillageQuota { get; set; }
    public bool Deleted { get; set; }
    public string? ShortTitle { get; set; }
    public string? Description { get; set; }
    public int? ESUVOID { get; set; }
    public bool? Classifier { get; set; }
    public int? EducationalProgramStatus { get; set; }
    public int? EducationalProgramType { get; set; }
    public int? Classifier2 { get; set; }
    public string? NoBDID { get; set; }
    public bool ReadyToSendESUVO { get; set; }
    public bool? DoubleDiploma { get; set; }
    public bool? Jointep { get; set; }
    public bool? Is_interdisciplinary { get; set; }
    public bool? is_not_active { get; set; }
    public decimal? EducationDuration { get; set; }
    public bool? isPrikladnoy { get; set; }
    public int LevelID { get; set; }
    public int? PrimarySubjectID { get; set; }
    public int? FithSubjectID { get; set; }
    public int? RupEditorOrgUnitID { get; set; }

    public Edu_SpecialityLevelsDto? Level { get; set; }
    public SchoolSubjectRefDto? PrimarySubject { get; set; }
    public SchoolSubjectRefDto? FithSubject { get; set; }
    public OrgUnitRefDto? RupEditorOrgUnit { get; set; }

    public class SchoolSubjectRefDto
    {
        public int ID { get; set; }
        public string? Title { get; set; }
        public string? Number { get; set; }
        public bool? IsRequired { get; set; }
    }

    public class OrgUnitRefDto
    {
        public int ID { get; set; }
        public string? Title { get; set; }
    }
}
