namespace AccountingScholarships.Domain.DTO.University;

public class Edu_UsersDto
{
    public int ID { get; set; }
    public string? LastName { get; set; }
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string? Email { get; set; }
    public string? PersonalEmail { get; set; }
    public DateOnly? DOB { get; set; }
    public string? PlaceOfBirth { get; set; }
    public bool? Male { get; set; }
    public string? HomePhone { get; set; }
    public string? MobilePhone { get; set; }
    public string? IIN { get; set; }
    public int? ESUVOID { get; set; }
    public bool Resident { get; set; }
    public int? NationalityID { get; set; }
    public int? MaritalStatusID { get; set; }
    public int? MessengerTypeID { get; set; }
    public int? CitizenshipCountryID { get; set; }
    public int? CitizenCategoryID { get; set; }

    public SimpleRefDto? Nationality { get; set; }
    public SimpleRefDto? MaritalStatus { get; set; }
    public SimpleRefDto? MessengerType { get; set; }
    public SimpleRefDto? CitizenshipCountry { get; set; }
    public SimpleRefDto? CitizenCategory { get; set; }

    public class SimpleRefDto
    {
        public int ID { get; set; }
        public string? Title { get; set; }
    }
}
