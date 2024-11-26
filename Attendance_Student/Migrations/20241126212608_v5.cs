using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Attendance_Student.Migrations
{
    /// <inheritdoc />
    public partial class v5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Teachers_TeacherId",
                table: "Classes");

            migrationBuilder.DropIndex(
                name: "IX_Classes_TeacherId",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "Classes");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "46f2ce7e-12ab-42c1-b46c-a451796f9b91", "2db44203-3ea7-4791-80a8-47d570b7428c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "91823605-f71a-4aff-99b2-f47289e45c97", "10abaf5e-70dd-4ddb-b13c-cdeacd807c5f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "bed8e469-59f0-40a5-b805-bf19f81f8d08", "0cfb4ae7-0445-4fb4-9222-0703d65a87a9" });

            migrationBuilder.UpdateData(
                table: "TimeTables",
                keyColumn: "timeTable_Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "LastUpdatedDate" },
                values: new object[] { new DateTime(2024, 11, 26, 21, 26, 7, 479, DateTimeKind.Utc).AddTicks(6233), new DateTime(2024, 11, 26, 21, 26, 7, 479, DateTimeKind.Utc).AddTicks(6237) });

            migrationBuilder.UpdateData(
                table: "TimeTables",
                keyColumn: "timeTable_Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "LastUpdatedDate" },
                values: new object[] { new DateTime(2024, 11, 26, 21, 26, 7, 479, DateTimeKind.Utc).AddTicks(6239), new DateTime(2024, 11, 26, 21, 26, 7, 479, DateTimeKind.Utc).AddTicks(6239) });

            migrationBuilder.UpdateData(
                table: "TimeTables",
                keyColumn: "timeTable_Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "LastUpdatedDate" },
                values: new object[] { new DateTime(2024, 11, 26, 21, 26, 7, 479, DateTimeKind.Utc).AddTicks(6241), new DateTime(2024, 11, 26, 21, 26, 7, 479, DateTimeKind.Utc).AddTicks(6241) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeacherId",
                table: "Classes",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "13e56d5b-6834-45ab-bed6-64cfc3e8cf69", "2b55349b-90c9-4405-9eff-31a3c3547f95" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "ce367e10-3639-456b-83f3-52108e57b730", "7c933f31-cdab-417a-bba9-7ffbd0624d5e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "0640210b-f4b0-4d27-82e5-e2965ada70a6", "b59700dc-985c-445c-b313-618b2cf9c9c4" });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "Class_Id",
                keyValue: 1,
                column: "TeacherId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "Class_Id",
                keyValue: 2,
                column: "TeacherId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "Class_Id",
                keyValue: 3,
                column: "TeacherId",
                value: null);

            migrationBuilder.UpdateData(
                table: "TimeTables",
                keyColumn: "timeTable_Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "LastUpdatedDate" },
                values: new object[] { new DateTime(2024, 11, 26, 21, 18, 57, 266, DateTimeKind.Utc).AddTicks(6034), new DateTime(2024, 11, 26, 21, 18, 57, 266, DateTimeKind.Utc).AddTicks(6037) });

            migrationBuilder.UpdateData(
                table: "TimeTables",
                keyColumn: "timeTable_Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "LastUpdatedDate" },
                values: new object[] { new DateTime(2024, 11, 26, 21, 18, 57, 266, DateTimeKind.Utc).AddTicks(6040), new DateTime(2024, 11, 26, 21, 18, 57, 266, DateTimeKind.Utc).AddTicks(6041) });

            migrationBuilder.UpdateData(
                table: "TimeTables",
                keyColumn: "timeTable_Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "LastUpdatedDate" },
                values: new object[] { new DateTime(2024, 11, 26, 21, 18, 57, 266, DateTimeKind.Utc).AddTicks(6043), new DateTime(2024, 11, 26, 21, 18, 57, 266, DateTimeKind.Utc).AddTicks(6044) });

            migrationBuilder.CreateIndex(
                name: "IX_Classes_TeacherId",
                table: "Classes",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Teachers_TeacherId",
                table: "Classes",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id");
        }
    }
}
