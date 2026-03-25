using AccountingScholarships.Domain.DTO.EpvoSso;
using AccountingScholarships.Domain.Entities.Real.epvosso;
using AccountingScholarships.Domain.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.EpvoSso;

public class GetAllStudentInfoQueryHandler
    : IRequestHandler<GetAllStudentInfoQuery, IReadOnlyList<EpvoStudentInfoDto>>
{
    private readonly IEpvoSsoRepository<Student_Info> _repository;

    public GetAllStudentInfoQueryHandler(IEpvoSsoRepository<Student_Info> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<EpvoStudentInfoDto>> Handle(
        GetAllStudentInfoQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(cancellationToken);
        return entities.Select(s => new EpvoStudentInfoDto
        {
            UniversityId = s.UniversityId,
            StudentId = s.StudentId,
            EndHighSchoolType = s.EndHighSchoolType,
            PreviousGpa = s.PreviousGpa,
            EducationConditionId = s.EducationConditionId,
            ForeignLangCertMark = s.ForeignLangCertMark,
            EntranceExamFinalMark = s.EntranceExamFinalMark,
            CenterUniversityId = s.CenterUniversityId,
            CenterProfessionCode = s.CenterProfessionCode,
            EntCertSeries = s.EntCertSeries,
            EntCertDatePrint = s.EntCertDatePrint,
            EntPassedLang = s.EntPassedLang,
            EntIndividualCode = s.EntIndividualCode,
            ConditionallyEnrolled = s.ConditionallyEnrolled,
            GraduateDiplomaNumber = s.GraduateDiplomaNumber,
            GraduateDiplomaSeries = s.GraduateDiplomaSeries,
            NostrificationSeries = s.NostrificationSeries,
            NostrificationDate = s.NostrificationDate,
            OtherBornCountryId = s.OtherBornCountryId,
            BornInAnotherCountry = s.BornInAnotherCountry,
            ForeignLangCertSubjectId = s.ForeignLangCertSubjectId,
            ExamBySpecialtySubjectId = s.ExamBySpecialtySubjectId,
            ForeignLangCertExists = s.ForeignLangCertExists,
            ForeignLangCertId = s.ForeignLangCertId,
            HighSchoolType = s.HighSchoolType,
            GraduatedCountryId = s.GraduatedCountryId,
            ByProfile = s.ByProfile,
            DegreeId = s.DegreeId,
            EntranceExamLanguageId = s.EntranceExamLanguageId,
            InstitutionId = s.InstitutionId,
            IcDepartmentId = s.IcDepartmentId,
            DomesticHighSchoolName = s.DomesticHighSchoolName,
            DomesticHighSchoolProfession = s.DomesticHighSchoolProfession,
            CertificateSeries = s.CertificateSeries,
            AwardedDate = s.AwardedDate,
            HasCreativeExam = s.HasCreativeExam,
            SpecialExamProvided = s.SpecialExamProvided,
            SpecialExamAdmission = s.SpecialExamAdmission,
            DomesticHighSchoolType = s.DomesticHighSchoolType,
            InterviewProtocolId = s.InterviewProtocolId,
            TrilingualEducation = s.TrilingualEducation,
            InNationalStudentLeague = s.InNationalStudentLeague,
            StudiedForeignLangId = s.StudiedForeignLangId,
            IntergovernmentalGrant = s.IntergovernmentalGrant,
            PublicAuthorityGrant = s.PublicAuthorityGrant,
            BenefitQuotaId = s.BenefitQuotaId,
            AddEntranceExamAdmission = s.AddEntranceExamAdmission,
            AddEntranceExamDate = s.AddEntranceExamDate,
            Iic = s.Iic,
            Bic = s.Bic,
            BankId = s.BankId,
            WinterAdmission = s.WinterAdmission,
            ProgramId = s.ProgramId,
            UpdateDate = s.UpdateDate,
            FhighSchool = s.FhighSchool,
            FhighSchoolProfession = s.FhighSchoolProfession,
            TypeCode = s.TypeCode,
        }).ToList().AsReadOnly();
    }
}
