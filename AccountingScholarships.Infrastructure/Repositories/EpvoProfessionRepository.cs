using AccountingScholarships.Domain.DTO.EpvoSso;
using AccountingScholarships.Domain.Entities.epvosso;
using AccountingScholarships.Domain.Interfaces;
using AccountingScholarships.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingScholarships.Infrastructure.Repositories
{
    public class EpvoProfessionRepository
    : EpvoSsoRepository<Profession>, IEpvoProfessionRepository
    {
        public EpvoProfessionRepository(EpvoSsoDbContext context)
            : base(context) { }
        public async Task<EpvoProfessionDto?> GetAsDtoAsync(
            int id, CancellationToken ct = default)
        {
            var entity = await GetByIdAsync(id, ct);
            return entity is null ? null : MapToDto(entity);
        }
        public async Task<IReadOnlyList<EpvoProfessionDto>> GetAllAsDtoAsync(
            CancellationToken ct = default)
        {
            var list = await GetAllAsync(ct);
            return list.Select(MapToDto).ToList();
        }
        // ─── Маппер Entity → DTO ──────────────────────────────────────
        private static EpvoProfessionDto MapToDto(Profession p) => new()
        {
            ProfessionId = p.ProfessionId,
            Code = p.Code,
            ProfessionCode = p.ProfessionCode,
            ProfessionNameRu = p.ProfessionNameRu,
            ProfessionNameKz = p.ProfessionNameKz,
            ProfessionNameEn = p.ProfessionNameEn,
            DoubleDiploma = p.DoubleDiploma,
            UniversityId = p.universityId,
            TypeCode = p.TypeCode,
        };
    }
}
