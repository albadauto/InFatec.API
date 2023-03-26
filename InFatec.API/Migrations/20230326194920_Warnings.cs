using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InFatec.API.Migrations
{
    /// <inheritdoc />
    public partial class Warnings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Code",
                table: "Code",
                newName: "CodeString");

            migrationBuilder.CreateTable(
                name: "Warnings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ImgUri = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warnings", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Warnings");

            migrationBuilder.RenameColumn(
                name: "CodeString",
                table: "Code",
                newName: "Code");
        }
    }
}
