using AccountingScholarships.Domain.DTO;
using AccountingScholarships.Domain.Interfaces;
using AccountingScholarships.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AccountingScholarships.Infrastructure.Repositories;

public class ReferenceDataRepository : IReferenceDataRepository
{
    private readonly ApplicationDbContext _context;

    public ReferenceDataRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ReferenceDataDto> GetAllReferenceDataAsync(CancellationToken cancellationToken = default)
    {
        var institutes = await _context.Institutes
            .AsNoTracking()
            .Select(i => new InstituteDto { Id = i.Id, InstituteName = i.InstituteName, InstituteDirector = i.InstituteDirector })
            .ToListAsync(cancellationToken);

        var departments = await _context.Departments
            .Include(d => d.Institute)
            .AsNoTracking()
            .Select(d => new DepartmentDto
            {
                Id = d.Id,
                DepartmentName = d.DepartmentName,
                DepartmentHead = d.DepartmentHead,
                InstituteId = d.InstituteId,
                InstituteName = d.Institute.InstituteName
            })
            .ToListAsync(cancellationToken);

        var specialities = await _context.Specialities
            .Include(s => s.Department)
                .ThenInclude(d => d.Institute)
            .AsNoTracking()
            .Select(s => new SpecialityDto
            {
                Id = s.Id,
                SpecialityName = s.SpecialityName,
                DepartmentId = s.DepartmentId,
                DepartmentName = s.Department.DepartmentName,
                InstituteId = s.Department.InstituteId,
                InstituteName = s.Department.Institute.InstituteName
            })
            .ToListAsync(cancellationToken);

        var studyForms = await _context.StudyForms
            .AsNoTracking()
            .Select(sf => new StudyFormDto { Id = sf.Id, StudyFormName = sf.StudyFormName })
            .ToListAsync(cancellationToken);

        var degreeLevels = await _context.DegreeLevels
            .AsNoTracking()
            .Select(dl => new DegreeLevelDto { Id = dl.Id, DegreeName = dl.DegreeName })
            .ToListAsync(cancellationToken);

        var banks = await _context.Banks
            .AsNoTracking()
            .Select(b => new BankDto { Id = b.Id, RecipientBank = b.RecipientBank, Bic = b.Bic })
            .ToListAsync(cancellationToken);

        var scholarshipTypes = await _context.ScholarshipTypes
            .AsNoTracking()
            .Select(st => new ScholarshipTypeDto { Id = st.Id, ScholarshipName = st.ScholarshipName })
            .ToListAsync(cancellationToken);

        return new ReferenceDataDto
        {
            Institutes = institutes,
            Departments = departments,
            Specialities = specialities,
            StudyForms = studyForms,
            DegreeLevels = degreeLevels,
            Banks = banks,
            ScholarshipTypes = scholarshipTypes
        };
    }
}
