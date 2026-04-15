using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Drkb.UniversalBot.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changed_configuration4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Statistics_Categories_CategoryId",
                table: "Statistics");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "Statistics");

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoryId",
                table: "Statistics",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Statistics_Categories_CategoryId",
                table: "Statistics",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Statistics_Categories_CategoryId",
                table: "Statistics");

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoryId",
                table: "Statistics",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EventId",
                table: "Statistics",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Statistics_Categories_CategoryId",
                table: "Statistics",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
