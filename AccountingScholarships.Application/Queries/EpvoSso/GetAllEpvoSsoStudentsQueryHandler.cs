using AccountingScholarships.Domain.DTO.EpvoSso;
using AccountingScholarships.Domain.Interfaces;
using MediatR;
using EpvoStudent = AccountingScholarships.Domain.Entities.Real.epvosso.Student;

namespace AccountingScholarships.Application.Queries.EpvoSso;

public class GetAllEpvoSsoStudentsQueryHandler
    : IRequestHandler<GetAllEpvoSsoStudentsQuery, IReadOnlyList<EpvoStudentSsoDto>>
{
    private readonly IEpvoSsoRepository<EpvoStudent> _repository;

    public GetAllEpvoSsoStudentsQueryHandler(IEpvoSsoRepository<EpvoStudent> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<EpvoStudentSsoDto>> Handle(
        GetAllEpvoSsoStudentsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(cancellationToken);
        return entities.Select(s => new EpvoStudentSsoDto
        {
            UniversityId = s.UniversityId,
            StudentId = s.StudentId,
            FirstName = s.FirstName,
            LastName = s.LastName,
            Patronymic = s.Patronymic,
            BirthDate = s.BirthDate,
            StartDate = s.StartDate,
            Address = s.Address,
            NationId = s.NationId,
            StudyFormId = s.StudyFormId,
            StudyCalendarId = s.StudyCalendarId,
            PaymentFormId = s.PaymentFormId,
            StudyLanguageId = s.StudyLanguageId,
            ProfessionId = s.ProfessionId,
            CourseNumber = s.CourseNumber,
            TranscriptNumber = s.TranscriptNumber,
            TranscriptSeries = s.TranscriptSeries,
            IsMarried = s.IsMarried,
            IcNumber = s.IcNumber,
            IcDate = s.IcDate,
            Education = s.Education,
            HasExcellent = s.HasExcellent,
            StartOrder = s.StartOrder,
            IsStudent = s.IsStudent,
            Certificate = s.Certificate,
            GrantNumber = s.GrantNumber,
            Gpa = s.Gpa,
            CurrentCreditsSum = s.CurrentCreditsSum,
            Residence = s.Residence,
            SitizenshipId = s.SitizenshipId,
            DormState = s.DormState,
            IsInRetire = s.IsInRetire,
            FromId = s.FromId,
            Local = s.Local,
            City = s.City,
            ContractId = s.ContractId,
            SpecializationId = s.SpecializationId,
            IinPlt = s.IinPlt,
            AltynBelgi = s.AltynBelgi,
            DataVydachiAttestata = s.DataVydachiAttestata,
            DataVydachiDiploma = s.DataVydachiDiploma,
            DateDocEducation = s.DateDocEducation,
            EndCollege = s.EndCollege,
            EndHighSchool = s.EndHighSchool,
            EndSchool = s.EndSchool,
            IcSeries = s.IcSeries,
            IcType = s.IcType,
            LivingAddress = s.LivingAddress,
            NomerAttestata = s.NomerAttestata,
            OtherBirthPlace = s.OtherBirthPlace,
            SeriesNumberDocEducation = s.SeriesNumberDocEducation,
            SeriyaAttestata = s.SeriyaAttestata,
            SeriyaDiploma = s.SeriyaDiploma,
            SchoolName = s.SchoolName,
            FacultyId = s.FacultyId,
            SexId = s.SexId,
            Mail = s.Mail,
            Phone = s.Phone,
            SumPoints = s.SumPoints,
            SumPointsCreative = s.SumPointsCreative,
            EnrollOrderDate = s.EnrollOrderDate,
            MobilePhone = s.MobilePhone,
            GrantType = s.GrantType,
            AcademicMobility = s.AcademicMobility,
            IncorrectIin = s.IncorrectIin,
            BirthPlaceCatoId = s.BirthPlaceCatoId,
            LivingPlaceCatoId = s.LivingPlaceCatoId,
            RegistrationPlaceCatoId = s.RegistrationPlaceCatoId,
            NaselennyiPunktAttestataCatoId = s.NaselennyiPunktAttestataCatoId,
            EnterExamType = s.EnterExamType,
            FundingId = s.FundingId,
            TypeCode = s.TypeCode,
        }).ToList().AsReadOnly();
    }
}
