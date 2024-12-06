using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Attendance_Student.Migrations
{
    /// <inheritdoc />
    public partial class v8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Parent_Id",
                table: "Notification",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_Parent_Id",
                table: "Notification",
                column: "Parent_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_AspNetUsers_Parent_Id",
                table: "Notification",
                column: "Parent_Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notification_AspNetUsers_Parent_Id",
                table: "Notification");

            migrationBuilder.DropIndex(
                name: "IX_Notification_Parent_Id",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "Parent_Id",
                table: "Notification");
        }
    }
}
