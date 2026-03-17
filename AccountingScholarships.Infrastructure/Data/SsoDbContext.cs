using AccountingScholarships.Domain.Entities.university;
using Microsoft.EntityFrameworkCore;

namespace AccountingScholarships.Infrastructure.Data;

public class SsoDbContext : DbContext
{
    public SsoDbContext(DbContextOptions<SsoDbContext> options) : base(options) { }

    // Основные таблицы
    public DbSet<EduUsers> EduUsers => Set<EduUsers>();
    public DbSet<EduStudents> EduStudents => Set<EduStudents>();
    public DbSet<EduEmployees> EduEmployees => Set<EduEmployees>();
    public DbSet<EduEmployeePositions> EduEmployeePositions => Set<EduEmployeePositions>();

    // Справочники
    public DbSet<EduAcademicStatuses> EduAcademicStatuses => Set<EduAcademicStatuses>();
    public DbSet<EduCitizenCategories> EduCitizenCategories => Set<EduCitizenCategories>();
    public DbSet<EduCountries> EduCountries => Set<EduCountries>();
    public DbSet<EduEducationDurations> EduEducationDurations => Set<EduEducationDurations>();
    public DbSet<EduEducationPaymentTypes> EduEducationPaymentTypes => Set<EduEducationPaymentTypes>();
    public DbSet<EduEducationTypes> EduEducationTypes => Set<EduEducationTypes>();
    public DbSet<EduGrantTypes> EduGrantTypes => Set<EduGrantTypes>();
    public DbSet<EduLanguages> EduLanguages => Set<EduLanguages>();
    public DbSet<EduMaritalStatuses> EduMaritalStatuses => Set<EduMaritalStatuses>();
    public DbSet<EduMessengers> EduMessengers => Set<EduMessengers>();
    public DbSet<EduNationalities> EduNationalities => Set<EduNationalities>();
    public DbSet<EduPositionCategories> EduPositionCategories => Set<EduPositionCategories>();
    public DbSet<EduPositions> EduPositions => Set<EduPositions>();
    public DbSet<EduSpecialityLevels> EduSpecialityLevels => Set<EduSpecialityLevels>();
    public DbSet<Edu_OrgUnitTypes> EduOrgUnitTypes => Set<Edu_OrgUnitTypes>();
    public DbSet<Edu_OrgUnits> EduOrgUnits => Set<Edu_OrgUnits>();

    // Новые таблицы
    public DbSet<Edu_DocumentIssueOrgs> EduDocumentIssueOrgs => Set<Edu_DocumentIssueOrgs>();
    public DbSet<Edu_SchoolSubjects> EduSchoolSubjects => Set<Edu_SchoolSubjects>();
    public DbSet<Edu_Specialities> EduSpecialities => Set<Edu_Specialities>();
    public DbSet<Edu_UserDocumentTypes> EduUserDocumentTypes => Set<Edu_UserDocumentTypes>();
    public DbSet<Edu_UserDocuments> EduUserDocuments => Set<Edu_UserDocuments>();
    public DbSet<Edu_Rups> EduRups => Set<Edu_Rups>();
    public DbSet<Edu_RupAlgorithms> EduRupAlgorithms => Set<Edu_RupAlgorithms>();
    public DbSet<Edu_StudentStatuses> EduStudentStatuses => Set<Edu_StudentStatuses>();
    public DbSet<Edu_StudentCategories> EduStudentCategories => Set<Edu_StudentCategories>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ═══════════════════════════════════════════════════════════════
        // EduUsers
        // ═══════════════════════════════════════════════════════════════
        modelBuilder.Entity<EduUsers>(entity =>
        {
            entity.HasKey(e => e.ID);
            entity.Property(e => e.PhotoFileName).HasColumnType("varchar(255)");

            entity.HasOne(e => e.Nationality)
                  .WithMany(n => n.Users)
                  .HasForeignKey(e => e.NationalityID)
                  .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(e => e.MaritalStatus)
                  .WithMany(m => m.Users)
                  .HasForeignKey(e => e.MaritalStatusID)
                  .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(e => e.MessengerType)
                  .WithMany(m => m.Users)
                  .HasForeignKey(e => e.MessengerTypeID)
                  .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(e => e.CitizenshipCountry)
                  .WithMany(c => c.Users)
                  .HasForeignKey(e => e.CitizenshipCountryID)
                  .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(e => e.CitizenCategory)
                  .WithMany(c => c.Users)
                  .HasForeignKey(e => e.CitizenCategoryID)
                  .OnDelete(DeleteBehavior.SetNull);
        });

