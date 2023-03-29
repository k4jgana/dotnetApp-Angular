using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DutchTreat.Migrations
{
    /// <inheritdoc />
    public partial class Safeguard2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2023, 3, 9, 13, 29, 6, 834, DateTimeKind.Local).AddTicks(7420));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2023, 3, 9, 13, 27, 13, 412, DateTimeKind.Local).AddTicks(5009));
        }
    }
}
