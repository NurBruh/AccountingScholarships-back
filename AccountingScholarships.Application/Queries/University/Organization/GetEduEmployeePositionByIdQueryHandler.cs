using AccountingScholarships.Application.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Application.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.Organization;

public class GetEduEmployeePositionByIdQueryHandler : IRequestHandler<GetEduEmployeePositionByIdQuery, Edu_EmployeePositionsDto?>
{
    private readonly ISsoRepository<Edu_EmployeePositions> _repository;

    public GetEduEmployeePositionByIdQueryHandler(ISsoRepository<Edu_EmployeePositions> repository)
    {
        _repository = repository;
    }

    public async Task<Edu_EmployeePositionsDto?> Handle(GetEduEmployeePositionByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.FindFirstWithIncludesAsync(
            x => x.ID == request.Id,
            new[] { "OrgUnit", "Position", "Employee" },
            cancellationToken);
        if (entity is null) return null;
        return new Edu_EmployeePositionsDto
        {
            ID = entity.ID,
            StartedOn = entity.StartedOn,
            EndedOn = entity.EndedOn,
            LastUpdatedBy = entity.LastUpdatedBy,
            LastUpdatedOn = entity.LastUpdatedOn,
            Rate = entity.Rate,
            IsMainPosition = entity.IsMainPosition,
            HrOrderId = entity.HrOrderId,
            OrgUnitID = entity.OrgUnitID,
            PositionID = entity.PositionID,
            EmployeeID = entity.EmployeeID,
            OrgUnit = entity.OrgUnit == null ? null : new Edu_EmployeePositionsDto.OrgUnitRefDto
            {
                ID = entity.OrgUnit.ID,
                Title = entity.OrgUnit.Title
            },
            Position = entity.Position == null ? null : new Edu_EmployeePositionsDto.PositionRefDto
            {
                ID = entity.Position.ID,
                Title = entity.Position.Title
            },
            Employee = entity.Employee == null ? null : new Edu_EmployeePositionsDto.EmployeeRefDto
            {
                ID = entity.Employee.ID,
                IsAdvisor = entity.Employee.IsAdvisor
            }
        };
    }
}
