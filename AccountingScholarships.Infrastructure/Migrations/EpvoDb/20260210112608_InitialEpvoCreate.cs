using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AccountingScholarships.Infrastructure.Migrations.EpvoDb
{
    /// <inheritdoc />
    public partial class InitialEpvoCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EpvoStudents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    FirstName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MiddleName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IIN = table.Column<string>(type: "varchar(12)", maxLength: 12, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateOfBirth = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Faculty = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Speciality = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Course = table.Column<int>(type: "int", nullable: false),
                    GrantName = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GrantAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    ScholarshipName = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ScholarshipAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    SyncDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EpvoStudents", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "EpvoStudents",
                columns: new[] { "Id", "Course", "DateOfBirth", "Faculty", "FirstName", "GrantAmount", "GrantName", "IIN", "IsActive", "LastName", "MiddleName", "ScholarshipAmount", "ScholarshipName", "Speciality", "SyncDate" },
                values: new object[,]
                {
                    { new Guid("a1b2c3d4-e5f6-7890-abcd-ef1234567890"), 4, new DateTime(2003, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Информационные технологии", "Алихан", 500000m, "Государственный грант", "030515500123", true, "Сериков", "Бауржанович", 36660m, "Академическая стипендия", "Программная инженерия", new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("b2c3d4e5-f6a7-8901-bcde-f12345678901"), 3, new DateTime(2004, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Экономика", "Айгерим", 450000m, "Государственный грант", "040820600456", true, "Нурланова", "Ерлановна", 50000m, "Повышенная стипендия", "Финансы", new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("c3d4e5f6-a7b8-9012-cdef-123456789012"), 2, new DateTime(2002, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Юриспруденция", "Дамир", 400000m, "Государственный грант", "020110500789", true, "Касымов", null, null, null, "Правоведение", new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("d4e5f6a7-b8c9-0123-defa-234567890123"), 1, new DateTime(2005, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Информационные технологии", "Мадина", null, null, "050305700234", true, "Абдрахманова", "Канатовна", null, null, "Информационная безопасность", new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("e5f6a7b8-c9d0-1234-efab-345678901234"), 4, new DateTime(2001, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Медицина", "Нурсултан", 600000m, "Государственный грант", "010725500567", false, "Токаев", "Маратович", 36660m, "Академическая стипендия", "Общая медицина", new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EpvoStudents_IIN",
                table: "EpvoStudents",
                column: "IIN",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EpvoStudents");
        }
    }
}
