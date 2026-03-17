using AccountingScholarships.Domain.DTO.EpvoSso;
using AccountingScholarships.Domain.Entities.epvosso;
using AccountingScholarships.Domain.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.EpvoSso;

public class GetAllUniversitiesQueryHandler
    : IRequestHandler<GetAllUniversitiesQuery, IReadOnlyList<EpvoUniversityDto>>
{
    private readonly IEpvoSsoRepository<AccountingScholarships.Domain.Entities.epvosso.University> _repository;

    public GetAllUniversitiesQueryHandler(IEpvoSsoRepository<AccountingScholarships.Domain.Entities.epvosso.University> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<EpvoUniversityDto>> Handle(
        GetAllUniversitiesQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(cancellationToken);
        return entities.Select(u => new EpvoUniversityDto
        {
            UniversityId = u.UniversityId,
            FullNameEn = u.FullNameEn,
            FullNameKz = u.FullNameKz,
            FullNameRu = u.FullNameRu,
            AddressEn = u.AddressEn,
            AddressKz = u.AddressKz,
            AddressRu = u.AddressRu,
            Phone = u.Phone,
            RectorId = u.RectorId,
            StartDate = u.StartDate,
            CompCnt = u.CompCnt,
            OldCompCnt = u.OldCompCnt,
            StsOnComp = u.StsOnComp,
            UniversityTypeId = u.UniversityTypeId,
            AccessRepLibrary = u.AccessRepLibrary,
            SpeedInternetConn = u.SpeedInternetConn,
            Bin = u.Bin,
            RowNumberForPk = u.RowNumberForPk,
            RegionId = u.RegionId,
            RegionName = u.RegionName,
            Website = u.Website,
            StudyLanguages = u.StudyLanguages,
            ComputerRoomsCount = u.ComputerRoomsCount,
            LaboratoriesCount = u.LaboratoriesCount,
            BorrowingCheckSystem = u.BorrowingCheckSystem,
            BankId = u.BankId,
            BankAccNum = u.BankAccNum,
            Email = u.Email,
            CorpManOrgan = u.CorpManOrgan,
            HasMilitary = u.HasMilitary,
            PointsSpeedConnAbove = u.PointsSpeedConnAbove,
            PointsSpeedConnUp = u.PointsSpeedConnUp,
            InfSystemId = u.InfSystemId,
            InfSystemName = u.InfSystemName,
            InfSystemAddress = u.InfSystemAddress,
            CanteenCount = u.CanteenCount,
            InternetCompCount = u.InternetCompCount,
            LibraryAddress = u.LibraryAddress,
            TypeCode = u.TypeCode,
        }).ToList().AsReadOnly();
    }
}
