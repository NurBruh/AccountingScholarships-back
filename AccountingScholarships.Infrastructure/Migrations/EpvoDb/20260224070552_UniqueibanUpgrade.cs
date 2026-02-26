using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountingScholarships.Infrastructure.Migrations.EpvoDb
{
    /// <inheritdoc />
    public partial class UniqueibanUpgrade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_EpvoStudents_iban",
                table: "EpvoStudents",
                column: "iban",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EpvoStudents_iban",
                table: "EpvoStudents");
        }
    }
}
