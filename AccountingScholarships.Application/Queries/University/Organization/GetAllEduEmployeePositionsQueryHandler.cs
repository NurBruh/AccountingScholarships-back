using AccountingScholarships.Application.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Application.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.Organization;

public class GetAllEduEmployeePositionsQueryHandler : IRequestHandler<GetAllEduEmployeePositionsQuery, IReadOnlyList<Edu_EmployeePositionsDto>>
{
    private readonly ISsoRepository<Edu_EmployeePositions> _repository;

    public GetAllEduEmployeePositionsQueryHandler(ISsoRepository<Edu_EmployeePositions> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<Edu_EmployeePositionsDto>> Handle(GetAllEduEmployeePositionsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllWithIncludesAsync(new[] { "OrgUnit", "Position", "Employee" }, cancellationToken);
        return entities.Select(e => new Edu_EmployeePositionsDto
        {
            ID = e.ID,
            StartedOn = e.StartedOn,
            EndedOn = e.EndedOn,
            LastUpdatedBy = e.LastUpdatedBy,
            LastUpdatedOn = e.LastUpdatedOn,
            Rate = e.Rate,
            IsMainPosition = e.IsMainPosition,
            HrOrderId = e.HrOrderId,
            OrgUnitID = e.OrgUnitID,
            PositionID = e.PositionID,
            EmployeeID = e.EmployeeID,
            OrgUnit = e.OrgUnit == null ? null : new Edu_EmployeePositionsDto.OrgUnitRefDto
            {
                ID = e.OrgUnit.ID,
                Title = e.OrgUnit.Title
            },
            Position = e.Position == null ? null : new Edu_EmployeePositionsDto.PositionRefDto
            {
                ID = e.Position.ID,
                Title = e.Position.Title
            },
            Employee = e.Employee == null ? null : new Edu_EmployeePositionsDto.EmployeeRefDto
            {
                ID = e.Employee.ID,
                IsAdvisor = e.Employee.IsAdvisor
            }
        }).ToList().AsReadOnly();
    }
}
