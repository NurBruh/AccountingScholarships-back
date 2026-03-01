namespace AccountingScholarships.Domain.DTO;

public class ReferenceDataDto
{
    public List<InstituteDto> Institutes { get; set; } = new();
    public List<DepartmentDto> Departments { get; set; } = new();
    public List<SpecialityDto> Specialities { get; set; } = new();
    public List<StudyFormDto> StudyForms { get; set; } = new();
    public List<DegreeLevelDto> DegreeLevels { get; set; } = new();
    public List<BankDto> Banks { get; set; } = new();
    public List<ScholarshipTypeDto> ScholarshipTypes { get; set; } = new();
}

public class InstituteDto
{
    public int Id { get; set; }
    public string InstituteName { get; set; } = string.Empty;
    public string? InstituteDirector { get; set; }
}

public class DepartmentDto
{
    public int Id { get; set; }
    public string DepartmentName { get; set; } = string.Empty;
    public string? DepartmentHead { get; set; }
    public int InstituteId { get; set; }
    public string? InstituteName { get; set; }
}

public class SpecialityDto
{
    public int Id { get; set; }
    public string SpecialityName { get; set; } = string.Empty;
    public int DepartmentId { get; set; }
    public string? DepartmentName { get; set; }
    public int? InstituteId { get; set; }
    public string? InstituteName { get; set; }
}

public class StudyFormDto
{
    public int Id { get; set; }
    public string StudyFormName { get; set; } = string.Empty;
}

public class DegreeLevelDto
{
    public int Id { get; set; }
    public string DegreeName { get; set; } = string.Empty;
}

public class BankDto
{
    public int Id { get; set; }
    public string RecipientBank { get; set; } = string.Empty;
    public string Bic { get; set; } = string.Empty;
}

public class ScholarshipTypeDto
{
    public int Id { get; set; }
    public string ScholarshipName { get; set; } = string.Empty;
}
