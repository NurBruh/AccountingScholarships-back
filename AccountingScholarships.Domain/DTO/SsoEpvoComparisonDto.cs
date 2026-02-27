namespace AccountingScholarships.Domain.DTO;

public class SsoEpvoComparisonDto
{
    public IList<SsoEpvoComparisonItemDto> Items { get; set; } = new List<SsoEpvoComparisonItemDto>();
    public int TotalDifferences { get; set; }
    public int OnlyInSso { get; set; }
    public int OnlyInEpvo { get; set; }
}

public class SsoEpvoComparisonItemDto
{
    public string IIN { get; set; } = string.Empty;
    public StudentSsoDataDto? SsoData { get; set; }
    public StudentEpvoDataDto? EpvoData { get; set; }
    public IList<FieldDifferenceDto> Differences { get; set; } = new List<FieldDifferenceDto>();
    public bool HasDifferences { get; set; }
    public bool OnlyInSso { get; set; }
    public bool OnlyInEpvo { get; set; }
}

public class StudentSsoDataDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? MiddleName { get; set; }
    public string IIN { get; set; } = string.Empty;
    public string? Faculty { get; set; }
    public string? Speciality { get; set; }
    public int Course { get; set; }
    public string? GrantName { get; set; }
    public decimal? GrantAmount { get; set; }
    public string? ScholarshipName { get; set; }
    public decimal? ScholarshipAmount { get; set; }
    public string? ScholarshipNotes { get; set; }
    public string Iban { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}

public class StudentEpvoDataDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? MiddleName { get; set; }
    public string IIN { get; set; } = string.Empty;
    public string? Faculty { get; set; }
    public string? Speciality { get; set; }
    public int Course { get; set; }
    public string? GrantName { get; set; }
    public decimal? GrantAmount { get; set; }
    public string? ScholarshipName { get; set; }
    public decimal? ScholarshipAmount { get; set; }
    public string? ScholarshipNotes { get; set; }
    public string Iban { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTime SyncDate { get; set; }
}

public class FieldDifferenceDto
{
    public string Field { get; set; } = string.Empty;
    public string FieldLabel { get; set; } = string.Empty;
    public string? SsoValue { get; set; }
    public string? EpvoValue { get; set; }
}
