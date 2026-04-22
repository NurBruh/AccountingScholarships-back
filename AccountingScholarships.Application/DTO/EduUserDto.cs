namespace AccountingScholarships.Application.DTO;

public class EduUserDto
{
    public int ID { get; set; }
    public string LastName { get; set; } = string.Empty;
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string ShortName { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? PersonalEmail { get; set; }
    public string? IIN { get; set; }
    public DateOnly? DOB { get; set; }
    public bool? Male { get; set; }
    public bool Resident { get; set; }
    public string? HomePhone { get; set; }
    public string? MobilePhone { get; set; }
    public string? PlaceOfBirth { get; set; }

    // Resolved from FK
    public string? Nationality { get; set; }
    public string? MaritalStatus { get; set; }
    public string? CitizenshipCountry { get; set; }
    public string? CitizenCategory { get; set; }
    public string? MessengerType { get; set; }
}
