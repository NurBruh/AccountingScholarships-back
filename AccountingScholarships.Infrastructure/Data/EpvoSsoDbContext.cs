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
        // потом добавишь:
        // public DbSet<Student> Students => Set<Student>();
        // public DbSet<University> Universities => Set<University>();
        // ...и т.д.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // ─── Profession ───────────────────────────────────────────
            modelBuilder.Entity<Profession>(e =>
            {
                e.HasKey(x => x.ProfessionId);
                e.ToTable("PROFESSION");        // dbo.PROFESSION
            });
            // потом для каждой модели:
            // modelBuilder.Entity<Student>(e => {
            //     e.HasKey(x => x.StudentId);
            //     e.ToTable("STUDENT");
            // });
        }
    }
}
