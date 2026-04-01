using AccountingScholarships.Domain.DTO.EpvoSso;
using MediatR;
using AccountingScholarships.Domain.Entities.Real.epvosso;
using AccountingScholarships.Domain.Interfaces; 
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingScholarships.Application.Queries.EpvoSso
{
    public class GetAllProfessionEpvoNewQueryHandler
        : IRequestHandler<GetAllProfessionEpvoNewQuery, IReadOnlyList<Profession_Epvo_NewDto>>
    {
        private readonly IEpvoSsoRepository<Profession_Epvo_New> _repository;
        
        public GetAllProfessionEpvoNewQueryHandler(IEpvoSsoRepository<Profession_Epvo_New> repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyList<Profession_Epvo_NewDto>> Handle(
            GetAllProfessionEpvoNewQuery request, CancellationToken ct)
        {
            var entities = await _repository.GetAllAsync(ct);
            return entities.Select(p => new Profession_Epvo_NewDto
            {
                TypeCode = p.TypeCode,
                UniversityId = p.UniversityId,
                ProfessionId = p.ProfessionId,
                ProfessionNameRu = p.ProfessionNameRu,
                ProfessionNameKz = p.ProfessionNameKz,
                ProfessionNameEn = p.ProfessionNameEn,
                DescriptionRu = p.DescriptionRu,
                DescriptionKz = p.DescriptionKz,
                DescriptionEn = p.DescriptionEn,
                ProfessionCode = p.ProfessionCode,
                PartnerName = p.PartnerName,
                Created = p.Created,
                DdStart = p.DdStart,
                Deleted = p.Deleted,
                AdvisorId = p.AdvisorId,
                AccreditAgencyId = p.AccreditAgencyId,
                AccreditValidity = p.AccreditValidity,
                AccreditInstMark = p.AccreditInstMark,
                Code = p.Code,
                TrainingDirectionsId = p.TrainingDirectionsId,
                Classifier = p.Classifier,
            }).ToList().AsReadOnly();
        }
    }
}
