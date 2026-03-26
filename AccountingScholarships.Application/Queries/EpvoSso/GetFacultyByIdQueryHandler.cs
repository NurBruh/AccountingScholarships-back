using AccountingScholarships.Domain.DTO.EpvoSso;
using AccountingScholarships.Domain.Entities.Real.epvosso;
using AccountingScholarships.Domain.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.EpvoSso;

public class GetFacultyByIdQueryHandler
    : IRequestHandler<GetFacultyByIdQuery, FacultyDto?>
{
    private readonly IEpvoSsoRepository<Faculties> _repository;

    public GetFacultyByIdQueryHandler(IEpvoSsoRepository<Faculties> repository)
    {
        _repository = repository;
    }

    public async Task<FacultyDto?> Handle(
        GetFacultyByIdQuery request, CancellationToken cancellationToken)
    {
        var s = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (s is null) return null;

        return new FacultyDto
        {
            Created = s.Created,
            DialUp = s.DialUp,
            FacultyDean = s.FacultyDean,
            FacultyId = s.FacultyId,
            FacultyNameEn = s.FacultyNameEn,
            FacultyNameKz = s.FacultyNameKz,
            FacultyNameRu = s.FacultyNameRu,
            UniversityId = s.UniversityId,
            InformationEn = s.InformationEn,
            InformationKz = s.InformationKz,
            InformationRu = s.InformationRu,
            Proper = s.Proper,
            Satellite = s.Satellite,
            TypeCode = s.TypeCode,
        };
    }
}
