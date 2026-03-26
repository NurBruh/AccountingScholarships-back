namespace AccountingScholarships.Domain.DTO.University;

public class Edu_PositionsDto
{
    public int ID { get; set; }
    public string? Title { get; set; }
    public bool Deleted { get; set; }
    public string? Description { get; set; }
    public int Lectures { get; set; }
    public int Practices { get; set; }
    public int Labs { get; set; }
    public int? CategoryID { get; set; }
    public Edu_PositionCategoriesDto? Category { get; set; }
}
