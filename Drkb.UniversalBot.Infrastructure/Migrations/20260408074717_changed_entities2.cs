using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Drkb.UniversalBot.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changed_entities2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Statistics");

            migrationBuilder.AddColumn<int>(
                name: "EventId",
                table: "Statistics",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventId",
                table: "Statistics");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Statistics",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
