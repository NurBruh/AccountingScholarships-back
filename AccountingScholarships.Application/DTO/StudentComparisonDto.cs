namespace AccountingScholarships.Application.DTO;

public class StudentComparisonDto
{
    // Идентификатор
    public int StudentId { get; set; }
    public string? IIN { get; set; }

    // ССО данные (трансформированные под ЕПВО формат)
    public string? Sso_FullName { get; set; }
    public int? Sso_CourseNumber { get; set; }
    public string? Sso_StudyForm { get; set; }
    public string? Sso_Institute { get; set; }
    public string? Sso_Cafedra { get; set; }
    public string? Sso_Speciality { get; set; }
    public string? Sso_PaymentType { get; set; }
    public string? Sso_GrantType { get; set; }
    public string? Sso_Iic { get; set; }
    public DateTime? Sso_UpdatedDate { get; set; }

    // ЕПВО данные
    public string? Epvo_FullName { get; set; }
    public int? Epvo_CourseNumber { get; set; }
    public string? Epvo_StudyForm { get; set; }
    public string? Epvo_FacultyName { get; set; }
    public string? Epvo_Specialization { get; set; }
    public string? Epvo_PaymentType { get; set; }
    public string? Epvo_GrantType { get; set; }
    public string? Epvo_Iic { get; set; }
    public DateOnly? Epvo_UpdateDate { get; set; }

    // Статус расхождения
    public bool HasDifference { get; set; }
    public List<string> DifferentFields { get; set; } = new();
}

public class StudentComparisonPagedDto
{
    public IList<StudentComparisonDto> Items { get; set; } = new List<StudentComparisonDto>();
    public int TotalItems { get; set; }
    public int WithDifferences { get; set; }
    public int OnlyInSso { get; set; }
    public int OnlyInEpvo { get; set; }
    public int Matching { get; set; }
    public int FilteredCount { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
}
