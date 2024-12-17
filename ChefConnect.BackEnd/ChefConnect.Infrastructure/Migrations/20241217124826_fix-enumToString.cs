using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChefConnect.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixenumToString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Users",
                type: "nvarchar(50)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 17, 12, 48, 24, 599, DateTimeKind.Utc).AddTicks(3533), new DateTime(2024, 12, 17, 12, 48, 24, 599, DateTimeKind.Utc).AddTicks(3536) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 17, 12, 48, 24, 599, DateTimeKind.Utc).AddTicks(3538), new DateTime(2024, 12, 17, 12, 48, 24, 599, DateTimeKind.Utc).AddTicks(3538) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 17, 12, 48, 24, 599, DateTimeKind.Utc).AddTicks(3539), new DateTime(2024, 12, 17, 12, 48, 24, 599, DateTimeKind.Utc).AddTicks(3540) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Users",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 17, 7, 14, 57, 6, DateTimeKind.Utc).AddTicks(2918), new DateTime(2024, 12, 17, 7, 14, 57, 6, DateTimeKind.Utc).AddTicks(2921) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 17, 7, 14, 57, 6, DateTimeKind.Utc).AddTicks(2923), new DateTime(2024, 12, 17, 7, 14, 57, 6, DateTimeKind.Utc).AddTicks(2923) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 17, 7, 14, 57, 6, DateTimeKind.Utc).AddTicks(2924), new DateTime(2024, 12, 17, 7, 14, 57, 6, DateTimeKind.Utc).AddTicks(2924) });
        }
    }
}
