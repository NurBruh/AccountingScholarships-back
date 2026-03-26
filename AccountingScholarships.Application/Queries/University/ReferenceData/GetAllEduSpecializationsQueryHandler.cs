using AccountingScholarships.Domain.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Domain.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.ReferenceData;

public class GetAllEduSpecializationsQueryHandler : IRequestHandler<GetAllEduSpecializationsQuery, IReadOnlyList<Edu_SpecializationsDto>>
{
    private readonly ISsoRepository<Edu_Specializations> _repository;

    public GetAllEduSpecializationsQueryHandler(ISsoRepository<Edu_Specializations> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<Edu_SpecializationsDto>> Handle(GetAllEduSpecializationsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(cancellationToken);

        return entities
            .Select(e => new Edu_SpecializationsDto
            {
                Id = e.Id,
                TitleRu = e.TitleRu,
                TitleKz = e.TitleKz,
                TitleEn = e.TitleEn,
                Code = e.Code,
                ESUVOID = e.ESUVOID
            })
            .ToList()
            .AsReadOnly();
    }
}
