using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcRatings.Migrations
{
    /// <inheritdoc />
    public partial class CustomUserData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rating_AspNetUsers_UserId1",
                table: "Rating");

            migrationBuilder.DropIndex(
                name: "IX_Rating_UserId1",
                table: "Rating");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Rating");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Rating",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.CreateIndex(
                name: "IX_Rating_UserId",
                table: "Rating",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_AspNetUsers_UserId",
                table: "Rating",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rating_AspNetUsers_UserId",
                table: "Rating");

            migrationBuilder.DropIndex(
                name: "IX_Rating_UserId",
                table: "Rating");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Rating",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "Rating",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rating_UserId1",
                table: "Rating",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_AspNetUsers_UserId1",
                table: "Rating",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
