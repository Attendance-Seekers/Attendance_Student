using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Attendance_Student.Migrations
{
    /// <inheritdoc />
    public partial class custom_migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop Foreign Keys
            migrationBuilder.DropForeignKey(name: "FK_AspNetUsers_AspNetUsers_ParentId", table: "AspNetUsers");
            migrationBuilder.DropForeignKey(name: "FK_AspNetUsers_Classes_ClassId", table: "AspNetUsers");
            migrationBuilder.DropForeignKey(name: "FK_AspNetUsers_Departments_DeptId", table: "AspNetUsers");
            migrationBuilder.DropForeignKey(name: "FK_AspNetUsers_Subjects_SubjectId", table: "AspNetUsers");
            migrationBuilder.DropForeignKey(name: "FK_Attendances_TimeTables_TimeTableId", table: "Attendances");
            migrationBuilder.DropForeignKey(name: "FK_DaySchedules_TimeTables_TimeTable_id", table: "DaySchedules");
            migrationBuilder.DropForeignKey(name: "FK_SubjectDaySchedule_DaySchedules_DayScheduleId", table: "SubjectDaySchedule");
            migrationBuilder.DropForeignKey(name: "FK_SubjectDaySchedule_Subjects_SubjectId", table: "SubjectDaySchedule");
            migrationBuilder.DropForeignKey(name: "FK_Subjects_Departments_DeptId", table: "Subjects");
            migrationBuilder.DropForeignKey(name: "FK_TimeTables_Classes_class_id", table: "TimeTables");

            // Modify columns to allow nulls
            migrationBuilder.AlterColumn<int>(
                name: "class_id",
                table: "TimeTables",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DeptId",
                table: "Subjects",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "TimeTable_id",
                table: "DaySchedules",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "TimeTableId",
                table: "Attendances",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            // Add Foreign Keys with ON DELETE SET NULL
            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_ParentId",
                table: "AspNetUsers",
                column: "ParentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Classes_ClassId",
                table: "AspNetUsers",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "Class_Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Departments_DeptId",
                table: "AspNetUsers",
                column: "DeptId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Subjects_SubjectId",
                table: "AspNetUsers",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "subject_Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_TimeTables_TimeTableId",
                table: "Attendances",
                column: "TimeTableId",
                principalTable: "TimeTables",
                principalColumn: "TimeTableId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_DaySchedules_TimeTables_TimeTable_id",
                table: "DaySchedules",
                column: "TimeTable_id",
                principalTable: "TimeTables",
                principalColumn: "TimeTableId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAttendance_AspNetUsers_StudentId",
                table: "StudentAttendances",
                column: "StudentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAttendance_Attendances_AttendanceId",
                table: "StudentAttendances",
                column: "AttendanceId",
                principalTable: "Attendances",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectDaySchedule_DaySchedules_DayScheduleId",
                table: "SubjectDaySchedule",
                column: "DayScheduleId",
                principalTable: "DaySchedules",
                principalColumn: "DayScheduleId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectDaySchedule_Subjects_SubjectId",
                table: "SubjectDaySchedule",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "subject_Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Departments_DeptId",
                table: "Subjects",
                column: "DeptId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeTables_Classes_class_id",
                table: "TimeTables",
                column: "class_id",
                principalTable: "Classes",
                principalColumn: "Class_Id",
                onDelete: ReferentialAction.SetNull);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
          name: "FK_AspNetUsers_AspNetUsers_ParentId",
          table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Classes_ClassId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Departments_DeptId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Subjects_SubjectId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_TimeTables_TimeTableId",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_DaySchedules_TimeTables_TimeTable_id",
                table: "DaySchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentAttendance_AspNetUsers_StudentId",
                table: "StudentAttendances");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentAttendance_Attendances_AttendanceId",
                table: "StudentAttendances");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectDaySchedule_DaySchedules_DayScheduleId",
                table: "SubjectDaySchedule");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectDaySchedule_Subjects_SubjectId",
                table: "SubjectDaySchedule");

            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Departments_DeptId",
                table: "Subjects");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeTables_Classes_class_id",
                table: "TimeTables");

            // Revert column changes to make columns non-nullable again
            migrationBuilder.AlterColumn<int>(
                name: "class_id",
                table: "TimeTables",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DeptId",
                table: "Subjects",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TimeTable_id",
                table: "DaySchedules",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TimeTableId",
                table: "Attendances",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            // Re-add the Foreign Keys with their original DELETE actions
            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_ParentId",
                table: "AspNetUsers",
                column: "ParentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Classes_ClassId",
                table: "AspNetUsers",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "Class_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Departments_DeptId",
                table: "AspNetUsers",
                column: "DeptId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Subjects_SubjectId",
                table: "AspNetUsers",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "subject_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_TimeTables_TimeTableId",
                table: "Attendances",
                column: "TimeTableId",
                principalTable: "TimeTables",
                principalColumn: "TimeTableId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DaySchedules_TimeTables_TimeTable_id",
                table: "DaySchedules",
                column: "TimeTable_id",
                principalTable: "TimeTables",
                principalColumn: "TimeTableId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAttendance_AspNetUsers_StudentId",
                table: "StudentAttendances",
                column: "StudentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAttendance_Attendances_AttendanceId",
                table: "StudentAttendances",
                column: "AttendanceId",
                principalTable: "Attendances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectDaySchedule_DaySchedules_DayScheduleId",
                table: "SubjectDaySchedule",
                column: "DayScheduleId",
                principalTable: "DaySchedules",
                principalColumn: "DayScheduleId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectDaySchedule_Subjects_SubjectId",
                table: "SubjectDaySchedule",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "subject_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Departments_DeptId",
                table: "Subjects",
                column: "DeptId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeTables_Classes_class_id",
                table: "TimeTables",
                column: "class_id",
                principalTable: "Classes",
                principalColumn: "Class_Id",
                onDelete: ReferentialAction.Restrict);
        }

    }
    
}
