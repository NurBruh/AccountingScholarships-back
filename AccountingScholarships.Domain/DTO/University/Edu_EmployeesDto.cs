namespace AccountingScholarships.Domain.DTO.University;

public class Edu_EmployeesDto
{
    public int ID { get; set; }
    public bool IsAdvisor { get; set; }
    public int? RoleGroupId { get; set; }
    public UserRefDto? User { get; set; }

    public class UserRefDto
    {
        public int ID { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? IIN { get; set; }
        public string? Email { get; set; }
        public string? MobilePhone { get; set; }
    }
}
