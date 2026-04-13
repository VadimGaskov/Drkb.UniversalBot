using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Drkb.UniversalBot.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changed_category : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_Category_ParentCategoryId",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_MessageStructure_Category_CategoryId",
                table: "MessageStructure");

            migrationBuilder.DropForeignKey(
                name: "FK_Recommendation_SenderUser_SenderUserId",
                table: "Recommendation");

            migrationBuilder.DropForeignKey(
                name: "FK_Statistics_Category_CategoryId",
                table: "Statistics");

            migrationBuilder.DropForeignKey(
                name: "FK_Statistics_SenderUser_SenderUserId",
                table: "Statistics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SenderUser",
                table: "SenderUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Recommendation",
                table: "Recommendation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MessageStructure",
                table: "MessageStructure");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            migrationBuilder.RenameTable(
                name: "SenderUser",
                newName: "SenderUsers");

            migrationBuilder.RenameTable(
                name: "Recommendation",
                newName: "Recommendations");

            migrationBuilder.RenameTable(
                name: "MessageStructure",
                newName: "MessageStructures");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "Categories");

            migrationBuilder.RenameIndex(
                name: "IX_Recommendation_SenderUserId",
                table: "Recommendations",
                newName: "IX_Recommendations_SenderUserId");

            migrationBuilder.RenameIndex(
                name: "IX_MessageStructure_Title_CategoryId",
                table: "MessageStructures",
                newName: "IX_MessageStructures_Title_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_MessageStructure_CategoryId",
                table: "MessageStructures",
                newName: "IX_MessageStructures_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Category_Title",
                table: "Categories",
                newName: "IX_Categories_Title");

            migrationBuilder.RenameIndex(
                name: "IX_Category_ParentCategoryId",
                table: "Categories",
                newName: "IX_Categories_ParentCategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SenderUsers",
                table: "SenderUsers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Recommendations",
                table: "Recommendations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MessageStructures",
                table: "MessageStructures",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_ParentCategoryId",
                table: "Categories",
                column: "ParentCategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MessageStructures_Categories_CategoryId",
                table: "MessageStructures",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Recommendations_SenderUsers_SenderUserId",
                table: "Recommendations",
                column: "SenderUserId",
                principalTable: "SenderUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Statistics_Categories_CategoryId",
                table: "Statistics",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Statistics_SenderUsers_SenderUserId",
                table: "Statistics",
                column: "SenderUserId",
                principalTable: "SenderUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_ParentCategoryId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_MessageStructures_Categories_CategoryId",
                table: "MessageStructures");

            migrationBuilder.DropForeignKey(
                name: "FK_Recommendations_SenderUsers_SenderUserId",
                table: "Recommendations");

            migrationBuilder.DropForeignKey(
                name: "FK_Statistics_Categories_CategoryId",
                table: "Statistics");

            migrationBuilder.DropForeignKey(
                name: "FK_Statistics_SenderUsers_SenderUserId",
                table: "Statistics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SenderUsers",
                table: "SenderUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Recommendations",
                table: "Recommendations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MessageStructures",
                table: "MessageStructures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.RenameTable(
                name: "SenderUsers",
                newName: "SenderUser");

            migrationBuilder.RenameTable(
                name: "Recommendations",
                newName: "Recommendation");

            migrationBuilder.RenameTable(
                name: "MessageStructures",
                newName: "MessageStructure");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Category");

            migrationBuilder.RenameIndex(
                name: "IX_Recommendations_SenderUserId",
                table: "Recommendation",
                newName: "IX_Recommendation_SenderUserId");

            migrationBuilder.RenameIndex(
                name: "IX_MessageStructures_Title_CategoryId",
                table: "MessageStructure",
                newName: "IX_MessageStructure_Title_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_MessageStructures_CategoryId",
                table: "MessageStructure",
                newName: "IX_MessageStructure_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_Title",
                table: "Category",
                newName: "IX_Category_Title");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_ParentCategoryId",
                table: "Category",
                newName: "IX_Category_ParentCategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SenderUser",
                table: "SenderUser",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Recommendation",
                table: "Recommendation",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MessageStructure",
                table: "MessageStructure",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Category_ParentCategoryId",
                table: "Category",
                column: "ParentCategoryId",
                principalTable: "Category",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MessageStructure_Category_CategoryId",
                table: "MessageStructure",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Recommendation_SenderUser_SenderUserId",
                table: "Recommendation",
                column: "SenderUserId",
                principalTable: "SenderUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Statistics_Category_CategoryId",
                table: "Statistics",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Statistics_SenderUser_SenderUserId",
                table: "Statistics",
                column: "SenderUserId",
                principalTable: "SenderUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
