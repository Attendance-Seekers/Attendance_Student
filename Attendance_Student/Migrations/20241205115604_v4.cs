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
            migrationBuilder.DropTable(
                name: "TeacherAttendance");

            migrationBuilder.AddColumn<string>(
                name: "teacher_id",
                table: "Attendances",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_teacher_id",
                table: "Attendances",
                column: "teacher_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_AspNetUsers_teacher_id",
                table: "Attendances",
                column: "teacher_id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_AspNetUsers_teacher_id",
                table: "Attendances");

            migrationBuilder.DropIndex(
                name: "IX_Attendances_teacher_id",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "teacher_id",
                table: "Attendances");

            migrationBuilder.CreateTable(
                name: "TeacherAttendance",
                columns: table => new
                {
                    TeacherId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AttendanceId = table.Column<int>(type: "int", nullable: false),
                    RecordDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherAttendance", x => new { x.TeacherId, x.AttendanceId });
                    table.ForeignKey(
                        name: "FK_TeacherAttendance_AspNetUsers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TeacherAttendance_Attendances_AttendanceId",
                        column: x => x.AttendanceId,
                        principalTable: "Attendances",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeacherAttendance_AttendanceId",
                table: "TeacherAttendance",
                column: "AttendanceId");
        }
    }
}
