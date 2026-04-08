using AccountingScholarships.Domain.Entities.Real.epvosso;
using Microsoft.EntityFrameworkCore;

namespace AccountingScholarships.Infrastructure.Data
{
    public class EpvoSsoDbContext : DbContext
    {
        public EpvoSsoDbContext(DbContextOptions<EpvoSsoDbContext> options)
            : base(options) { }

        public DbSet<Profession> Professions => Set<Profession>();
        public DbSet<Student> Students => Set<Student>();
        public DbSet<Student_Info> Student_Info => Set<Student_Info>();
        public DbSet<Scholarship> Scholarships => Set<Scholarship>();
        public DbSet<Scholarship_new> Scholarship_new => Set<Scholarship_new>();
        public DbSet<University> Universities => Set<University>();
        public DbSet<SpecialitiesEpvo> SpecialitiesEpvo => Set<SpecialitiesEpvo>();
        public DbSet<SpecialitiesEpvoNew> SpecialitiesEpvoNew => Set<SpecialitiesEpvoNew>();
        public DbSet<Specializations> Specializations => Set<Specializations>();
        public DbSet<Study_forms> Study_forms => Set<Study_forms>();
        public DbSet<Studycalendar> Studycalendars => Set<Studycalendar>();
        public DbSet<Faculties> Faculties => Set<Faculties>();

        // Новые таблицы
        public DbSet<Center_Kato> Center_Kato => Set<Center_Kato>();
        public DbSet<Center_Countries> Center_Countries => Set<Center_Countries>();
        public DbSet<Center_Nationalities> Center_Nationalities => Set<Center_Nationalities>();

        public DbSet<Maritalstates> Maritalstates => Set<Maritalstates>();
        public DbSet<Nationalities> Nationalities => Set<Nationalities>();
        public DbSet<StudyLanguages> StudyLanguages => Set<StudyLanguages>();
        public DbSet<Student_Sso> Student_Sso => Set<Student_Sso>();
        public DbSet<Student_Temp> Student_Temp => Set<Student_Temp>();
        public DbSet<StudentSyncLog> StudentSyncLogs => Set<StudentSyncLog>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Profession>(e =>
            {
                e.HasKey(x => x.ProfessionId);
                e.ToTable("PROFESSION");
            });

            modelBuilder.Entity<Student_Info>(e =>
            {
                e.HasKey(x => x.UniversityId);
                e.ToTable("STUDENT_INFO");
            });

            modelBuilder.Entity<Student>(e =>
            {
                e.HasKey(x => x.StudentId);
                e.ToTable("STUDENT");
            });

            modelBuilder.Entity<Scholarship>(e =>
            {
                e.HasKey(x => x.Id);
                e.ToTable("SCHOLARSHIP");
            });

            modelBuilder.Entity<Scholarship_new>(e =>
            {
                e.HasKey(x => x.Id);
                e.ToTable("SCHOLARSHIP_NEW");
            });

            modelBuilder.Entity<University>(e =>
            {
                e.HasKey(x => x.UniversityId);
                e.ToTable("UNIVERSITY");
            });

            modelBuilder.Entity<Studycalendar>(e =>
            {
                e.HasKey(x => x.StudyCalendarId);
                e.ToTable("STUDYCALENDAR");
            });

            modelBuilder.Entity<Specializations>(e =>
            {
                e.HasKey(x => x.Id);
                e.ToTable("SPECIALIZATIONS");
            });

            modelBuilder.Entity<Study_forms>(e =>
            {
                e.HasKey(x => x.Id);
                e.ToTable("STUDY_FORMS");
            });

            modelBuilder.Entity<SpecialitiesEpvoNew>(e =>
            {
                e.HasKey(x => x.Id);
                e.ToTable("SPECIALITIES_EPVO_2025");
            });

            modelBuilder.Entity<SpecialitiesEpvo>(e =>
            {
                e.HasKey(x => x.Id);
                e.ToTable("SPECIALITIES_EPVO");
            });

            modelBuilder.Entity<Faculties>(e =>
            {
                e.HasKey(x => x.FacultyId);
                e.ToTable("FACULTIES");
            });

            // Новые таблицы
            modelBuilder.Entity<Center_Kato>(e =>
            {
                e.HasKey(x => x.Id);
                e.ToTable("CENTER_KATO");
            });

            modelBuilder.Entity<Center_Countries>(e =>
            {
                e.HasKey(x => x.Id);
                e.ToTable("center_countries");
            });

            modelBuilder.Entity<Center_Nationalities>(e =>
            {
                e.HasKey(x => x.Id);
                e.ToTable("center_nationalities");
            });

            modelBuilder.Entity<Maritalstates>(e =>
            {
                e.HasKey(x => x.Id);
                e.ToTable("MARITALSTATES");
            });

            modelBuilder.Entity<Nationalities>(e =>
            {
                e.HasKey(x => x.Id);
                e.ToTable("NATIONALITIES");
            });

            modelBuilder.Entity<StudyLanguages>(e =>
            {
                e.HasKey(x => x.Id);
                e.ToTable("STUDYLANGUAGES");
            });

            modelBuilder.Entity<Student_Sso>(e =>
            {
                e.HasKey(x => x.StudentId);
                e.ToTable("STUDENT_SSO");
            });

            modelBuilder.Entity<Student_Temp>(e =>
            {
                e.HasKey(x => x.StudentId);
                e.ToTable("STUDENT_TEMP");
            });

            modelBuilder.Entity<StudentSyncLog>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Id).ValueGeneratedOnAdd();
                e.Property(x => x.Status).HasMaxLength(20);
                e.Property(x => x.IinPlt).HasMaxLength(12);
                e.Property(x => x.EpvoEndpoint).HasMaxLength(500);
                e.Property(x => x.TriggeredBy).HasMaxLength(256);
                e.ToTable("STUDENT_SYNC_LOG");
            });
        }
    }
}