        // ═══════════════════════════════════════════════════════════════
        // EduStudents (StudentID = PK и FK к EduUsers.ID)
        // ═══════════════════════════════════════════════════════════════
        modelBuilder.Entity<EduStudents>(entity =>
        {
            entity.HasKey(e => e.StudentID);

            entity.HasOne(e => e.User)
                  .WithOne(u => u.Student)
                  .HasForeignKey<EduStudents>(e => e.StudentID)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.EducationType)
                  .WithMany(t => t.Students)
                  .HasForeignKey(e => e.EducationTypeID)
                  .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(e => e.EducationPaymentType)
                  .WithMany(t => t.Students)
                  .HasForeignKey(e => e.EducationPaymentTypeID)
                  .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(e => e.GrantType)
                  .WithMany(g => g.Students)
                  .HasForeignKey(e => e.GrantTypeID)
                  .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(e => e.EducationDuration)
                  .WithMany(d => d.Students)
                  .HasForeignKey(e => e.EducationDurationID)
                  .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(e => e.StudyLanguage)
                  .WithMany(l => l.Students)
                  .HasForeignKey(e => e.StudyLanguageID)
                  .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(e => e.AcademicStatus)
                  .WithMany(a => a.Students)
                  .HasForeignKey(e => e.AcademicStatusID)
                  .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(e => e.Advisor)
                  .WithMany(a => a.AdvisedStudents)
                  .HasForeignKey(e => e.AdvisorID)
                  .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(e => e.Speciality)
                  .WithMany(s => s.Students)
                  .HasForeignKey(e => e.SpecialityID)
                  .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(e => e.Rup)
                  .WithMany(r => r.Students)
                  .HasForeignKey(e => e.RupID)
                  .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(e => e.Status)
                  .WithMany(s => s.Students)
                  .HasForeignKey(e => e.StatusID)
                  .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(e => e.Category)
                  .WithMany(c => c.Students)
                  .HasForeignKey(e => e.CategoryID)
                  .OnDelete(DeleteBehavior.SetNull);
        });

        // ═══════════════════════════════════════════════════════════════
        // EduEmployees (ID = PK и FK к EduUsers.ID)
        // ═══════════════════════════════════════════════════════════════
        modelBuilder.Entity<EduEmployees>(entity =>
        {
            entity.HasKey(e => e.ID);

            entity.HasOne(e => e.User)
                  .WithOne(u => u.Employee)
                  .HasForeignKey<EduEmployees>(e => e.ID)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // ═══════════════════════════════════════════════════════════════
        // EduEmployeePositions
        // ═══════════════════════════════════════════════════════════════
        modelBuilder.Entity<EduEmployeePositions>(entity =>
        {
            entity.HasKey(e => e.ID);

            entity.HasOne(e => e.Employee)
                  .WithMany(emp => emp.Positions)
                  .HasForeignKey(e => e.EmployeeID)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.OrgUnit)
                  .WithMany(o => o.EmployeePositions)
                  .HasForeignKey(e => e.OrgUnitID)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Position)
                  .WithMany(p => p.EmployeePositions)
                  .HasForeignKey(e => e.PositionID)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // ═══════════════════════════════════════════════════════════════
        // EduEducationDurations
        // ═══════════════════════════════════════════════════════════════
        modelBuilder.Entity<EduEducationDurations>(entity =>
        {
            entity.HasKey(e => e.ID);
            entity.Property(e => e.NoBDIId).HasColumnType("nchar(100)");

            entity.HasOne(e => e.Level)
                  .WithMany(l => l.EducationDurations)
                  .HasForeignKey(e => e.LevelID)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // ═══════════════════════════════════════════════════════════════
        // EduPositions
        // ═══════════════════════════════════════════════════════════════
        modelBuilder.Entity<EduPositions>(entity =>
        {
            entity.HasKey(e => e.ID);

            entity.HasOne(e => e.Category)
                  .WithMany(c => c.Positions)
                  .HasForeignKey(e => e.CategoryID)
                  .OnDelete(DeleteBehavior.SetNull);
        });

        // ═══════════════════════════════════════════════════════════════
        // Edu_OrgUnits
        // ═══════════════════════════════════════════════════════════════
        modelBuilder.Entity<Edu_OrgUnits>(entity =>
        {
            entity.HasKey(e => e.ID);

            entity.HasOne(e => e.Type)
                  .WithMany(t => t.OrgUnits)
                  .HasForeignKey(e => e.TypeID)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Parent)
                  .WithMany(p => p.Children)
                  .HasForeignKey(e => e.ParentID)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // ═══════════════════════════════════════════════════════════════
        // Edu_Specialities (НОВАЯ)
        // ═══════════════════════════════════════════════════════════════
        modelBuilder.Entity<Edu_Specialities>(entity =>
        {
            entity.HasKey(e => e.ID);

            entity.HasOne(e => e.Level)
                  .WithMany(l => l.Specialities)
                  .HasForeignKey(e => e.LevelID)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.PrimarySubject)
                  .WithMany(s => s.PrimarySubjectSpecialities)
                  .HasForeignKey(e => e.PrimarySubjectID)
                  .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(e => e.FithSubject)
                  .WithMany(s => s.FithSubjectSpecialities)
                  .HasForeignKey(e => e.FithSubjectID)
                  .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(e => e.RupEditorOrgUnit)
                  .WithMany(o => o.Edu_Specialities)
                  .HasForeignKey(e => e.RupEditorOrgUnitID)
                  .OnDelete(DeleteBehavior.SetNull);
        });

        // ═══════════════════════════════════════════════════════════════
        // Edu_UserDocuments (НОВАЯ)
        // ═══════════════════════════════════════════════════════════════
        modelBuilder.Entity<Edu_UserDocuments>(entity =>
        {
            entity.HasKey(e => e.ID);

            entity.HasOne(e => e.User)
                  .WithMany(u => u.Documents)
                  .HasForeignKey(e => e.UserID)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.DocumentType)
                  .WithMany(t => t.Documents)
                  .HasForeignKey(e => e.DocumentTypeID)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.IssuedByOrg)
                  .WithMany(o => o.Documents)
                  .HasForeignKey(e => e.IssuedByID)
                  .OnDelete(DeleteBehavior.SetNull);
        });

        // ═══════════════════════════════════════════════════════════════
        // Edu_Rups
        // ═══════════════════════════════════════════════════════════════
        modelBuilder.Entity<Edu_Rups>(entity =>
        {
            entity.HasKey(e => e.ID);

            entity.HasOne(e => e.Algorithm)
                  .WithMany(a => a.Rups)
                  .HasForeignKey(e => e.AlgorithmID)
                  .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(e => e.EducationDuration)
                  .WithMany()
                  .HasForeignKey(e => e.EducationDurationID)
                  .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(e => e.Speciality)
                  .WithMany(s => s.Rups)
                  .HasForeignKey(e => e.SpecialityID)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // ═══════════════════════════════════════════════════════════════
        // Простые справочники — только Primary Key
        // ═══════════════════════════════════════════════════════════════
        modelBuilder.Entity<EduAcademicStatuses>(e => e.HasKey(x => x.ID));
        modelBuilder.Entity<EduCitizenCategories>(e => e.HasKey(x => x.ID));
        modelBuilder.Entity<EduCountries>(e => e.HasKey(x => x.ID));
        modelBuilder.Entity<EduEducationPaymentTypes>(e =>
        {
            e.HasKey(x => x.ID);
            e.Property(x => x.NoBDID).HasColumnType("nchar(50)");
        });
        modelBuilder.Entity<EduEducationTypes>(e => e.HasKey(x => x.ID));
        modelBuilder.Entity<EduGrantTypes>(e => e.HasKey(x => x.ID));
        modelBuilder.Entity<EduLanguages>(e => e.HasKey(x => x.ID));
        modelBuilder.Entity<EduMaritalStatuses>(e => e.HasKey(x => x.ID));
        modelBuilder.Entity<EduMessengers>(e => e.HasKey(x => x.ID));
        modelBuilder.Entity<EduNationalities>(e => e.HasKey(x => x.ID));
        modelBuilder.Entity<EduPositionCategories>(e => e.HasKey(x => x.ID));
        modelBuilder.Entity<EduSpecialityLevels>(e => e.HasKey(x => x.ID));
        modelBuilder.Entity<Edu_OrgUnitTypes>(e => e.HasKey(x => x.ID));
        modelBuilder.Entity<Edu_DocumentIssueOrgs>(e => e.HasKey(x => x.ID));
        modelBuilder.Entity<Edu_SchoolSubjects>(e => e.HasKey(x => x.ID));
        modelBuilder.Entity<Edu_UserDocumentTypes>(e => e.HasKey(x => x.ID));
        modelBuilder.Entity<Edu_StudentStatuses>(e => e.HasKey(x => x.ID));
        modelBuilder.Entity<Edu_StudentCategories>(e => e.HasKey(x => x.ID));
        modelBuilder.Entity<Edu_RupAlgorithms>(e => e.HasKey(x => x.ID));

        // ═══════════════════════════════════════════════════════════════
        // Имена таблиц в MSSQL базе
        // ═══════════════════════════════════════════════════════════════
        modelBuilder.Entity<EduUsers>().ToTable("Edu_Users");
        modelBuilder.Entity<EduStudents>().ToTable("Edu_Students");
        modelBuilder.Entity<EduEmployees>().ToTable("Edu_Employees");
        modelBuilder.Entity<EduEmployeePositions>().ToTable("Edu_EmployeePositions");
        modelBuilder.Entity<EduAcademicStatuses>().ToTable("Edu_AcademicStatuses");
        modelBuilder.Entity<EduCitizenCategories>().ToTable("Edu_CitizenCategories");
        modelBuilder.Entity<EduCountries>().ToTable("Edu_Countries");
        modelBuilder.Entity<EduEducationDurations>().ToTable("Edu_EducationDurations");
        modelBuilder.Entity<EduEducationPaymentTypes>().ToTable("Edu_EducationPaymentTypes");
        modelBuilder.Entity<EduEducationTypes>().ToTable("Edu_EducationTypes");
        modelBuilder.Entity<EduGrantTypes>().ToTable("Edu_GrantTypes");
        modelBuilder.Entity<EduLanguages>().ToTable("Edu_Languages");
        modelBuilder.Entity<EduMaritalStatuses>().ToTable("Edu_MaritalStatuses");
        modelBuilder.Entity<EduMessengers>().ToTable("Edu_Messengers");
        modelBuilder.Entity<EduNationalities>().ToTable("Edu_Nationalities");
        modelBuilder.Entity<EduPositionCategories>().ToTable("Edu_PositionCategories");
        modelBuilder.Entity<EduPositions>().ToTable("Edu_Positions");
        modelBuilder.Entity<EduSpecialityLevels>().ToTable("Edu_SpecialityLevels");
        modelBuilder.Entity<Edu_OrgUnits>().ToTable("Edu_OrgUnits");
        modelBuilder.Entity<Edu_OrgUnitTypes>().ToTable("Edu_OrgUnitTypes");
        modelBuilder.Entity<Edu_DocumentIssueOrgs>().ToTable("Edu_DocumentIssueOrgs");
        modelBuilder.Entity<Edu_SchoolSubjects>().ToTable("Edu_SchoolSubjects");
        modelBuilder.Entity<Edu_Specialities>().ToTable("Edu_Specialities");
        modelBuilder.Entity<Edu_UserDocumentTypes>().ToTable("Edu_UserDocumentTypes");
        modelBuilder.Entity<Edu_UserDocuments>().ToTable("Edu_UserDocuments");
        modelBuilder.Entity<Edu_Rups>().ToTable("Edu_Rups");
        modelBuilder.Entity<Edu_RupAlgorithms>().ToTable("Edu_RupAlgorithms");
        modelBuilder.Entity<Edu_StudentStatuses>().ToTable("Edu_StudentStatuses");
        modelBuilder.Entity<Edu_StudentCategories>().ToTable("Edu_StudentCategories");
    }
}
