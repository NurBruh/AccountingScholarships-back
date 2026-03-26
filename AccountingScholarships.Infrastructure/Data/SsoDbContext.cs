using AccountingScholarships.Domain.Entities.Real.university;
using Microsoft.EntityFrameworkCore;

namespace AccountingScholarships.Infrastructure.Data;

public class SsoDbContext : DbContext
{
    public SsoDbContext(DbContextOptions<SsoDbContext> options) : base(options) { }

    // Основные таблицы
    public DbSet<Edu_Users> Edu_Users => Set<Edu_Users>();
    public DbSet<Edu_Students> Edu_Students => Set<Edu_Students>();
    public DbSet<Edu_Employees> Edu_Employees => Set<Edu_Employees>();
    public DbSet<Edu_EmployeePositions> Edu_EmployeePositions => Set<Edu_EmployeePositions>();

    // Справочники
    public DbSet<Edu_AcademicStatuses> Edu_AcademicStatuses => Set<Edu_AcademicStatuses>();
    public DbSet<Edu_CitizenCategories> Edu_CitizenCategories => Set<Edu_CitizenCategories>();
    public DbSet<Edu_Countries> Edu_Countries => Set<Edu_Countries>();
    public DbSet<Edu_EducationDurations> Edu_EducationDurations => Set<Edu_EducationDurations>();
    public DbSet<Edu_EducationPaymentTypes> Edu_EducationPaymentTypes => Set<Edu_EducationPaymentTypes>();
    public DbSet<Edu_EducationTypes> Edu_EducationTypes => Set<Edu_EducationTypes>();
    public DbSet<Edu_GrantTypes> Edu_GrantTypes => Set<Edu_GrantTypes>();
    public DbSet<Edu_Languages> Edu_Languages => Set<Edu_Languages>();
    public DbSet<Edu_MaritalStatuses> Edu_MaritalStatuses => Set<Edu_MaritalStatuses>();
    public DbSet<Edu_Messengers> Edu_Messengers => Set<Edu_Messengers>();
    public DbSet<Edu_Nationalities> Edu_Nationalities => Set<Edu_Nationalities>();
    public DbSet<Edu_PositionCategories> Edu_PositionCategories => Set<Edu_PositionCategories>();
    public DbSet<Edu_Positions> Edu_Positions => Set<Edu_Positions>();
    public DbSet<Edu_SpecialityLevels> Edu_SpecialityLevels => Set<Edu_SpecialityLevels>();
    public DbSet<Edu_OrgUnitTypes> Edu_OrgUnitTypes => Set<Edu_OrgUnitTypes>();
    public DbSet<Edu_OrgUnits> Edu_OrgUnits => Set<Edu_OrgUnits>();

    // Новые таблицы
    public DbSet<Edu_DocumentIssueOrgs> Edu_DocumentIssueOrgs => Set<Edu_DocumentIssueOrgs>();
    public DbSet<Edu_SchoolSubjects> Edu_SchoolSubjects => Set<Edu_SchoolSubjects>();
    public DbSet<Edu_Specialities> Edu_Specialities => Set<Edu_Specialities>();
    public DbSet<Edu_UserDocumentTypes> Edu_UserDocumentTypes => Set<Edu_UserDocumentTypes>();
    public DbSet<Edu_UserDocuments> Edu_UserDocuments => Set<Edu_UserDocuments>();
    public DbSet<Edu_Rups> Edu_Rups => Set<Edu_Rups>();
    public DbSet<Edu_RupAlgorithms> Edu_RupAlgorithms => Set<Edu_RupAlgorithms>();
    public DbSet<Edu_StudentStatuses> Edu_StudentStatuses => Set<Edu_StudentStatuses>();
    public DbSet<Edu_StudentCategories> Edu_StudentCategories => Set<Edu_StudentCategories>();

