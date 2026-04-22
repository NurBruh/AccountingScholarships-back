using AccountingScholarships.Application.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Application.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.Academic;

public class GetEduSpecialitySpecializationByIdQueryHandler : IRequestHandler<GetEduSpecialitySpecializationByIdQuery, Edu_SpecialitySpecializationsDto?>
{
    private readonly ISsoRepository<Edu_SpecialitySpecializations> _repository;

    public GetEduSpecialitySpecializationByIdQueryHandler(ISsoRepository<Edu_SpecialitySpecializations> repository)
    {
        _repository = repository;
    }

    public async Task<Edu_SpecialitySpecializationsDto?> Handle(GetEduSpecialitySpecializationByIdQuery request, CancellationToken cancellationToken)
    {
        var e = await _repository.FindFirstWithIncludesAsync(
            x => x.ID == request.Id,
            new[] { "Speciality", "Specialization" },
            cancellationToken);

        if (e is null) return null;

        return new Edu_SpecialitySpecializationsDto
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
        };
    }
}
