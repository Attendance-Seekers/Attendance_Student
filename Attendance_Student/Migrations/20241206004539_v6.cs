using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Attendance_Student.Migrations
{
    /// <inheritdoc />
    public partial class v6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NotificationId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    sendDate = table.Column<DateOnly>(type: "date", nullable: false),
                    admin_id = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notification_AspNetUsers_admin_id",
                        column: x => x.admin_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_NotificationId",
                table: "AspNetUsers",
                column: "NotificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_admin_id",
                table: "Notification",
                column: "admin_id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Notification_NotificationId",
                table: "AspNetUsers",
                column: "NotificationId",
                principalTable: "Notification",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Notification_NotificationId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_NotificationId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "NotificationId",
                table: "AspNetUsers");
        }
    }
}
