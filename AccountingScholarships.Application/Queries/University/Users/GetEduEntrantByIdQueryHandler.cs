using AccountingScholarships.Domain.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Domain.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.Users;

public class GetEduEntrantByIdQueryHandler : IRequestHandler<GetEduEntrantByIdQuery, Edu_EntrantsDto?>
{
    private readonly ISsoRepository<Edu_Entrants> _repository;

    public GetEduEntrantByIdQueryHandler(ISsoRepository<Edu_Entrants> repository)
    {
        _repository = repository;
    }

    public async Task<Edu_EntrantsDto?> Handle(GetEduEntrantByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.FindFirstWithIncludesAsync(
            x => x.EntrantID == request.Id,
            new[] { "User", "Level", "Status" },
            cancellationToken);
        if (entity is null) return null;
        return new Edu_EntrantsDto
        {
            EntrantID = entity.EntrantID,
            RegisteredOn = entity.RegisteredOn,
            LevelID = entity.LevelID,
            StatusID = entity.StatusID,
            CheckedByAdmissions = entity.CheckedByAdmissions,
            AllowCheckByDoctor = entity.AllowCheckByDoctor,
            CheckedByDoctor = entity.CheckedByDoctor,
            FormState = entity.FormState,
            LastUpdatedBy = entity.LastUpdatedBy,
            LastUpdatedOn = entity.LastUpdatedOn,
            HasAppealation = entity.HasAppealation,
            Application = entity.Application,
            HasReceipt = entity.HasReceipt,
            EnrollmentDate = entity.EnrollmentDate,
            User = entity.User == null ? null : new Edu_EntrantsDto.UserRefDto
            {
                ID = entity.User.ID,
                LastName = entity.User.LastName,
                FirstName = entity.User.FirstName,
                MiddleName = entity.User.MiddleName,
                IIN = entity.User.IIN,
                DOB = entity.User.DOB
            },
            Level = entity.Level == null ? null : new Edu_EntrantsDto.LevelRefDto
            {
                ID = entity.Level.ID,
                Title = entity.Level.Title
            },
            Status = entity.Status == null ? null : new Edu_EntrantsDto.StatusRefDto
            {
                ID = entity.Status.ID,
                Title = entity.Status.Title
            }
        };
    }
}
