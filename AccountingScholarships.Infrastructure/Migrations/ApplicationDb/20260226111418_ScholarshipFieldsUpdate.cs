using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountingScholarships.Infrastructure.Migrations.ApplicationDb
{
    /// <inheritdoc />
    public partial class ScholarshipFieldsUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Переименовываем EndDate → LostDate (сохраняем данные)
            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "Scholarships",
                newName: "LostDate");

            // Новые колонки
            migrationBuilder.AddColumn<DateTime>(
                name: "OrderLostDate",
                table: "Scholarships",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Scholarships",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "OrderCandidateDate",
                table: "Scholarships",
                type: "datetime(6)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderLostDate",
                table: "Scholarships");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Scholarships");

            migrationBuilder.DropColumn(
                name: "OrderCandidateDate",
                table: "Scholarships");

            migrationBuilder.RenameColumn(
                name: "LostDate",
                table: "Scholarships",
                newName: "EndDate");
        }
    }
}
