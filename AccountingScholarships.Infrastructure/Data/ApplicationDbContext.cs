using AccountingScholarships.Domain;
using AccountingScholarships.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AccountingScholarships.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Student> Students => Set<Student>();
    public DbSet<Grant> Grants => Set<Grant>();
    public DbSet<Scholarship> Scholarships => Set<Scholarship>();
    public DbSet<User> Users => Set<User>();
    public DbSet<EpvoPosrednik> EpvoPosredniki => Set<EpvoPosrednik>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.IIN).IsUnique();
            entity.HasIndex(e => e.Email);
            entity.Property(e => e.FirstName).HasMaxLength(100).IsRequired();
            entity.Property(e => e.LastName).HasMaxLength(100).IsRequired();
            entity.Property(e => e.MiddleName).HasMaxLength(100);
            entity.Property(e => e.IIN).HasMaxLength(12).IsRequired();
            entity.Property(e => e.Email).HasMaxLength(200).IsRequired();
            entity.Property(e => e.Phone).HasMaxLength(20).IsRequired();
            entity.Property(e => e.GroupName).HasMaxLength(50);
            entity.Property(e => e.Faculty).HasMaxLength(200);
            entity.Property(e => e.iban).HasMaxLength(20).IsRequired();
            entity.HasIndex(e => e.iban).IsUnique();
            entity.Property(e => e.Speciality).HasMaxLength(200);
            entity.Property(e => e.EducationForm).HasMaxLength(50);
        });

        modelBuilder.Entity<Grant>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.StudentId);
            entity.Property(e => e.Name).HasMaxLength(200).IsRequired();
            entity.Property(e => e.Type).HasMaxLength(100).IsRequired();
            entity.Property(e => e.Amount).HasPrecision(18, 2);
            entity.HasOne(e => e.Student)
                  .WithMany(s => s.Grants)
                  .HasForeignKey(e => e.StudentId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Scholarship>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.StudentId);
            entity.Property(e => e.Name).HasMaxLength(200).IsRequired();
            entity.Property(e => e.Type).HasMaxLength(100);
            entity.Property(e => e.Amount).HasPrecision(18, 2);
            entity.HasOne(e => e.Student)
                  .WithMany(s => s.Scholarships)
                  .HasForeignKey(e => e.StudentId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Username).IsUnique();
            entity.HasIndex(e => e.Email);
            entity.Property(e => e.Username).HasMaxLength(100).IsRequired();
            entity.Property(e => e.PasswordHash).HasMaxLength(500).IsRequired();
            entity.Property(e => e.Email).HasMaxLength(200).IsRequired();
            entity.Property(e => e.Role).HasMaxLength(50).IsRequired();
        });

        modelBuilder.Entity<EpvoPosrednik>(entity =>
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
            entity.Property(e => e.iban).HasMaxLength(20).IsRequired();
            entity.HasIndex(e => e.iban).IsUnique();
        });
    }
}
