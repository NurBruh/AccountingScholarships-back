using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingScholarships.Domain.Entities.university
{
    public class EduUsers
    {
        public int ID { get; set; } 
        public string LastName { get; set; }
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
        public string? PhotoFileName { get; set; }
        public byte[]? PhotoFileData { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public Guid? FileContainerID { get; set; }
        public string? MobilePushID { get; set; }
        public int? oldId { get; set; }
        public string FullName => $"{LastName} {FirstName} {MiddleName}".Trim();
        public string ShortName => $"{LastName} {FirstName?.FirstOrDefault()}. {MiddleName?.FirstOrDefault()}.".Trim();
        public int? ESUVOID { get; set; }
        public Guid? ExtraFileContainerID { get; set; }
        public bool Resident { get; set; }
        public int? Hero_Person_ID { get; set; }
        public bool? IsReadTeamsNotif { get; set; }

        // FK свойства
        public int? NationalityID { get; set; }
        public int? MaritalStatusID { get; set; }
        public int? MessengerTypeID { get; set; }
        public int? CitizenshipCountryID { get; set; }
        public int? CitizenCategoryID { get; set; }

        // Navigation Properties
        public EduNationalities? Nationality { get; set; }
        public EduMaritalStatuses? MaritalStatus { get; set; }
        public EduMessengers? MessengerType { get; set; }
        public EduCountries? CitizenshipCountry { get; set; }
        public EduCitizenCategories? CitizenCategory { get; set; }
        public EduStudents? Student { get; set; }
        public EduEmployees? Employee { get; set; }
        public ICollection<Edu_UserDocuments> Documents { get; set; } = new List<Edu_UserDocuments>();
    }
}
