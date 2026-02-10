using AccountingScholarships.Domain.Entities.Epvo;
using AccountingScholarships.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AccountingScholarships.Infrastructure.Data;

public static class EpvoDbContextSeed
{
    public static async Task SeedAsync(EpvoDbContext context)
    {
        if (await context.EpvoStudents.AnyAsync())
            return;

        var students = new List<EpvoStudent>
        {
            new()
            {
                Id = Guid.NewGuid(),
                FirstName = "Айдар",
                LastName = "Нурланов",
                MiddleName = "Ерланович",
                IIN = "990815350123",
                DateOfBirth = new DateTime(1999, 8, 15),
                Faculty = "Информационные технологии",
                Speciality = "Компьютерные науки",
                Course = 4,
                GrantName = "Образовательный грант",
                GrantAmount = 550000.00m,
                ScholarshipName = "Академическая стипендия",
                ScholarshipAmount = 36000.00m,
                IsActive = true,
                SyncDate = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                FirstName = "Жанар",
                LastName = "Серикова",
                MiddleName = "Асановна",
                IIN = "001205450789",
                DateOfBirth = new DateTime(2000, 12, 5),
                Faculty = "Экономика и бизнес",
                Speciality = "Финансы",
                Course = 3,
                GrantName = "Образовательный грант",
                GrantAmount = 500000.00m,
                ScholarshipName = "Социальная стипендия",
                ScholarshipAmount = 24000.00m,
                IsActive = true,
                SyncDate = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                FirstName = "Дархан",
                LastName = "Қайратұлы",
                MiddleName = null,
                IIN = "011030550234",
                DateOfBirth = new DateTime(2001, 10, 30),
                Faculty = "Инженерия",
                Speciality = "Машиностроение",
                Course = 2,
                GrantName = "Образовательный грант",
                GrantAmount = 520000.00m,
                ScholarshipName = null,
                ScholarshipAmount = null,
                IsActive = true,
                SyncDate = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                FirstName = "Әсем",
                LastName = "Төлеуова",
                MiddleName = "Бауыржанқызы",
                IIN = "020318650456",
                DateOfBirth = new DateTime(2002, 3, 18),
                Faculty = "Право",
                Speciality = "Юриспруденция",
                Course = 1,
                GrantName = "Образовательный грант",
                GrantAmount = 480000.00m,
                ScholarshipName = "Академическая стипендия",
                ScholarshipAmount = 36000.00m,
                IsActive = true,
                SyncDate = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                FirstName = "Нұрлан",
                LastName = "Әбдіғалиев",
                MiddleName = "Серікұлы",
                IIN = "991122350678",
                DateOfBirth = new DateTime(1999, 11, 22),
                Faculty = "Медицина",
                Speciality = "Общая медицина",
                Course = 5,
                GrantName = "Целевой грант",
                GrantAmount = 600000.00m,
                ScholarshipName = "Повышенная стипендия",
                ScholarshipAmount = 48000.00m,
                IsActive = true,
                SyncDate = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                FirstName = "Сауле",
                LastName = "Мұхтарова",
                MiddleName = "Қанатқызы",
                IIN = "000725450890",
                DateOfBirth = new DateTime(2000, 7, 25),
                Faculty = "Педагогика",
                Speciality = "Начальное образование",
                Course = 3,
                GrantName = "Образовательный грант",
                GrantAmount = 490000.00m,
                ScholarshipName = "Академическая стипендия",
                ScholarshipAmount = 36000.00m,
                IsActive = true,
                SyncDate = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                FirstName = "Ербол",
                LastName = "Тұрсынұлы",
                MiddleName = null,
                IIN = "010909550345",
                DateOfBirth = new DateTime(2001, 9, 9),
                Faculty = "Архитектура",
                Speciality = "Архитектура",
                Course = 2,
                GrantName = "Образовательный грант",
                GrantAmount = 530000.00m,
                ScholarshipName = null,
                ScholarshipAmount = null,
                IsActive = true,
                SyncDate = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                FirstName = "Гульнара",
                LastName = "Смагулова",
                MiddleName = "Нуржановна",
                IIN = "991005350123",
                DateOfBirth = new DateTime(1999, 10, 5),
                Faculty = "Информационные технологии",
                Speciality = "Информационная безопасность",
                Course = 4,
                GrantName = "Образовательный грант",
                GrantAmount = 540000.00m,
                ScholarshipName = "Академическая стипендия",
                ScholarshipAmount = 36000.00m,
                IsActive = true,
                SyncDate = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                FirstName = "Қуаныш",
                LastName = "Әлібекұлы",
                MiddleName = null,
                IIN = "020515650789",
                DateOfBirth = new DateTime(2002, 5, 15),
                Faculty = "Математика и физика",
                Speciality = "Прикладная математика",
                Course = 1,
                GrantName = "Образовательный грант",
                GrantAmount = 475000.00m,
                ScholarshipName = "Социальная стипендия",
                ScholarshipAmount = 24000.00m,
                IsActive = true,
                SyncDate = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                FirstName = "Индира",
                LastName = "Әміртаева",
                MiddleName = "Болатқызы",
                IIN = "001213450456",
                DateOfBirth = new DateTime(2000, 12, 13),
                Faculty = "Международные отношения",
                Speciality = "Международные отношения",
                Course = 3,
                GrantName = "Образовательный грант",
                GrantAmount = 510000.00m,
                ScholarshipName = "Академическая стипендия",
                ScholarshipAmount = 36000.00m,
                IsActive = true,
                SyncDate = DateTime.UtcNow
            }
        };

        await context.EpvoStudents.AddRangeAsync(students);
        await context.SaveChangesAsync();
    }
}
