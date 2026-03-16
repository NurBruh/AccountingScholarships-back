using AccountingScholarships.Domain.Entities.epvosso;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingScholarships.Infrastructure.Data
{
    public class EpvoSsoDbContext : DbContext
    {
        public EpvoSsoDbContext(DbContextOptions<EpvoSsoDbContext> options)
            : base(options) { }
        // ─── DbSet для каждой модели ──────────────────────────────────
        public DbSet<Profession> Professions => Set<Profession>();
        public DbSet<Student> Students => Set<Student>();
        public DbSet<Student_Info> StudentsInfo => Set<Student_Info>();
        public DbSet<Scholarship> Scholarships => Set<Scholarship>();
        public DbSet<Scholarship_new> Scholarshipsnew => Set<Scholarship_new>();
        public DbSet<University> Universities => Set<University>();
        public DbSet<SpecialitiesEpvo> SpecialitiesEpvos => Set<SpecialitiesEpvo>();
        public DbSet<SpecialitiesEpvoNew> SpecialitiesEpvoNew => Set<SpecialitiesEpvoNew>();
        public DbSet<Specializations> Specializations => Set<Specializations>();
        public DbSet<Study_forms> StudyForms => Set<Study_forms>();
        public DbSet<Studycalendar> Studycalendars => Set<Studycalendar>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Profession>(e =>
            {
                e.HasKey(x => x.ProfessionId);
                e.ToTable("PROFESSION");
            });
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Student_Info>(e =>
            {
                e.HasKey(x => x.UniversityId);
                e.ToTable("STUDENT_INFO");
            });
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Student>(e =>
            {
                e.HasKey(x => x.StudentId);
                e.ToTable("STUDENT");
            });
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Scholarship>(e =>
            {
                e.HasKey(x => x.Id);
                e.ToTable("SCHOLARSHIP");
            });
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Scholarship_new>(e =>
            {
                e.HasKey(x => x.Id);
                e.ToTable("SCHOLARSHIP_NEW");
            });
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<University>(e =>
            {
                e.HasKey(x => x.UniversityId);
                e.ToTable("UNIVERSITY");
            });
        }
    }
}
