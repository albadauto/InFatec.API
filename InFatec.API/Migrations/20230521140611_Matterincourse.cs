using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InFatec.API.Migrations
{
    /// <inheritdoc />
    public partial class Matterincourse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Teacher",
                table: "Courses",
                newName: "Matter");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Matter",
                table: "Courses",
                newName: "Teacher");
        }
    }
}
