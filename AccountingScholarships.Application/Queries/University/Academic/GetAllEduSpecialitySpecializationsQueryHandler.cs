using AccountingScholarships.Domain.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Domain.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.Academic;

public class GetAllEduSpecialitySpecializationsQueryHandler : IRequestHandler<GetAllEduSpecialitySpecializationsQuery, IReadOnlyList<Edu_SpecialitySpecializationsDto>>
{
    private readonly ISsoRepository<Edu_SpecialitySpecializations> _repository;

    public GetAllEduSpecialitySpecializationsQueryHandler(ISsoRepository<Edu_SpecialitySpecializations> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<Edu_SpecialitySpecializationsDto>> Handle(GetAllEduSpecialitySpecializationsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllWithIncludesAsync(new[] { "Speciality", "Specialization" }, cancellationToken);

        return entities.Select(e => new Edu_SpecialitySpecializationsDto
        {
            ID = e.ID,
            SpecialityId = e.SpecialityId,
            SpecializationId = e.SpecializationId,
            Speciality = e.Speciality == null ? null : new Edu_SpecialitySpecializationsDto.SpecialityRefDto
            {
                ID = e.Speciality.ID,
                Code = e.Speciality.Code,
                Title = e.Speciality.Title
            },
            Specialization = e.Specialization == null ? null : new Edu_SpecialitySpecializationsDto.SpecializationRefDto
            {
                Id = e.Specialization.Id,
                TitleRu = e.Specialization.TitleRu,
                Code = e.Specialization.Code
            }
        }).ToList().AsReadOnly();
    }
}
