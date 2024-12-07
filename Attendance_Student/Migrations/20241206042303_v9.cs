using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Attendance_Student.Migrations
{
    /// <inheritdoc />
    public partial class v9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Notification_NotificationId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Notification_AspNetUsers_Parent_Id",
                table: "Notification");

            migrationBuilder.DropForeignKey(
                name: "FK_Notification_AspNetUsers_admin_id",
                table: "Notification");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Notification",
                table: "Notification");

            migrationBuilder.RenameTable(
                name: "Notification",
                newName: "Notifications");

            migrationBuilder.RenameIndex(
                name: "IX_Notification_Parent_Id",
                table: "Notifications",
                newName: "IX_Notifications_Parent_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Notification_admin_id",
                table: "Notifications",
                newName: "IX_Notifications_admin_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notifications",
                table: "Notifications",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Notifications_NotificationId",
                table: "AspNetUsers",
                column: "NotificationId",
                principalTable: "Notifications",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_AspNetUsers_Parent_Id",
                table: "Notifications",
                column: "Parent_Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_AspNetUsers_admin_id",
                table: "Notifications",
                column: "admin_id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Notifications_NotificationId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_AspNetUsers_Parent_Id",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_AspNetUsers_admin_id",
                table: "Notifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Notifications",
                table: "Notifications");

            migrationBuilder.RenameTable(
                name: "Notifications",
                newName: "Notification");

            migrationBuilder.RenameIndex(
                name: "IX_Notifications_Parent_Id",
                table: "Notification",
                newName: "IX_Notification_Parent_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Notifications_admin_id",
                table: "Notification",
                newName: "IX_Notification_admin_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notification",
                table: "Notification",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Notification_NotificationId",
                table: "AspNetUsers",
                column: "NotificationId",
                principalTable: "Notification",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_AspNetUsers_Parent_Id",
                table: "Notification",
                column: "Parent_Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_AspNetUsers_admin_id",
                table: "Notification",
                column: "admin_id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
