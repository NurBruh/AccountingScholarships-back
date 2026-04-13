using AccountingScholarships.Application.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Application.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.Users;

public class GetAllEduUserAddressesQueryHandler : IRequestHandler<GetAllEduUserAddressesQuery, IReadOnlyList<Edu_UserAddressesDto>>
{
    private readonly ISsoRepository<Edu_UserAddresses> _repository;

    public GetAllEduUserAddressesQueryHandler(ISsoRepository<Edu_UserAddresses> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<Edu_UserAddressesDto>> Handle(GetAllEduUserAddressesQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllWithIncludesAsync(new[] { "User", "AddressType", "Country", "Locality" }, cancellationToken);
        return entities.Select(e => new Edu_UserAddressesDto
        {
            ID = e.ID,
            UserID = e.UserID,
            AddressTypeID = e.AddressTypeID,
            CountryID = e.CountryID,
            LocalityID = e.LocalityID,
            LocalityText = e.LocalityText,
            AddressText = e.AddressText,
            Region = e.Region,
            Area = e.Area,
            AddressTextEN = e.AddressTextEN,
            User = e.User == null ? null : new Edu_UserAddressesDto.UserRefDto
            {
                ID = e.User.ID,
                LastName = e.User.LastName,
                FirstName = e.User.FirstName
            },
            AddressType = e.AddressType == null ? null : new Edu_UserAddressesDto.SimpleRefDto
            {
                ID = e.AddressType.ID,
                Title = e.AddressType.Title
            },
            Country = e.Country == null ? null : new Edu_UserAddressesDto.SimpleRefDto
            {
                ID = e.Country.ID,
                Title = e.Country.Title
            },
            Locality = e.Locality == null ? null : new Edu_UserAddressesDto.SimpleRefDto
            {
                ID = e.Locality.ID,
                Title = e.Locality.Title
            }
        }).ToList().AsReadOnly();
    }
}
