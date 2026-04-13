using AccountingScholarships.Application.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Application.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.Users;

public class GetAllEduEntrantsQueryHandler : IRequestHandler<GetAllEduEntrantsQuery, IReadOnlyList<Edu_EntrantsDto>>
{
    private readonly ISsoRepository<Edu_Entrants> _repository;

    public GetAllEduEntrantsQueryHandler(ISsoRepository<Edu_Entrants> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<Edu_EntrantsDto>> Handle(GetAllEduEntrantsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllWithIncludesAsync(new[] { "User", "Level", "Status" }, cancellationToken);
        return entities.Select(e => new Edu_EntrantsDto
        {
            EntrantID = e.EntrantID,
            RegisteredOn = e.RegisteredOn,
            LevelID = e.LevelID,
            StatusID = e.StatusID,
            CheckedByAdmissions = e.CheckedByAdmissions,
            AllowCheckByDoctor = e.AllowCheckByDoctor,
            CheckedByDoctor = e.CheckedByDoctor,
            FormState = e.FormState,
            LastUpdatedBy = e.LastUpdatedBy,
            LastUpdatedOn = e.LastUpdatedOn,
            HasAppealation = e.HasAppealation,
            Application = e.Application,
            HasReceipt = e.HasReceipt,
            EnrollmentDate = e.EnrollmentDate,
            User = e.User == null ? null : new Edu_EntrantsDto.UserRefDto
            {
                ID = e.User.ID,
                LastName = e.User.LastName,
                FirstName = e.User.FirstName,
                MiddleName = e.User.MiddleName,
                IIN = e.User.IIN,
                DOB = e.User.DOB
            },
            Level = e.Level == null ? null : new Edu_EntrantsDto.LevelRefDto
            {
                ID = e.Level.ID,
                Title = e.Level.Title
            },
            Status = e.Status == null ? null : new Edu_EntrantsDto.StatusRefDto
            {
                ID = e.Status.ID,
                Title = e.Status.Title
            }
        }).ToList().AsReadOnly();
    }
}
