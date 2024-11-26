using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Attendance_Student.Migrations
{
    /// <inheritdoc />
    public partial class v4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "fb3352a5-9750-4115-bd91-7cf8e54ba4b8", "9eda56b8-97c2-4182-a1ef-e7c8c2201472" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "8b804d5e-e1ec-48de-bfc4-5de6fd6ff2e7", "5d9d23ea-2091-4b23-b390-3b0b96c0a4d0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "8ff914cb-bd83-4152-852f-e41ab34d6508", "a52e488d-180b-45f8-a507-34f828f9727c" });

            migrationBuilder.UpdateData(
                table: "TimeTables",
                keyColumn: "timeTable_Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "LastUpdatedDate" },
                values: new object[] { new DateTime(2024, 11, 26, 21, 3, 25, 725, DateTimeKind.Utc).AddTicks(2589), new DateTime(2024, 11, 26, 21, 3, 25, 725, DateTimeKind.Utc).AddTicks(2591) });

            migrationBuilder.UpdateData(
                table: "TimeTables",
                keyColumn: "timeTable_Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "LastUpdatedDate" },
                values: new object[] { new DateTime(2024, 11, 26, 21, 3, 25, 725, DateTimeKind.Utc).AddTicks(2593), new DateTime(2024, 11, 26, 21, 3, 25, 725, DateTimeKind.Utc).AddTicks(2593) });

            migrationBuilder.UpdateData(
                table: "TimeTables",
                keyColumn: "timeTable_Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "LastUpdatedDate" },
                values: new object[] { new DateTime(2024, 11, 26, 21, 3, 25, 725, DateTimeKind.Utc).AddTicks(2594), new DateTime(2024, 11, 26, 21, 3, 25, 725, DateTimeKind.Utc).AddTicks(2595) });
        }
    }
}
