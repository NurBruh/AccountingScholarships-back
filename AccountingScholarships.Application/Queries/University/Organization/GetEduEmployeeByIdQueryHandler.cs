using AccountingScholarships.Application.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Application.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.Organization;

public class GetEduEmployeeByIdQueryHandler : IRequestHandler<GetEduEmployeeByIdQuery, Edu_EmployeesDto?>
{
    private readonly ISsoRepository<Edu_Employees> _repository;

    public GetEduEmployeeByIdQueryHandler(ISsoRepository<Edu_Employees> repository)
    {
        _repository = repository;
    }

    public async Task<Edu_EmployeesDto?> Handle(GetEduEmployeeByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.FindFirstWithIncludesAsync(x => x.ID == request.Id, new[] { "User" }, cancellationToken);
        if (entity is null) return null;
        return new Edu_EmployeesDto
        {
            ID = entity.ID,
            IsAdvisor = entity.IsAdvisor,
            RoleGroupId = entity.RoleGroupId,
            User = entity.User == null ? null : new Edu_EmployeesDto.UserRefDto
            {
                ID = entity.User.ID,
                LastName = entity.User.LastName,
                FirstName = entity.User.FirstName,
                MiddleName = entity.User.MiddleName,
                IIN = entity.User.IIN,
                Email = entity.User.Email,
                MobilePhone = entity.User.MobilePhone
            }
        };
    }
}
