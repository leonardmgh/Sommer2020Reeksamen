using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dentist.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeNumber",
                table: "AspNetUsers",
                type: "int",
                maxLength: 5,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BillableItems",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CostumerPart = table.Column<float>(type: "real", nullable: false),
                    InsurancePart = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillableItems", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Costumer",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CPRNumber = table.Column<int>(type: "int", maxLength: 10, nullable: false),
                    StreetName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StreetNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Insured = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Costumer", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Bill",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BillDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CostumerID = table.Column<int>(type: "int", nullable: false),
                    BillMakerId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bill", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Bill_AspNetUsers_BillMakerId",
                        column: x => x.BillMakerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bill_Costumer_CostumerID",
                        column: x => x.CostumerID,
                        principalTable: "Costumer",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BillBillableItems",
                columns: table => new
                {
                    BillsID = table.Column<int>(type: "int", nullable: false),
                    ItemsID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillBillableItems", x => new { x.BillsID, x.ItemsID });
                    table.ForeignKey(
                        name: "FK_BillBillableItems_Bill_BillsID",
                        column: x => x.BillsID,
                        principalTable: "Bill",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BillBillableItems_BillableItems_ItemsID",
                        column: x => x.ItemsID,
                        principalTable: "BillableItems",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bill_BillMakerId",
                table: "Bill",
                column: "BillMakerId");

            migrationBuilder.CreateIndex(
                name: "IX_Bill_CostumerID",
                table: "Bill",
                column: "CostumerID");

            migrationBuilder.CreateIndex(
                name: "IX_BillBillableItems_ItemsID",
                table: "BillBillableItems",
                column: "ItemsID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillBillableItems");

            migrationBuilder.DropTable(
                name: "Bill");

            migrationBuilder.DropTable(
                name: "BillableItems");

            migrationBuilder.DropTable(
                name: "Costumer");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "EmployeeNumber",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");
        }
    }
}
