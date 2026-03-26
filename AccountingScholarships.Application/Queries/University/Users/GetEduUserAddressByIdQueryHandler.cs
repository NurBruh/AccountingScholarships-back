using AccountingScholarships.Domain.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Domain.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.Users;

public class GetEduUserAddressByIdQueryHandler : IRequestHandler<GetEduUserAddressByIdQuery, Edu_UserAddressesDto?>
{
    private readonly ISsoRepository<Edu_UserAddresses> _repository;

    public GetEduUserAddressByIdQueryHandler(ISsoRepository<Edu_UserAddresses> repository)
    {
        _repository = repository;
    }

    public async Task<Edu_UserAddressesDto?> Handle(GetEduUserAddressByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.FindFirstWithIncludesAsync(
            x => x.ID == request.Id,
            new[] { "User", "AddressType", "Country", "Locality" },
            cancellationToken);
        if (entity is null) return null;
        return new Edu_UserAddressesDto
        {
            ID = entity.ID,
            UserID = entity.UserID,
            AddressTypeID = entity.AddressTypeID,
            CountryID = entity.CountryID,
            LocalityID = entity.LocalityID,
            LocalityText = entity.LocalityText,
            AddressText = entity.AddressText,
            Region = entity.Region,
            Area = entity.Area,
            AddressTextEN = entity.AddressTextEN,
            User = entity.User == null ? null : new Edu_UserAddressesDto.UserRefDto
            {
                ID = entity.User.ID,
                LastName = entity.User.LastName,
                FirstName = entity.User.FirstName
            },
            AddressType = entity.AddressType == null ? null : new Edu_UserAddressesDto.SimpleRefDto
            {
                ID = entity.AddressType.ID,
                Title = entity.AddressType.Title
            },
            Country = entity.Country == null ? null : new Edu_UserAddressesDto.SimpleRefDto
            {
                ID = entity.Country.ID,
                Title = entity.Country.Title
            },
            Locality = entity.Locality == null ? null : new Edu_UserAddressesDto.SimpleRefDto
            {
                ID = entity.Locality.ID,
                Title = entity.Locality.Title
            }
        };
    }
}
