using AccountingScholarships.Application.DTO;
using MediatR;
using AccountingScholarships.Application.Interfaces;
using AccountingScholarships.Domain.Entities.Real.university;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AccountingScholarships.Application.Queries.University.ReferenceData
{
    public class GetAllEduGrantTypesNQueryHandler : IRequestHandler<GetAllEduGrantTypesNQuery, IReadOnlyList<Edu_GrantTypesNDto>>
    {
        private readonly ISsoRepository<Edu_GrantTypesN> _repository;

        public GetAllEduGrantTypesNQueryHandler(ISsoRepository<Edu_GrantTypesN> repository)
        {
            _repository = repository;
        }
        public async Task<IReadOnlyList<Edu_GrantTypesNDto>> Handle(GetAllEduGrantTypesNQuery request, CancellationToken ct)
        {
            var entities = await _repository.GetAllAsync(ct);
            return entities.Select(e => new Edu_GrantTypesNDto
            {
                Id = e.Id,
                Title = e.Title,
                ESUVOGrantTypeId = e.ESUVOGrantTypeId,
                Deleted = e.Deleted
            }).ToList().AsReadOnly();
        }

    }
}

