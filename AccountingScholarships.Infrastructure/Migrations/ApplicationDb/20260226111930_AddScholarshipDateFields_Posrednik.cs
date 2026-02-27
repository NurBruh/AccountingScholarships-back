using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountingScholarships.Infrastructure.Migrations.ApplicationDb
{
    /// <inheritdoc />
    public partial class AddScholarshipDateFields_Posrednik : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ScholarshipLostDate",
                table: "EpvoPosredniki",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ScholarshipNotes",
                table: "EpvoPosredniki",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "ScholarshipOrderCandidateDate",
                table: "EpvoPosredniki",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ScholarshipOrderLostDate",
                table: "EpvoPosredniki",
                type: "datetime(6)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ScholarshipLostDate",
                table: "EpvoPosredniki");

            migrationBuilder.DropColumn(
                name: "ScholarshipNotes",
                table: "EpvoPosredniki");

            migrationBuilder.DropColumn(
                name: "ScholarshipOrderCandidateDate",
                table: "EpvoPosredniki");

            migrationBuilder.DropColumn(
                name: "ScholarshipOrderLostDate",
                table: "EpvoPosredniki");
        }
    }
}
