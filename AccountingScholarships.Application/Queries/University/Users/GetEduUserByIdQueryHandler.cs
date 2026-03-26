using AccountingScholarships.Domain.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Domain.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.Users;

public class GetEduUserByIdQueryHandler : IRequestHandler<GetEduUserByIdQuery, Edu_UsersDto?>
{
    private readonly ISsoRepository<Edu_Users> _repository;

    public GetEduUserByIdQueryHandler(ISsoRepository<Edu_Users> repository)
    {
        _repository = repository;
    }

    public async Task<Edu_UsersDto?> Handle(GetEduUserByIdQuery request, CancellationToken cancellationToken)
    {
        var e = await _repository.FindFirstWithIncludesAsync(
            x => x.ID == request.Id,
            new[] { "Nationality", "MaritalStatus", "MessengerType", "CitizenshipCountry", "CitizenCategory" },
            cancellationToken);

        if (e is null) return null;

        return new Edu_UsersDto
        {
            ID = e.ID,
            LastName = e.LastName,
            FirstName = e.FirstName,
            MiddleName = e.MiddleName,
            Email = e.Email,
            PersonalEmail = e.PersonalEmail,
            DOB = e.DOB,
            PlaceOfBirth = e.PlaceOfBirth,
            Male = e.Male,
            HomePhone = e.HomePhone,
            MobilePhone = e.MobilePhone,
            IIN = e.IIN,
            ESUVOID = e.ESUVOID,
            Resident = e.Resident,
            NationalityID = e.NationalityID,
            MaritalStatusID = e.MaritalStatusID,
            MessengerTypeID = e.MessengerTypeID,
            CitizenshipCountryID = e.CitizenshipCountryID,
            CitizenCategoryID = e.CitizenCategoryID,
            Nationality = e.Nationality == null ? null : new Edu_UsersDto.SimpleRefDto { ID = e.Nationality.ID, Title = e.Nationality.Title },
            MaritalStatus = e.MaritalStatus == null ? null : new Edu_UsersDto.SimpleRefDto { ID = e.MaritalStatus.ID, Title = e.MaritalStatus.Title },
            MessengerType = e.MessengerType == null ? null : new Edu_UsersDto.SimpleRefDto { ID = e.MessengerType.ID, Title = e.MessengerType.Title },
            CitizenshipCountry = e.CitizenshipCountry == null ? null : new Edu_UsersDto.SimpleRefDto { ID = e.CitizenshipCountry.ID, Title = e.CitizenshipCountry.Title },
            CitizenCategory = e.CitizenCategory == null ? null : new Edu_UsersDto.SimpleRefDto { ID = e.CitizenCategory.ID, Title = e.CitizenCategory.Title }
        };
    }
}
