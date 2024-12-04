using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Attendance_Student.Migrations
{
    /// <inheritdoc />
    public partial class deptsubj : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "dept_id",
                table: "Subjects",
                type: "int",
                nullable: false,
                defaultValue: 1);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