    // Новые таблицы (созданные entity)
    public DbSet<Edu_AddressTypes> Edu_AddressTypes => Set<Edu_AddressTypes>();
    public DbSet<Edu_Localities> Edu_Localities => Set<Edu_Localities>();
    public DbSet<Edu_LocalityTypes> Edu_LocalityTypes => Set<Edu_LocalityTypes>();
    public DbSet<Obsolete_Edu_Regions> Obsolete_Edu_Regions => Set<Obsolete_Edu_Regions>();
    public DbSet<Edu_UserAddresses> Edu_UserAddresses => Set<Edu_UserAddresses>();
    public DbSet<Edu_UserEducation> Edu_UserEducation => Set<Edu_UserEducation>();
    public DbSet<Edu_EducationDocumentTypes> Edu_EducationDocumentTypes => Set<Edu_EducationDocumentTypes>();
    public DbSet<Edu_EducationDocumentSubTypes> Edu_EducationDocumentSubTypes => Set<Edu_EducationDocumentSubTypes>();
    public DbSet<Edu_Schools> Edu_Schools => Set<Edu_Schools>();
    public DbSet<Edu_SchoolTypes> Edu_SchoolTypes => Set<Edu_SchoolTypes>();
    public DbSet<Edu_SchoolRegionStatuses> Edu_SchoolRegionStatuses => Set<Edu_SchoolRegionStatuses>();
    public DbSet<Edu_StudentCourses> Edu_StudentCourses => Set<Edu_StudentCourses>();
    public DbSet<Edu_SemesterCourses> Edu_SemesterCourses => Set<Edu_SemesterCourses>();
    public DbSet<Edu_Semesters> Edu_Semesters => Set<Edu_Semesters>();
    public DbSet<Edu_SemesterTypes> Edu_SemesterTypes => Set<Edu_SemesterTypes>();
    public DbSet<Edu_ControlTypes> Edu_ControlTypes => Set<Edu_ControlTypes>();
    public DbSet<Edu_CourseTypes> Edu_CourseTypes => Set<Edu_CourseTypes>();
    public DbSet<Edu_CourseTypeDvo> Edu_CourseTypeDvo => Set<Edu_CourseTypeDvo>();
    public DbSet<Edu_Specializations> Edu_Specializations => Set<Edu_Specializations>();
    public DbSet<Edu_SpecialitySpecializations> Edu_SpecialitySpecializations => Set<Edu_SpecialitySpecializations>();
    public DbSet<Edu_Specializations_OrgUnits> Edu_Specializations_OrgUnits => Set<Edu_Specializations_OrgUnits>();
    public DbSet<Edu_Entrants> Edu_Entrants => Set<Edu_Entrants>();
    public DbSet<Edu_EntrantStatuses> Edu_EntrantStatuses => Set<Edu_EntrantStatuses>();
    public DbSet<StudentInfo_Translations> StudentInfo_Translations => Set<StudentInfo_Translations>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ═══════════════════════════════════════════════════════════════
        // Edu_Users
        // ═══════════════════════════════════════════════════════════════
        modelBuilder.Entity<Edu_Users>(entity =>
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
        // Edu_Students (StudentID = PK и FK к Edu_Users.ID)
        // ═══════════════════════════════════════════════════════════════
        modelBuilder.Entity<Edu_Students>(entity =>
        {
            entity.HasKey(e => e.StudentID);

            entity.HasOne(e => e.User)
                  .WithOne(u => u.Student)
                  .HasForeignKey<Edu_Students>(e => e.StudentID)
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
        // Edu_Employees (ID = PK и FK к Edu_Users.ID)
        // ═══════════════════════════════════════════════════════════════
        modelBuilder.Entity<Edu_Employees>(entity =>
        {
            entity.HasKey(e => e.ID);

            entity.HasOne(e => e.User)
                  .WithOne(u => u.Employee)
                  .HasForeignKey<Edu_Employees>(e => e.ID)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // ═══════════════════════════════════════════════════════════════
        // Edu_EmployeePositions
        // ═══════════════════════════════════════════════════════════════
        modelBuilder.Entity<Edu_EmployeePositions>(entity =>
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
        // Edu_EducationDurations
        // ═══════════════════════════════════════════════════════════════
        modelBuilder.Entity<Edu_EducationDurations>(entity =>
        {
            entity.HasKey(e => e.ID);
            entity.Property(e => e.NoBDIId).HasColumnType("nchar(100)");

            entity.HasOne(e => e.Level)
                  .WithMany(l => l.EducationDurations)
                  .HasForeignKey(e => e.LevelID)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // ═══════════════════════════════════════════════════════════════
        // Edu_Positions
        // ═══════════════════════════════════════════════════════════════
        modelBuilder.Entity<Edu_Positions>(entity =>
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
        modelBuilder.Entity<Edu_AcademicStatuses>(e => e.HasKey(x => x.ID));
        modelBuilder.Entity<Edu_CitizenCategories>(e => e.HasKey(x => x.ID));
        modelBuilder.Entity<Edu_Countries>(e => e.HasKey(x => x.ID));
        modelBuilder.Entity<Edu_EducationPaymentTypes>(e =>
        {
            e.HasKey(x => x.ID);
            e.Property(x => x.NoBDID).HasColumnType("nchar(50)");
        });
        modelBuilder.Entity<Edu_EducationTypes>(e => e.HasKey(x => x.ID));
        modelBuilder.Entity<Edu_GrantTypes>(e => e.HasKey(x => x.ID));
        modelBuilder.Entity<Edu_Languages>(e => e.HasKey(x => x.ID));
        modelBuilder.Entity<Edu_MaritalStatuses>(e => e.HasKey(x => x.ID));
        modelBuilder.Entity<Edu_Messengers>(e => e.HasKey(x => x.ID));
        modelBuilder.Entity<Edu_Nationalities>(e => e.HasKey(x => x.ID));
        modelBuilder.Entity<Edu_PositionCategories>(e => e.HasKey(x => x.ID));
        modelBuilder.Entity<Edu_SpecialityLevels>(e => e.HasKey(x => x.ID));
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
        modelBuilder.Entity<Edu_Users>().ToTable("Edu_Users");
        modelBuilder.Entity<Edu_Students>().ToTable("Edu_Students");
        modelBuilder.Entity<Edu_Employees>().ToTable("Edu_Employees");
        modelBuilder.Entity<Edu_EmployeePositions>().ToTable("Edu_EmployeePositions");
        modelBuilder.Entity<Edu_AcademicStatuses>().ToTable("Edu_AcademicStatuses");
        modelBuilder.Entity<Edu_CitizenCategories>().ToTable("Edu_CitizenCategories");
        modelBuilder.Entity<Edu_Countries>().ToTable("Edu_Countries");
        modelBuilder.Entity<Edu_EducationDurations>().ToTable("Edu_EducationDurations");
        modelBuilder.Entity<Edu_EducationPaymentTypes>().ToTable("Edu_EducationPaymentTypes");
        modelBuilder.Entity<Edu_EducationTypes>().ToTable("Edu_EducationTypes");
        modelBuilder.Entity<Edu_GrantTypes>().ToTable("Edu_GrantTypes");
        modelBuilder.Entity<Edu_Languages>().ToTable("Edu_Languages");
        modelBuilder.Entity<Edu_MaritalStatuses>().ToTable("Edu_MaritalStatuses");
        modelBuilder.Entity<Edu_Messengers>().ToTable("Edu_Messengers");
        modelBuilder.Entity<Edu_Nationalities>().ToTable("Edu_Nationalities");
        modelBuilder.Entity<Edu_PositionCategories>().ToTable("Edu_PositionCategories");
        modelBuilder.Entity<Edu_Positions>().ToTable("Edu_Positions");
        modelBuilder.Entity<Edu_SpecialityLevels>().ToTable("Edu_SpecialityLevels");
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
