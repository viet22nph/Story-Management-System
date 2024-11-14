using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineStory.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updatenotification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserNotification_AppUser_UserId",
                table: "UserNotification");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserNotification",
                newName: "UserReceiveId");

            migrationBuilder.RenameIndex(
                name: "IX_UserNotification_UserId_NotificationId",
                table: "UserNotification",
                newName: "IX_UserNotification_UserReceiveId_NotificationId");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ReceivedDate",
                table: "UserNotification",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<bool>(
                name: "IsGlobal",
                table: "Notification",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_UserNotification_AppUser_UserReceiveId",
                table: "UserNotification",
                column: "UserReceiveId",
                principalTable: "AppUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserNotification_AppUser_UserReceiveId",
                table: "UserNotification");

            migrationBuilder.DropColumn(
                name: "ReceivedDate",
                table: "UserNotification");

            migrationBuilder.DropColumn(
                name: "IsGlobal",
                table: "Notification");

            migrationBuilder.RenameColumn(
                name: "UserReceiveId",
                table: "UserNotification",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserNotification_UserReceiveId_NotificationId",
                table: "UserNotification",
                newName: "IX_UserNotification_UserId_NotificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserNotification_AppUser_UserId",
                table: "UserNotification",
                column: "UserId",
                principalTable: "AppUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
