using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingScholarships.Domain.Entities.Real.epvosso
{
    public class University
    {
        public int UniversityId { get; set; }
        public string? FullNameEn { get; set; }
        public string? FullNameKz { get; set; }
        public string? FullNameRu { get; set; }
        public string? AddressEn { get; set; }
        public string? AddressKz { get; set; }
        public string? AddressRu { get; set; }
        public string? Phone { get; set; }
        public int? RectorId { get; set; }
        public DateOnly? StartDate { get; set; }
        public int? CompCnt { get; set; }
        public int? OldCompCnt { get; set; }
        public decimal? StsOnComp { get; set; }
        public int? UniversityTypeId { get; set; }
        public int AccessRepLibrary { get; set; }
        public string? SpeedInternetConn { get; set; }
        public string? Bin { get; set; }
        public int? RowNumberForPk { get; set; }
        public int? RegionId { get; set; }
        public string? RegionName { get; set; }
        public string? Website { get; set; }
        public string? StudyLanguages { get; set; }
        public int? ComputerRoomsCount { get; set; }
        public int? LaboratoriesCount { get; set; }
        public int? BorrowingCheckSystem { get; set; }
        public int? BankId { get; set; }
        public string? BankAccNum  { get; set; }
        public string? Email { get; set; }
        public string? CorpManOrgan { get; set; }
        public bool? HasMilitary { get; set; }
        public int? PointsSpeedConnAbove { get; set; }
        public int? PointsSpeedConnUp { get; set; }
        public int? InfSystemId { get; set; }
        public string? InfSystemName { get; set; }
        public string? InfSystemAddress { get; set; }
        public int? CanteenCount { get; set; }
        public int? InternetCompCount { get; set; }
        public string? LibraryAddress { get; set; }
        public string? TypeCode { get; set; }

    }
}
