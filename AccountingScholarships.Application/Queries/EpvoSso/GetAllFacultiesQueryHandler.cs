using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountingScholarships.Domain.Entities.epvosso;
using AccountingScholarships.Domain.Interfaces;
using AccountingScholarships.Domain.DTO.EpvoSso;
using MediatR;

namespace AccountingScholarships.Application.Queries.EpvoSso
{
    public class GetAllFacultiesQueryHandler : IRequestHandler<GetAllFacultiesQuery, IReadOnlyList<FacultyDto>>
    {
        private readonly IEpvoSsoRepository<Faculties> _repository;
        public GetAllFacultiesQueryHandler(IEpvoSsoRepository<Faculties> repository)
        {
            _repository = repository;
        }
        
        public async Task<IReadOnlyList<FacultyDto>> Handle (
            GetAllFacultiesQuery request, CancellationToken ct)
        {
            var entities = await _repository.GetAllAsync(ct);

            return entities.Select(e => new FacultyDto
            {
                Created = e.Created,
                DialUp = e.DialUp,
                FacultyDean = e.FacultyDean,
                FacultyId = e.FacultyId,
                FacultyNameEn = e.FacultyNameEn,
                FacultyNameKz = e.FacultyNameKz,
                FacultyNameRu = e.FacultyNameRu,
                UniversityId = e.UniversityId,
                InformationEn = e.InformationEn,
                InformationKz = e.InformationKz,
                InformationRu = e.InformationRu,
                Proper = e.Proper,
                Satellite = e.Satellite,
                TypeCode = e.TypeCode
            }).ToList().AsReadOnly();
        }
    }
}
