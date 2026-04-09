using System.Text;
using AccountingScholarships.Domain.Interfaces;
using AccountingScholarships.Infrastructure.Data;
using AccountingScholarships.Infrastructure.Repositories;
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
        var ssolocal = configuration.GetConnectionString("PcConnection");
        var epvolocal = configuration.GetConnectionString("PcEpvoConnection");

        var ssocon = configuration.GetConnectionString("MSSQLConnection");
        var epvocon = configuration.GetConnectionString("EPVOConnection1");

        services.AddDbContext<SsoDbContext>(options =>
            options.UseSqlServer(ssocon));
        services.AddDbContext<EpvoSsoDbContext>(options =>
            options.UseSqlServer(epvocon, sqlOptions =>
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

        services.AddScoped<IStoredProcedureRepository, StoredProcedureRepository>();

        return services;
    }
}
