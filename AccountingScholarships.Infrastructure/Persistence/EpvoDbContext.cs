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

        SeedData(modelBuilder);
    }

    private static void SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EpvoStudent>().HasData(
            new EpvoStudent
            {
                Id = Guid.Parse("a1b2c3d4-e5f6-7890-abcd-ef1234567890"),
                FirstName = "Алихан",
                LastName = "Сериков",
                MiddleName = "Бауржанович",
                IIN = "030515500123",
                DateOfBirth = new DateTime(2003, 5, 15),
                Faculty = "Информационные технологии",
                Speciality = "Программная инженерия",
                Course = 4,
                GrantName = "Государственный грант",
                GrantAmount = 500000m,
                ScholarshipName = "Академическая стипендия",
                ScholarshipAmount = 36660m,
                IsActive = true,
                SyncDate = new DateTime(2024, 9, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new EpvoStudent
            {
                Id = Guid.Parse("b2c3d4e5-f6a7-8901-bcde-f12345678901"),
                FirstName = "Айгерим",
                LastName = "Нурланова",
                MiddleName = "Ерлановна",
                IIN = "040820600456",
                DateOfBirth = new DateTime(2004, 8, 20),
                Faculty = "Экономика",
                Speciality = "Финансы",
                Course = 3,
                GrantName = "Государственный грант",
                GrantAmount = 450000m,
                ScholarshipName = "Повышенная стипендия",
                ScholarshipAmount = 50000m,
                IsActive = true,
                SyncDate = new DateTime(2024, 9, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new EpvoStudent
            {
                Id = Guid.Parse("c3d4e5f6-a7b8-9012-cdef-123456789012"),
                FirstName = "Дамир",
                LastName = "Касымов",
                IIN = "020110500789",
                DateOfBirth = new DateTime(2002, 1, 10),
                Faculty = "Юриспруденция",
                Speciality = "Правоведение",
                Course = 2,
                GrantName = "Государственный грант",
                GrantAmount = 400000m,
                IsActive = true,
                SyncDate = new DateTime(2024, 9, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new EpvoStudent
            {
                Id = Guid.Parse("d4e5f6a7-b8c9-0123-defa-234567890123"),
                FirstName = "Мадина",
                LastName = "Абдрахманова",
                MiddleName = "Канатовна",
                IIN = "050305700234",
                DateOfBirth = new DateTime(2005, 3, 5),
                Faculty = "Информационные технологии",
                Speciality = "Информационная безопасность",
                Course = 1,
                IsActive = true,
                SyncDate = new DateTime(2024, 9, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new EpvoStudent
            {
                Id = Guid.Parse("e5f6a7b8-c9d0-1234-efab-345678901234"),
                FirstName = "Нурсултан",
                LastName = "Токаев",
                MiddleName = "Маратович",
                IIN = "010725500567",
                DateOfBirth = new DateTime(2001, 7, 25),
                Faculty = "Медицина",
                Speciality = "Общая медицина",
                Course = 4,
                GrantName = "Государственный грант",
                GrantAmount = 600000m,
                ScholarshipName = "Академическая стипендия",
                ScholarshipAmount = 36660m,
                IsActive = false,
                SyncDate = new DateTime(2024, 9, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}
