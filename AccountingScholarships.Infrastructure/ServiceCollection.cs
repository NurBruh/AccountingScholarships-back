using System.Text;
using AccountingScholarships.Application.Interfaces;
using AccountingScholarships.Infrastructure.Data;
using AccountingScholarships.Infrastructure.Repositories;
using AccountingScholarships.Infrastructure.Services.StudentSync;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using AccountingScholarships.Infrastructure.Services;

namespace AccountingScholarships.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // ─── ТУМБЛЕР: DbSettings:UseLocalConnections в appsettings.json ───────────
        // true  → локальные PC (разработка)    false → сервер (продакшн)
        // Когда проект готов: убери этот блок и оставь только серверные строки
        // ──────────────────────────────────────────────────────────────────────────
        var useLocal = configuration.GetValue<bool>("DbSettings:UseLocalConnections");

        var ssoConn  = useLocal
            ? configuration.GetConnectionString("PcConnection")
            : configuration.GetConnectionString("MSSQLConnection");
        var epvoConn = useLocal
            ? configuration.GetConnectionString("PcEpvoConnection")
            : configuration.GetConnectionString("EPVOConnection1");

        services.AddDbContext<SsoDbContext>(options =>
            options.UseSqlServer(ssoConn));
        services.AddDbContext<EpvoSsoDbContext>(options =>
            options.UseSqlServer(epvoConn, sqlOptions =>
            {
                sqlOptions.CommandTimeout(300);
            }));

        services.AddScoped(typeof(IEpvoSsoRepository<>), typeof(EpvoSsoRepository<>));
        services.AddScoped<ISsoStudentDetailsRepository, SsoStudentDetailsRepository>();
        services.AddScoped<IJwtTokenService, JwtTokenService>();

        var jwtSettings = configuration.GetSection("JwtSettings");
        var key = Encoding.UTF8.GetBytes(jwtSettings["Secret"]!);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings["Issuer"],
                ValidAudience = jwtSettings["Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };
        });

        services.AddScoped<IEduStudentRepository, EduStudentRepository>();
        services.AddScoped(typeof(ISsoRepository<>), typeof(SsoRepository<>));
        services.AddScoped<IComparisonRepository, ComparisonRepository>();
        services.AddScoped<ISsoToEpvoMapperService, SsoToEpvoMapperService>();
        services.AddScoped<IChangeLogRepository, ChangeLogRepository>();
        services.AddScoped<IStoredProcedureRepository, StoredProcedureRepository>();
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}
