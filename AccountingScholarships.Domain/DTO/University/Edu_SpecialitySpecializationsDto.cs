namespace AccountingScholarships.Domain.DTO.University;

public class Edu_SpecialitySpecializationsDto
{
    public int ID { get; set; }
    public int? SpecialityId { get; set; }
    public int? SpecializationId { get; set; }
    public SpecialityRefDto? Speciality { get; set; }
    public SpecializationRefDto? Specialization { get; set; }

    public class SpecialityRefDto
    {
        public int ID { get; set; }
        public string? Code { get; set; }
        public string? Title { get; set; }
    }

    public class SpecializationRefDto
    {
        public int Id { get; set; }
        public string? TitleRu { get; set; }
        public string? Code { get; set; }
    }
}
