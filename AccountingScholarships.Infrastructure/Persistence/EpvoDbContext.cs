using AccountingScholarships.Domain.Entities.Epvo;
using Microsoft.EntityFrameworkCore;

namespace AccountingScholarships.Infrastructure.Persistence;

public class EpvoDbContext : DbContext
{
    public EpvoDbContext(DbContextOptions<EpvoDbContext> options) : base(options) { }

    public DbSet<EpvoStudent> EpvoStudents => Set<EpvoStudent>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<EpvoStudent>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.IIN).IsUnique();
            entity.Property(e => e.FirstName).HasMaxLength(100).IsRequired();
            entity.Property(e => e.LastName).HasMaxLength(100).IsRequired();
            entity.Property(e => e.MiddleName).HasMaxLength(100);
            entity.Property(e => e.IIN).HasMaxLength(12).IsRequired();
            entity.Property(e => e.Faculty).HasMaxLength(200);
            entity.Property(e => e.Speciality).HasMaxLength(200);
            entity.Property(e => e.GrantName).HasMaxLength(200);
            entity.Property(e => e.GrantAmount).HasPrecision(18, 2);
            entity.Property(e => e.ScholarshipName).HasMaxLength(200);
            entity.Property(e => e.ScholarshipAmount).HasPrecision(18, 2);
        });
    }
}
