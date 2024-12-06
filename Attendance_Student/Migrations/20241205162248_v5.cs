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
                name: "FK_StudentAttendance_AspNetUsers_StudentId",
                table: "StudentAttendance");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentAttendance_Attendances_AttendanceId",
                table: "StudentAttendance");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentAttendance",
                table: "StudentAttendance");

            migrationBuilder.RenameTable(
                name: "StudentAttendance",
                newName: "StudentAttendances");

            migrationBuilder.RenameIndex(
                name: "IX_StudentAttendance_AttendanceId",
                table: "StudentAttendances",
                newName: "IX_StudentAttendances_AttendanceId");

            migrationBuilder.AlterColumn<string>(
                name: "teacher_id",
                table: "Attendances",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "TimeTableId",
                table: "Attendances",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentAttendances",
                table: "StudentAttendances",
                columns: new[] { "StudentId", "AttendanceId" });

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAttendances_AspNetUsers_StudentId",
                table: "StudentAttendances",
                column: "StudentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAttendances_Attendances_AttendanceId",
                table: "StudentAttendances",
                column: "AttendanceId",
                principalTable: "Attendances",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentAttendances_AspNetUsers_StudentId",
                table: "StudentAttendances");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentAttendances_Attendances_AttendanceId",
                table: "StudentAttendances");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentAttendances",
                table: "StudentAttendances");

            migrationBuilder.RenameTable(
                name: "StudentAttendances",
                newName: "StudentAttendance");

            migrationBuilder.RenameIndex(
                name: "IX_StudentAttendances_AttendanceId",
                table: "StudentAttendance",
                newName: "IX_StudentAttendance_AttendanceId");

            migrationBuilder.AlterColumn<string>(
                name: "teacher_id",
                table: "Attendances",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TimeTableId",
                table: "Attendances",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentAttendance",
                table: "StudentAttendance",
                columns: new[] { "StudentId", "AttendanceId" });

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAttendance_AspNetUsers_StudentId",
                table: "StudentAttendance",
                column: "StudentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAttendance_Attendances_AttendanceId",
                table: "StudentAttendance",
                column: "AttendanceId",
                principalTable: "Attendances",
                principalColumn: "Id");
        }
    }
}
