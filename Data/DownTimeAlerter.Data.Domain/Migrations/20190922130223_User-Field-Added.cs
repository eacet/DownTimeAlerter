using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DownTimeAlerter.Data.Domain.Migrations
{
    public partial class UserFieldAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Monitors",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Monitors_UserId",
                table: "Monitors",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Monitors_AspNetUsers_UserId",
                table: "Monitors",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Monitors_AspNetUsers_UserId",
                table: "Monitors");

            migrationBuilder.DropIndex(
                name: "IX_Monitors_UserId",
                table: "Monitors");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Monitors");
        }
    }
}
