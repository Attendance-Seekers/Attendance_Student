using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Attendance_Student.Migrations
{
    /// <inheritdoc />
    public partial class delete_relation_between_students_and_notification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Notifications_NotificationId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_NotificationId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "NotificationId",
                table: "AspNetUsers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NotificationId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_NotificationId",
                table: "AspNetUsers",
                column: "NotificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Notifications_NotificationId",
                table: "AspNetUsers",
                column: "NotificationId",
                principalTable: "Notifications",
                principalColumn: "Id");
        }
    }
}
