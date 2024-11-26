using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Attendance_Student.Migrations
{
    /// <inheritdoc />
    public partial class v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Class_Id",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    Class_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Class_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Class_Size = table.Column<int>(type: "int", nullable: false),
                    TeacherId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.Class_Id);
                });

            migrationBuilder.CreateTable(
                name: "TimeTables",
                columns: table => new
                {
                    timeTable_Id = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeTables", x => x.timeTable_Id);
                    table.ForeignKey(
                        name: "FK_TimeTables_Classes_timeTable_Id",
                        column: x => x.timeTable_Id,
                        principalTable: "Classes",
                        principalColumn: "Class_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    subject_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    subject_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    subject_Duration = table.Column<int>(type: "int", nullable: false),
                    timeTable_Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.subject_Id);
                    table.ForeignKey(
                        name: "FK_Subjects_TimeTables_timeTable_Id",
                        column: x => x.timeTable_Id,
                        principalTable: "TimeTables",
                        principalColumn: "timeTable_Id");
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    DeptId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teachers_Departments_DeptId",
                        column: x => x.DeptId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Teachers_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "subject_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Classes",
                columns: new[] { "Class_Id", "Class_Name", "Class_Size", "TeacherId" },
                values: new object[,]
                {
                    { 1, "Class A", 30, null },
                    { 2, "Class B", 25, null },
                    { 3, "Class C", 20, null }
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Computer Science" },
                    { 2, "Mechanical Engineering" },
                    { 3, "Electrical Engineering" }
                });

            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "subject_Id", "subject_Duration", "subject_Name", "timeTable_Id" },
                values: new object[,]
                {
                    { 1, 60, "Mathematics", null },
                    { 2, 60, "Physics", null },
                    { 3, 60, "Chemistry", null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Class_Id", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Status", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "1", 0, 1, "fb3352a5-9750-4115-bd91-7cf8e54ba4b8", "Student", null, false, false, null, null, null, null, null, false, "9eda56b8-97c2-4182-a1ef-e7c8c2201472", "Active", false, "student1" },
                    { "2", 0, 2, "8b804d5e-e1ec-48de-bfc4-5de6fd6ff2e7", "Student", null, false, false, null, null, null, null, null, false, "5d9d23ea-2091-4b23-b390-3b0b96c0a4d0", "Active", false, "student2" },
                    { "3", 0, 3, "8ff914cb-bd83-4152-852f-e41ab34d6508", "Student", null, false, false, null, null, null, null, null, false, "a52e488d-180b-45f8-a507-34f828f9727c", "Inactive", false, "student3" }
                });

            migrationBuilder.InsertData(
                table: "Teachers",
                columns: new[] { "Id", "Address", "DeptId", "Name", "SubjectId" },
                values: new object[,]
                {
                    { 1, "123 Street, City", 1, "John Smith", 1 },
                    { 2, "456 Avenue, City", 2, "Alice Johnson", 2 },
                    { 3, "789 Boulevard, City", 3, "Robert Brown", 3 }
                });

            migrationBuilder.InsertData(
                table: "TimeTables",
                columns: new[] { "timeTable_Id", "CreatedDate", "LastUpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 11, 26, 21, 3, 25, 725, DateTimeKind.Utc).AddTicks(2589), new DateTime(2024, 11, 26, 21, 3, 25, 725, DateTimeKind.Utc).AddTicks(2591) },
                    { 2, new DateTime(2024, 11, 26, 21, 3, 25, 725, DateTimeKind.Utc).AddTicks(2593), new DateTime(2024, 11, 26, 21, 3, 25, 725, DateTimeKind.Utc).AddTicks(2593) },
                    { 3, new DateTime(2024, 11, 26, 21, 3, 25, 725, DateTimeKind.Utc).AddTicks(2594), new DateTime(2024, 11, 26, 21, 3, 25, 725, DateTimeKind.Utc).AddTicks(2595) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Class_Id",
                table: "AspNetUsers",
                column: "Class_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_TeacherId",
                table: "Classes",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_timeTable_Id",
                table: "Subjects",
                column: "timeTable_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_DeptId",
                table: "Teachers",
                column: "DeptId");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_SubjectId",
                table: "Teachers",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Classes_Class_Id",
                table: "AspNetUsers",
                column: "Class_Id",
                principalTable: "Classes",
                principalColumn: "Class_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Teachers_TeacherId",
                table: "Classes",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Classes_Class_Id",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Teachers_TeacherId",
                table: "Classes");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "TimeTables");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_Class_Id",
                table: "AspNetUsers");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3");

            migrationBuilder.DropColumn(
                name: "Class_Id",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "AspNetUsers");
        }
    }
}
