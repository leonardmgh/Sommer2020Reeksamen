using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DentistBilling.Migrations
{
    public partial class InitialCreate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Bill",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "BillDate", "TotalCostumer" },
                values: new object[] { new DateTimeOffset(new DateTime(2021, 8, 9, 23, 49, 0, 383, DateTimeKind.Unspecified).AddTicks(4739), new TimeSpan(0, 2, 0, 0, 0)), 421.0 });

            migrationBuilder.UpdateData(
                table: "Bill",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "BillDate", "TotalCostumer" },
                values: new object[] { new DateTimeOffset(new DateTime(2021, 8, 9, 23, 49, 0, 385, DateTimeKind.Unspecified).AddTicks(1437), new TimeSpan(0, 2, 0, 0, 0)), 531.5 });

            migrationBuilder.UpdateData(
                table: "Bill",
                keyColumn: "ID",
                keyValue: 3,
                column: "BillDate",
                value: new DateTimeOffset(new DateTime(2021, 8, 9, 23, 49, 0, 385, DateTimeKind.Unspecified).AddTicks(1466), new TimeSpan(0, 2, 0, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Bill",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "BillDate", "TotalCostumer" },
                values: new object[] { new DateTimeOffset(new DateTime(2021, 8, 9, 23, 44, 43, 234, DateTimeKind.Unspecified).AddTicks(4767), new TimeSpan(0, 2, 0, 0, 0)), 310.5 });

            migrationBuilder.UpdateData(
                table: "Bill",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "BillDate", "TotalCostumer" },
                values: new object[] { new DateTimeOffset(new DateTime(2021, 8, 9, 23, 44, 43, 236, DateTimeKind.Unspecified).AddTicks(164), new TimeSpan(0, 2, 0, 0, 0)), 310.5 });

            migrationBuilder.UpdateData(
                table: "Bill",
                keyColumn: "ID",
                keyValue: 3,
                column: "BillDate",
                value: new DateTimeOffset(new DateTime(2021, 8, 9, 23, 44, 43, 236, DateTimeKind.Unspecified).AddTicks(192), new TimeSpan(0, 2, 0, 0, 0)));
        }
    }
}
