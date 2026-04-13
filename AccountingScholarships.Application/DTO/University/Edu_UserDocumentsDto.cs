namespace AccountingScholarships.Application.DTO.University;

public class Edu_UserDocumentsDto
{
    public int ID { get; set; }
    public int UserID { get; set; }
    public int DocumentTypeID { get; set; }
    public int? IssuedByID { get; set; }
    public string? IssuedByText { get; set; }
    public DateOnly? IssuedOn { get; set; }
    public string? Number { get; set; }
    public string? Description { get; set; }
    public string? FileName { get; set; }
    public string? DescriptionText { get; set; }
    public UserRefDto? User { get; set; }
    public SimpleRefDto? DocumentType { get; set; }
    public SimpleRefDto? IssuedByOrg { get; set; }

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
