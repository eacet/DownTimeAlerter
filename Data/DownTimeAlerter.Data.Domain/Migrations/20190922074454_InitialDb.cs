using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DownTimeAlerter.Data.Domain.Migrations
{
    public partial class InitialDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Monitors",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    Interval = table.Column<int>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    LastCheckDate = table.Column<DateTime>(nullable: false),
                    LastStatus = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Monitors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MonitorRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    MonitorId = table.Column<Guid>(nullable: false),
                    ResponseCode = table.Column<short>(nullable: false),
                    ResponseMessage = table.Column<string>(nullable: true),
                    IsSuccess = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonitorRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MonitorRequests_Monitors_MonitorId",
                        column: x => x.MonitorId,
                        principalTable: "Monitors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MonitorRequests_MonitorId",
                table: "MonitorRequests",
                column: "MonitorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MonitorRequests");

            migrationBuilder.DropTable(
                name: "Monitors");
        }
    }
}
