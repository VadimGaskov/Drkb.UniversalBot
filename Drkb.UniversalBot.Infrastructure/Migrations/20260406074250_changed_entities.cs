using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Drkb.UniversalBot.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changed_entities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "MessageStructures",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "MessageStructures",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

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

            migrationBuilder.AddColumn<string>(
                name: "StoredFilePath",
                table: "MessageStructures",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "MessageStructures");

            migrationBuilder.DropColumn(
                name: "OriginalFileName",
                table: "MessageStructures");

            migrationBuilder.DropColumn(
                name: "StoredFilePath",
                table: "MessageStructures");

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "MessageStructures",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "MessageStructures",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
