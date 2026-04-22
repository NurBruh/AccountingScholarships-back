using AccountingScholarships.Application.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Application.Interfaces;
using MediatR;
namespace AccountingScholarships.Application.Queries.University.ReferenceData;
public class GetAllEduEducationPaymentTypesQueryHandler : IRequestHandler<GetAllEduEducationPaymentTypesQuery, IReadOnlyList<Edu_EducationPaymentTypesDto>>
{
    private readonly ISsoRepository<Edu_EducationPaymentTypes> _repository;
    public GetAllEduEducationPaymentTypesQueryHandler(ISsoRepository<Edu_EducationPaymentTypes> repository) { _repository = repository; }
    public async Task<IReadOnlyList<Edu_EducationPaymentTypesDto>> Handle(GetAllEduEducationPaymentTypesQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(cancellationToken);
        return entities.Select(e => new Edu_EducationPaymentTypesDto { ID = e.ID, Title = e.Title, ESUVOGrantTypeId = e.ESUVOGrantTypeId, NoBDID = e.NoBDID }).ToList().AsReadOnly();
    }
}
