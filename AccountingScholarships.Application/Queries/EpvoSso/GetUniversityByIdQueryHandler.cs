using AccountingScholarships.Application.DTO.EpvoSso;
using AccountingScholarships.Domain.Entities.Real.epvosso;
using AccountingScholarships.Application.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.EpvoSso;

public class GetUniversityByIdQueryHandler
    : IRequestHandler<GetUniversityByIdQuery, EpvoUniversityDto?>
{
    private readonly IEpvoSsoRepository<AccountingScholarships.Domain.Entities.Real.epvosso.University> _repository;

    public GetUniversityByIdQueryHandler(IEpvoSsoRepository<AccountingScholarships.Domain.Entities.Real.epvosso.University> repository)
    {
        _repository = repository;
    }

    public async Task<EpvoUniversityDto?> Handle(
        GetUniversityByIdQuery request, CancellationToken cancellationToken)
    {
        var u = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (u is null) return null;

        return new EpvoUniversityDto
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
        };
    }
}
