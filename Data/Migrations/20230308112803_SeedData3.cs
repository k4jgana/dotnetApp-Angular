using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DutchTreat.Migrations
{
    /// <inheritdoc />
    public partial class SeedData3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2023, 3, 8, 12, 28, 3, 312, DateTimeKind.Local).AddTicks(4459));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2023, 3, 8, 12, 16, 42, 967, DateTimeKind.Local).AddTicks(8259));
        }
    }
}
