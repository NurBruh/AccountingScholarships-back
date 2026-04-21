using AccountingScholarships.Application.DTO.EpvoSso;
using AccountingScholarships.Application.Interfaces;
using MediatR;
using AccountingScholarships.Domain.Entities.Real.epvosso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingScholarships.Application.Queries.EpvoSso
{
    public class GetAllEpvoProfession2025QueryHandler : IRequestHandler<GetAllEpvoProfession2025Query, IReadOnlyList<Profession_2025Dto>>
    {
        private readonly IEpvoSsoRepository<Profession_2025> _repository;

        public GetAllEpvoProfession2025QueryHandler(IEpvoSsoRepository<Profession_2025> repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyList<Profession_2025Dto>> Handle(GetAllEpvoProfession2025Query request, CancellationToken ct)
        {
            var entities = await _repository.GetAllAsync(ct);   
            return entities.Select(e => new Profession_2025Dto
            {
                typeCode = e.typeCode,
                UniversityId = e.UniversityId,
                ProfessionId = e.ProfessionId,
                ProfessionNameRu = e.ProfessionNameRu,
                ProfessionNameKz = e.ProfessionNameKz,
                ProfessionNameEn = e.ProfessionNameEn,
                DescriptionRu = e.DescriptionRu,
                DescriptionKz = e.DescriptionKz,
                DescriptionEn = e.DescriptionEn,
                ProfessionCode = e.ProfessionCode,
                DoubleDiploma = e.DoubleDiploma,
                PartnerName = e.PartnerName,
                Created = e.Created,
                DdStart = e.DdStart,
                Deleted = e.Deleted,
                AdvisorId = e.AdvisorId,
                AccreditAgencyId = e.AccreditAgencyId,
                AccreditValidity = e.AccreditValidity,
                AccreditInstMark = e.AccreditInstMark,
                Code = e.Code,
                TrainingDirectionsId = e.TrainingDirectionsId,
                Classifier = e.Classifier
            }).ToList().AsReadOnly();
        }
    }
}

