using AccountingScholarships.Domain.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Domain.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.Organization;

public class GetAllEduEmployeesQueryHandler : IRequestHandler<GetAllEduEmployeesQuery, IReadOnlyList<Edu_EmployeesDto>>
{
    private readonly ISsoRepository<Edu_Employees> _repository;

    public GetAllEduEmployeesQueryHandler(ISsoRepository<Edu_Employees> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<Edu_EmployeesDto>> Handle(GetAllEduEmployeesQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllWithIncludesAsync(new[] { "User" }, cancellationToken);
        return entities.Select(e => new Edu_EmployeesDto
        {
            ID = e.ID,
            IsAdvisor = e.IsAdvisor,
            RoleGroupId = e.RoleGroupId,
            User = e.User == null ? null : new Edu_EmployeesDto.UserRefDto
            {
                ID = e.User.ID,
                LastName = e.User.LastName,
                FirstName = e.User.FirstName,
                MiddleName = e.User.MiddleName,
                IIN = e.User.IIN,
                Email = e.User.Email,
                MobilePhone = e.User.MobilePhone
            }
        }).ToList().AsReadOnly();
    }
}
