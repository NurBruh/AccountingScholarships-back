using System.Text;
using AccountingScholarships.Domain.Interfaces;
using AccountingScholarships.Domain.Common;
using AccountingScholarships.Infrastructure.Data;
using AccountingScholarships.Infrastructure.Repositories;
using AccountingScholarships.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
//using Microsoft.EntityFrameworkCore.SqlServer;

namespace AccountingScholarships.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        //MySQL
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        var epvoConnectionString = configuration.GetConnectionString("EpvoConnection");
        var ssoConnectionString = configuration.GetConnectionString("SsoConnection");
        var localeConnectionString = configuration.GetConnectionString("LocaleConnection");
        var localeEpvoConnectionString = configuration.GetConnectionString("EpvoLocaleConnection");
        
        //ServerDb

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

        services.AddDbContext<EpvoDbContext>(options =>
            options.UseMySql(epvoConnectionString, ServerVersion.AutoDetect(epvoConnectionString)));

        //services.AddDbContext<SsoDbContext>(options =>
        //    options.UseMySql(ssoConnectionString, ServerVersion.AutoDetect(ssoConnectionString)));
        
        //LocalDB

        //services.AddDbContext<ApplicationDbContext>(options =>
        //    options.UseMySql(localeConnectionString, ServerVersion.AutoDetect(localeConnectionString)));

        //services.AddDbContext<EpvoDbContext>(options =>
        //    options.UseMySql(localeEpvoConnectionString, ServerVersion.AutoDetect(localeEpvoConnectionString)));
     

        //MSSQL

        var conmssql = configuration.GetConnectionString("MSSQLConnection");
        var conepvomssql = configuration.GetConnectionString("EPVOConnection1");

        services.AddDbContext<SsoDbContext>(options =>
            options.UseSqlServer(conmssql));
        services.AddDbContext<EpvoSsoDbContext>(options =>
            options.UseSqlServer(conepvomssql));



        //#region MSSQLConnection

        //services.AddDbContext<ApplicationDbContext>(options => 
        //    options.UseSqlServer(connectionString));
        //services.AddDbContext<EpvoDbContext>(options => 
        //    options.UseSqlServer(epvoConnectionString));

        //#endregion

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<IGrantRepository, GrantRepository>();
        services.AddScoped<IScholarshipRepository, ScholarshipRepository>();
        services.AddScoped<IEpvoRepository, EpvoRepository>();
        services.AddScoped<IScholarshipLossRepository, ScholarshipLossRepository>();
        services.AddScoped<IReferenceDataRepository, ReferenceDataRepository>();
        services.AddScoped<IChangeHistoryRepository, ChangeHistoryRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IEpvoSsoRepository<>), typeof(EpvoSsoRepository<>));
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

        services.AddScoped<IEpvoApiClient, EpvoApiClient>();

        // Testing services
        services.AddScoped<IStudentExportService, StudentExportService>();

        return services;
    }
}
