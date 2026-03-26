using AccountingScholarships.Domain.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Domain.Interfaces;
using MediatR;
namespace AccountingScholarships.Application.Queries.University.ReferenceData;
public class GetEduEducationPaymentTypeByIdQueryHandler : IRequestHandler<GetEduEducationPaymentTypeByIdQuery, Edu_EducationPaymentTypesDto?>
{
    private readonly ISsoRepository<Edu_EducationPaymentTypes> _repository;
    public GetEduEducationPaymentTypeByIdQueryHandler(ISsoRepository<Edu_EducationPaymentTypes> repository) { _repository = repository; }
    public async Task<Edu_EducationPaymentTypesDto?> Handle(GetEduEducationPaymentTypeByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity is null) return null;
        return new Edu_EducationPaymentTypesDto { ID = entity.ID, Title = entity.Title, ESUVOGrantTypeId = entity.ESUVOGrantTypeId, NoBDID = entity.NoBDID };
    }
}
