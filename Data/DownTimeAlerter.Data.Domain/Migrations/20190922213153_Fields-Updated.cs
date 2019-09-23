using Microsoft.EntityFrameworkCore.Migrations;

namespace DownTimeAlerter.Data.Domain.Migrations
{
    public partial class FieldsUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MonitorRequests_Monitors_MonitorId",
                table: "MonitorRequests");

            migrationBuilder.DropIndex(
                name: "IX_MonitorRequests_MonitorId",
                table: "MonitorRequests");

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "Monitors",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Monitors",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "Monitors",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Monitors",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_MonitorRequests_MonitorId",
                table: "MonitorRequests",
                column: "MonitorId");

            migrationBuilder.AddForeignKey(
                name: "FK_MonitorRequests_Monitors_MonitorId",
                table: "MonitorRequests",
                column: "MonitorId",
                principalTable: "Monitors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
