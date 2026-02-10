using AccountingScholarships.Domain.Entities;
using AccountingScholarships.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AccountingScholarships.Infrastructure.Data;

public static class ApplicationDbContextSeed
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        // Seed будет выполнен автоматически при первом запуске приложения
        // Пользователи будут создаваться через endpoint /api/v1/Auth/register
        
        // Проверяем есть ли уже данные
        if (await context.Students.AnyAsync())
            return;

        // Можно добавить начальные тестовые данные если нужно
        // Пока оставляем пустым - данные будут создаваться через API или синхронизацию с ЕПВО
    }
}
