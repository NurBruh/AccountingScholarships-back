using AccountingScholarships.Domain.Entities;
using AccountingScholarships.Domain.Entities.Auth;
using AccountingScholarships.Domain.Entities.StudentData;
using Microsoft.EntityFrameworkCore;

namespace AccountingScholarships.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    // Основные таблицы
    public DbSet<Student> Students => Set<Student>();
    public DbSet<Grant> Grants => Set<Grant>();
    public DbSet<Scholarship> Scholarships => Set<Scholarship>();
    public DbSet<User> Users => Set<User>();
    public DbSet<ScholarshipLossRecord> ScholarshipLossRecords => Set<ScholarshipLossRecord>();
    public DbSet<ChangeHistoryRecord> ChangeHistoryRecords => Set<ChangeHistoryRecord>();

    // Справочники
    public DbSet<Institute> Institutes => Set<Institute>();
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<Speciality> Specialities => Set<Speciality>();
    public DbSet<StudyForm> StudyForms => Set<StudyForm>();
    public DbSet<DegreeLevel> DegreeLevels => Set<DegreeLevel>();
    public DbSet<Bank> Banks => Set<Bank>();
    public DbSet<ScholarshipType> ScholarshipTypes => Set<ScholarshipType>();

    // Связующие таблицы
    public DbSet<StudentGrant> StudentGrants => Set<StudentGrant>();
    public DbSet<StudentScholarship> StudentScholarships => Set<StudentScholarship>();

    // Статусы и история
    public DbSet<StatusType> StatusTypes => Set<StatusType>();
    public DbSet<StatusStudentHistory> StatusStudentHistories => Set<StatusStudentHistory>();
    public DbSet<UserActionHistory> UserActionHistories => Set<UserActionHistory>();

    // RBAC
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<Permission> Permissions => Set<Permission>();
    public DbSet<RolePermission> RolePermissions => Set<RolePermission>();
    public DbSet<UserRoleAssignment> UserRoleAssignments => Set<UserRoleAssignment>();
    public DbSet<Scope> Scopes => Set<Scope>();
    public DbSet<AccessRole> AccessRoles => Set<AccessRole>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ==================== Справочники ====================

        modelBuilder.Entity<Institute>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.InstituteName).HasMaxLength(300).IsRequired();
            entity.Property(e => e.InstituteDirector).HasMaxLength(200);
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.DepartmentName).HasMaxLength(300).IsRequired();
            entity.Property(e => e.DepartmentHead).HasMaxLength(200);
            entity.HasOne(e => e.Institute)
                  .WithMany(i => i.Departments)
                  .HasForeignKey(e => e.InstituteId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Speciality>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.SpecialityName).HasMaxLength(300).IsRequired();
            entity.HasOne(e => e.Department)
                  .WithMany(d => d.Specialities)
                  .HasForeignKey(e => e.DepartmentId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<StudyForm>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.StudyFormName).HasMaxLength(100).IsRequired();
        });

        modelBuilder.Entity<DegreeLevel>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.DegreeName).HasMaxLength(100).IsRequired();
        });

        modelBuilder.Entity<Bank>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.RecipientBank).HasMaxLength(200).IsRequired();
            entity.Property(e => e.Bic).HasMaxLength(20).IsRequired();
        });

        modelBuilder.Entity<ScholarshipType>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.ScholarshipName).HasMaxLength(200).IsRequired();
        });

        modelBuilder.Entity<StatusType>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.StatusName).HasMaxLength(100).IsRequired();
        });

        // ==================== Student ====================

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.IIN).IsUnique();
            entity.HasIndex(e => e.Email);
            entity.Property(e => e.FirstName).HasMaxLength(100).IsRequired();
            entity.Property(e => e.LastName).HasMaxLength(100).IsRequired();
            entity.Property(e => e.MiddleName).HasMaxLength(100);
            entity.Property(e => e.IIN).HasMaxLength(12).IsRequired();
            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.GroupName).HasMaxLength(50);
            entity.Property(e => e.iban).HasMaxLength(34).IsRequired();
            entity.HasIndex(e => e.iban).IsUnique();
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Sex).HasMaxLength(10);
            entity.Property(e => e.CreatedBy).HasMaxLength(100);

            entity.HasOne(e => e.Speciality)
                  .WithMany(s => s.Students)
                  .HasForeignKey(e => e.SpecialityId)
                  .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(e => e.StudyForm)
                  .WithMany(sf => sf.Students)
                  .HasForeignKey(e => e.StudyFormId)
                  .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(e => e.DegreeLevel)
                  .WithMany(dl => dl.Students)
                  .HasForeignKey(e => e.DegreeLevelId)
                  .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(e => e.Bank)
                  .WithMany(b => b.Students)
                  .HasForeignKey(e => e.BankId)
                  .OnDelete(DeleteBehavior.SetNull);
        });

        // ==================== Grant / Scholarship ====================

        modelBuilder.Entity<Grant>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.StudentId);
            entity.Property(e => e.Name).HasMaxLength(200).IsRequired();
            entity.Property(e => e.Type).HasMaxLength(100);
            entity.Property(e => e.Amount).HasPrecision(18, 2);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Conditions).HasMaxLength(500);
            entity.HasOne(e => e.Student)
                  .WithMany(s => s.Grants)
                  .HasForeignKey(e => e.StudentId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Scholarship>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.StudentId);
            entity.Property(e => e.Name).HasMaxLength(200).IsRequired();
            entity.Property(e => e.Type).HasMaxLength(100);
            entity.Property(e => e.Amount).HasPrecision(18, 2);
            entity.Property(e => e.Conditions).HasMaxLength(500);
            entity.HasOne(e => e.Student)
                  .WithMany(s => s.Scholarships)
                  .HasForeignKey(e => e.StudentId)
                  .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.ScholarshipTypeRef)
                  .WithMany(st => st.Scholarships)
                  .HasForeignKey(e => e.ScholarshipTypeId)
                  .OnDelete(DeleteBehavior.SetNull);
        });

        // ==================== Связующие таблицы ====================

        modelBuilder.Entity<StudentGrant>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.Student)
                  .WithMany(s => s.StudentGrants)
                  .HasForeignKey(e => e.StudentId)
                  .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.Grant)
                  .WithMany(g => g.StudentGrants)
                  .HasForeignKey(e => e.GrantId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<StudentScholarship>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.Reason).HasMaxLength(500);
            entity.HasOne(e => e.Student)
                  .WithMany(s => s.StudentScholarships)
                  .HasForeignKey(e => e.StudentId)
                  .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.Scholarship)
                  .WithMany(s => s.StudentScholarships)
                  .HasForeignKey(e => e.ScholarshipId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // ==================== История ====================

        modelBuilder.Entity<StatusStudentHistory>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Note).HasMaxLength(500);
            entity.HasOne(e => e.Student)
                  .WithMany(s => s.StatusHistories)
                  .HasForeignKey(e => e.StudentId)
                  .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.StatusType)
                  .WithMany(st => st.StatusHistories)
                  .HasForeignKey(e => e.StatusTypeId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<UserActionHistory>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Action).HasMaxLength(500).IsRequired();
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.HasOne(e => e.User)
                  .WithMany()
                  .HasForeignKey(e => e.UserId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // ==================== ChangeHistoryRecord ====================

        modelBuilder.Entity<ChangeHistoryRecord>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.StudentIIN);
            entity.Property(e => e.StudentIIN).HasMaxLength(12).IsRequired();
            entity.Property(e => e.StudentName).HasMaxLength(300).IsRequired();
            entity.Property(e => e.FieldName).HasMaxLength(100).IsRequired();
            entity.Property(e => e.FieldLabel).HasMaxLength(200).IsRequired();
            entity.Property(e => e.OldValue).HasMaxLength(500);
            entity.Property(e => e.NewValue).HasMaxLength(500);
            entity.Property(e => e.Source).HasMaxLength(200);
        });

        // ==================== ScholarshipLossRecord ====================

        modelBuilder.Entity<ScholarshipLossRecord>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.IIN);
            entity.Property(e => e.FirstName).HasMaxLength(100).IsRequired();
            entity.Property(e => e.LastName).HasMaxLength(100).IsRequired();
            entity.Property(e => e.MiddleName).HasMaxLength(100);
            entity.Property(e => e.IIN).HasMaxLength(12).IsRequired();
            entity.Property(e => e.OrderNumber).HasMaxLength(100);
            entity.Property(e => e.Reason).HasMaxLength(500);
            entity.Property(e => e.ScholarshipName).HasMaxLength(200);
            entity.HasOne(e => e.Student)
                  .WithMany()
                  .HasForeignKey(e => e.StudentId)
                  .OnDelete(DeleteBehavior.SetNull);
        });

        // ==================== User ====================

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Username).IsUnique();
            entity.HasIndex(e => e.Email);
            entity.Property(e => e.Username).HasMaxLength(100).IsRequired();
            entity.Property(e => e.PasswordHash).HasMaxLength(500).IsRequired();
            entity.Property(e => e.Email).HasMaxLength(200).IsRequired();
            entity.Property(e => e.Role).HasMaxLength(50).IsRequired();
        });

        // ==================== RBAC ====================

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Name).IsUnique();
            entity.Property(e => e.Name).HasMaxLength(100).IsRequired();
            entity.Property(e => e.Description).HasMaxLength(500);
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Name).IsUnique();
            entity.Property(e => e.Name).HasMaxLength(100).IsRequired();
            entity.Property(e => e.Description).HasMaxLength(500);
        });

        modelBuilder.Entity<RolePermission>(entity =>
        {
            entity.HasKey(rp => new { rp.RoleId, rp.PermissionId });
            entity.HasOne(rp => rp.Role)
                .WithMany(r => r.RolePermissions)
                .HasForeignKey(rp => rp.RoleId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(rp => rp.Permission)
                .WithMany(p => p.RolePermissions)
                .HasForeignKey(rp => rp.PermissionId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<UserRoleAssignment>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.ScopeType).HasMaxLength(50).IsRequired();
            entity.HasOne(e => e.User)
                .WithMany(u => u.UserAssignments)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.Role)
                .WithMany(r => r.UserAssignments)
                .HasForeignKey(e => e.RoleId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Scope>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.ScopeName).HasMaxLength(100).IsRequired();
            entity.Property(e => e.Description).HasMaxLength(500);
        });

        modelBuilder.Entity<AccessRole>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.AccessRoleName).HasMaxLength(100).IsRequired();
            entity.Property(e => e.Description).HasMaxLength(500);
        });

        // ==================== Seed Data ====================
        SeedData(modelBuilder);
    }

    private static void SeedData(ModelBuilder modelBuilder)
    {
        // Институты
        modelBuilder.Entity<Institute>().HasData(
            new Institute { Id = 1, InstituteName = "ИАиС" },
            new Institute { Id = 2, InstituteName = "Институт автоматики и информационных технологий" },
            new Institute { Id = 3, InstituteName = "Горно-металлургический институт имени О.Байконурова" },
            new Institute { Id = 4, InstituteName = "ИЭиМ" },
            new Institute { Id = 5, InstituteName = "ИГНД" },
            new Institute { Id = 6, InstituteName = "ШТИиЛ" },
            new Institute { Id = 7, InstituteName = "БШ" }
        );

        // Кафедры
        modelBuilder.Entity<Department>().HasData(
            // Институт автоматики и информационных технологий (Id=2)
            new Department { Id = 1, DepartmentName = "ПИ", InstituteId = 2 },
            new Department { Id = 2, DepartmentName = "АиУ", InstituteId = 2 },
            new Department { Id = 3, DepartmentName = "ЭТиКТ", InstituteId = 2 },
            new Department { Id = 4, DepartmentName = "КОиХИ", InstituteId = 2 },
            new Department { Id = 5, DepartmentName = "РТиТСА", InstituteId = 2 },
            new Department { Id = 6, DepartmentName = "ВМиМ", InstituteId = 2 },
            new Department { Id = 7, DepartmentName = "КБ", InstituteId = 2 },
            new Department { Id = 8, DepartmentName = "ИС", InstituteId = 2 },
            // ИАиС (Id=1)
            new Department { Id = 9, DepartmentName = "ИСиС", InstituteId = 1 },
            new Department { Id = 10, DepartmentName = "СиСМ", InstituteId = 1 },
            new Department { Id = 11, DepartmentName = "Академия дизайна", InstituteId = 1 },
            new Department { Id = 12, DepartmentName = "Студенческое конструкторское бюро", InstituteId = 1 },
            // Горно-металлургический институт (Id=3)
            new Department { Id = 13, DepartmentName = "МДиГ", InstituteId = 3 },
            new Department { Id = 14, DepartmentName = "ГД", InstituteId = 3 },
            new Department { Id = 15, DepartmentName = "МНиИФ", InstituteId = 3 },
            new Department { Id = 16, DepartmentName = "МиОПИ", InstituteId = 3 },
            new Department { Id = 17, DepartmentName = "ХПиПЭ", InstituteId = 3 },
            new Department { Id = 18, DepartmentName = "ИЦГ", InstituteId = 3 },
            // ИЭиМ (Id=4)
            new Department { Id = 19, DepartmentName = "Энергетика", InstituteId = 4 },
            new Department { Id = 20, DepartmentName = "ИМ", InstituteId = 4 },
            new Department { Id = 21, DepartmentName = "ССиМ", InstituteId = 4 },
            new Department { Id = 22, DepartmentName = "МС", InstituteId = 4 },
            new Department { Id = 23, DepartmentName = "ОФ", InstituteId = 4 },
            new Department { Id = 24, DepartmentName = "ТМиО", InstituteId = 4 },
            new Department { Id = 25, DepartmentName = "ТМиГТУ", InstituteId = 4 },
            new Department { Id = 26, DepartmentName = "ФкМ", InstituteId = 4 },
            new Department { Id = 27, DepartmentName = "РиМВМ", InstituteId = 4 },
            // ИГНД (Id=5)
            new Department { Id = 28, DepartmentName = "ГСПиРМПИ", InstituteId = 5 },
            new Department { Id = 29, DepartmentName = "НИ", InstituteId = 5 },
            new Department { Id = 30, DepartmentName = "ГС", InstituteId = 5 },
            new Department { Id = 31, DepartmentName = "ГИиНГ", InstituteId = 5 },
            new Department { Id = 32, DepartmentName = "ХиБИ", InstituteId = 5 },
            // ШТИиЛ (Id=6)
            new Department { Id = 33, DepartmentName = "Логистика", InstituteId = 6 },
            new Department { Id = 34, DepartmentName = "Транспортная инженерия", InstituteId = 6 },
            new Department { Id = 35, DepartmentName = "ШТИиЛ", InstituteId = 6 },
            // БШ (Id=7)
            new Department { Id = 36, DepartmentName = "Бизнес-школа", InstituteId = 7 }
        );

        // Специальности (по одной на каждую кафедру)
        modelBuilder.Entity<Speciality>().HasData(
            // ИАИТ — кафедра ПИ, АиУ, ЭТиКТ, КОиХИ, РТиТСА, ВМиМ, КБ, ИС
            new Speciality { Id = 1, SpecialityName = "Программная инженерия", DepartmentId = 1 },
            new Speciality { Id = 2, SpecialityName = "Автоматизация и управление", DepartmentId = 2 },
            new Speciality { Id = 3, SpecialityName = "Электронная техника и коммуникационные технологии", DepartmentId = 3 },
            new Speciality { Id = 4, SpecialityName = "Компьютерная оптика и химическое инженерство", DepartmentId = 4 },
            new Speciality { Id = 5, SpecialityName = "Радиотехника и телекоммуникации", DepartmentId = 5 },
            new Speciality { Id = 6, SpecialityName = "Вычислительная математика и моделирование", DepartmentId = 6 },
            new Speciality { Id = 7, SpecialityName = "Кибербезопасность", DepartmentId = 7 },
            new Speciality { Id = 8, SpecialityName = "Информационные системы", DepartmentId = 8 },
            // ИАиС
            new Speciality { Id = 9, SpecialityName = "Инженерные системы и сети", DepartmentId = 9 },
            new Speciality { Id = 10, SpecialityName = "Строительство и строительные материалы", DepartmentId = 10 },
            new Speciality { Id = 11, SpecialityName = "Дизайн", DepartmentId = 11 },
            new Speciality { Id = 12, SpecialityName = "Конструирование", DepartmentId = 12 },
            // ГМИ
            new Speciality { Id = 13, SpecialityName = "Металлургия и горное дело", DepartmentId = 13 },
            new Speciality { Id = 14, SpecialityName = "Горное дело", DepartmentId = 14 },
            new Speciality { Id = 15, SpecialityName = "Металлургия наноматериалов", DepartmentId = 15 },
            new Speciality { Id = 16, SpecialityName = "Металлургия и обогащение полезных ископаемых", DepartmentId = 16 },
            new Speciality { Id = 17, SpecialityName = "Химическое производство и промышленная экология", DepartmentId = 17 },
            new Speciality { Id = 18, SpecialityName = "Инженерная и компьютерная графика", DepartmentId = 18 },
            // ИЭиМ
            new Speciality { Id = 19, SpecialityName = "Энергетика", DepartmentId = 19 },
            new Speciality { Id = 20, SpecialityName = "Инженерная механика", DepartmentId = 20 },
            new Speciality { Id = 21, SpecialityName = "Стандартизация, сертификация и метрология", DepartmentId = 21 },
            new Speciality { Id = 22, SpecialityName = "Машиностроение", DepartmentId = 22 },
            new Speciality { Id = 23, SpecialityName = "Обогащение и флотация", DepartmentId = 23 },
            new Speciality { Id = 24, SpecialityName = "Технология машиностроения и оборудования", DepartmentId = 24 },
            new Speciality { Id = 25, SpecialityName = "Теплотехника и газотурбинные установки", DepartmentId = 25 },
            new Speciality { Id = 26, SpecialityName = "Физика конденсированного состояния и материаловедение", DepartmentId = 26 },
            new Speciality { Id = 27, SpecialityName = "Робототехника и мехатроника", DepartmentId = 27 },
            // ИГНД
            new Speciality { Id = 28, SpecialityName = "Геология и разведка месторождений", DepartmentId = 28 },
            new Speciality { Id = 29, SpecialityName = "Нефтяное инженерство", DepartmentId = 29 },
            new Speciality { Id = 30, SpecialityName = "Геодезия и съёмка", DepartmentId = 30 },
            new Speciality { Id = 31, SpecialityName = "Геоинформатика и нефтегазовое дело", DepartmentId = 31 },
            new Speciality { Id = 32, SpecialityName = "Химия и биоинженерия", DepartmentId = 32 },
            // ШТИиЛ
            new Speciality { Id = 33, SpecialityName = "Логистика", DepartmentId = 33 },
            new Speciality { Id = 34, SpecialityName = "Транспортная инженерия", DepartmentId = 34 },
            new Speciality { Id = 35, SpecialityName = "Транспортные технологии и логистика", DepartmentId = 35 },
            // БШ
            new Speciality { Id = 36, SpecialityName = "Бизнес и предпринимательство", DepartmentId = 36 }
        );

        // Формы обучения
        modelBuilder.Entity<StudyForm>().HasData(
            new StudyForm { Id = 1, StudyFormName = "Очная" },
            new StudyForm { Id = 2, StudyFormName = "Онлайн" }
        );

        // Уровни образования
        modelBuilder.Entity<DegreeLevel>().HasData(
            new DegreeLevel { Id = 1, DegreeName = "Бакалавриат" },
            new DegreeLevel { Id = 2, DegreeName = "Магистратура" },
            new DegreeLevel { Id = 3, DegreeName = "Докторантура" }
        );

        // Банки
        modelBuilder.Entity<Bank>().HasData(
            new Bank { Id = 1, RecipientBank = "Kaspi Bank", Bic = "CASPKZKA" },
            new Bank { Id = 2, RecipientBank = "Halyk Bank", Bic = "HSBKKZKX" },
            new Bank { Id = 3, RecipientBank = "Jusan Bank", Bic = "TSBNKZKA" },
            new Bank { Id = 4, RecipientBank = "Forte Bank", Bic = "IRTYKZKA" },
            new Bank { Id = 5, RecipientBank = "Bank CenterCredit", Bic = "KCJBKZKX" },
            new Bank { Id = 6, RecipientBank = "Bereke Bank", Bic = "SABRKZKA" }
        );

        // Типы стипендий
        modelBuilder.Entity<ScholarshipType>().HasData(
            new ScholarshipType { Id = 1, ScholarshipName = "Государственная стипендия" }
        );

        // Типы статусов
        modelBuilder.Entity<StatusType>().HasData(
            new StatusType { Id = 1, StatusName = "Активный" },
            new StatusType { Id = 2, StatusName = "Академический отпуск" },
            new StatusType { Id = 3, StatusName = "Отчислен" },
            new StatusType { Id = 4, StatusName = "Выпускник" },
            new StatusType { Id = 5, StatusName = "Переведён" }
        );

        // Роли RBAC
        modelBuilder.Entity<Role>().HasData(
            new Role { Id = 1, Name = "manager_or", Description = "Менеджер ОР — полный доступ, синхронизация" },
            new Role { Id = 2, Name = "department_head", Description = "Заведующий кафедры — просмотр своей кафедры" },
            new Role { Id = 3, Name = "institute_director", Description = "Директор института — просмотр института" }
        );
    }
}
