using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Drkb.UniversalBot.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changed_db : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "MessageStructures");

            migrationBuilder.DropColumn(
                name: "OriginalFileName",
                table: "MessageStructures");

            migrationBuilder.DropColumn(
                name: "Seq",
                table: "MessageStructures");

            migrationBuilder.DropColumn(
                name: "StoredFilePath",
                table: "MessageStructures");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "MessageStructures",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OriginalFileName",
                table: "MessageStructures",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Seq",
                table: "MessageStructures",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "StoredFilePath",
                table: "MessageStructures",
                type: "text",
                nullable: true);
        }
    }
}
