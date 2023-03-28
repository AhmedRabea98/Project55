using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WePayOffer.DAL.Migrations
{
    public partial class initialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceNumber",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OfferId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceNumber", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceNumber_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceTransaction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceNumberId = table.Column<int>(type: "int", nullable: false),
                    IsSucceed = table.Column<bool>(type: "bit", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FunctionId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResponseMessage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceTransaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceTransaction_ServiceNumber_ServiceNumberId",
                        column: x => x.ServiceNumberId,
                        principalTable: "ServiceNumber",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Status",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "New" },
                    { 2, "Succeed" },
                    { 3, "Failed" },
                    { 4, "In Progress" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceNumber_StatusId",
                table: "ServiceNumber",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceTransaction_ServiceNumberId",
                table: "ServiceTransaction",
                column: "ServiceNumberId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceTransaction");

            migrationBuilder.DropTable(
                name: "ServiceNumber");

            migrationBuilder.DropTable(
                name: "Status");
        }
    }
}
