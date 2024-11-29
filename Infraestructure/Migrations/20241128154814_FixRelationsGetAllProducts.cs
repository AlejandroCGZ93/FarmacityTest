using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class FixRelationsGetAllProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Barcodes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateAdd", "DateUpdate" },
                values: new object[] { new DateTime(2024, 11, 28, 12, 48, 13, 529, DateTimeKind.Local).AddTicks(8129), new DateTime(2024, 11, 28, 12, 48, 13, 531, DateTimeKind.Local).AddTicks(989) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateAdd", "DateUpdate" },
                values: new object[] { new DateTime(2024, 11, 28, 12, 48, 13, 531, DateTimeKind.Local).AddTicks(6170), new DateTime(2024, 11, 28, 12, 48, 13, 531, DateTimeKind.Local).AddTicks(6307) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Barcodes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateAdd", "DateUpdate" },
                values: new object[] { new DateTime(2024, 11, 27, 21, 8, 10, 547, DateTimeKind.Local).AddTicks(4806), new DateTime(2024, 11, 27, 21, 8, 10, 548, DateTimeKind.Local).AddTicks(8815) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateAdd", "DateUpdate" },
                values: new object[] { new DateTime(2024, 11, 27, 21, 8, 10, 549, DateTimeKind.Local).AddTicks(4247), new DateTime(2024, 11, 27, 21, 8, 10, 549, DateTimeKind.Local).AddTicks(4400) });
        }
    }
}
