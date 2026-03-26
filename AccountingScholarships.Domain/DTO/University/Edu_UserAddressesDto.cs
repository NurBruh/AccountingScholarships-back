namespace AccountingScholarships.Domain.DTO.University;

public class Edu_UserAddressesDto
{
    public int ID { get; set; }
    public int UserID { get; set; }
    public int AddressTypeID { get; set; }
    public int CountryID { get; set; }
    public int LocalityID { get; set; }
    public string? LocalityText { get; set; }
    public string? AddressText { get; set; }
    public string? Region { get; set; }
    public string? Area { get; set; }
    public string? AddressTextEN { get; set; }
    public UserRefDto? User { get; set; }
    public SimpleRefDto? AddressType { get; set; }
    public SimpleRefDto? Country { get; set; }
    public SimpleRefDto? Locality { get; set; }

    public class UserRefDto
    {
        public int ID { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
    }

    public class SimpleRefDto
    {
        public int ID { get; set; }
        public string? Title { get; set; }
    }
}
