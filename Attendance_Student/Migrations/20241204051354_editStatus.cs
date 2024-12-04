using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Attendance_Student.Migrations
{
    /// <inheritdoc />
    public partial class editStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Departments_dept_id",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_dept_id",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "dept_id",
                table: "Subjects");

            migrationBuilder.AddColumn<int>(
                name: "DeptId",
                table: "Subjects",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AlterColumn<int>(
                name: "status",
                table: "AspNetUsers",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_DeptId",
                table: "Subjects",
                column: "DeptId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Departments_DeptId",
                table: "Subjects",
                column: "DeptId",
                principalTable: "Departments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Departments_DeptId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_DeptId",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "DeptId",
                table: "Subjects");

            migrationBuilder.AddColumn<int>(
                name: "dept_id",
                table: "Subjects",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_dept_id",
                table: "Subjects",
                column: "dept_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Departments_dept_id",
                table: "Subjects",
                column: "dept_id",
                principalTable: "Departments",
                principalColumn: "Id");
        }
    }
}
