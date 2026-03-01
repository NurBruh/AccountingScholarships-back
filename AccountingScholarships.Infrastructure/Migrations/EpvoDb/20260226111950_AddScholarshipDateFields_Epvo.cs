using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountingScholarships.Infrastructure.Migrations.EpvoDb
{
    /// <inheritdoc />
    public partial class AddScholarshipDateFields_Epvo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ScholarshipLostDate",
                table: "EpvoStudents",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ScholarshipNotes",
                table: "EpvoStudents",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "ScholarshipOrderCandidateDate",
                table: "EpvoStudents",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ScholarshipOrderLostDate",
                table: "EpvoStudents",
                type: "datetime(6)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ScholarshipLostDate",
                table: "EpvoStudents");

            migrationBuilder.DropColumn(
                name: "ScholarshipNotes",
                table: "EpvoStudents");

            migrationBuilder.DropColumn(
                name: "ScholarshipOrderCandidateDate",
                table: "EpvoStudents");

            migrationBuilder.DropColumn(
                name: "ScholarshipOrderLostDate",
                table: "EpvoStudents");
        }
    }
}
