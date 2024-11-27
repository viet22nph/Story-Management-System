using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineStory.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updatechaptertablev1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "ChapterImage",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "CreateBy",
                table: "Chapter",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UpdateBy",
                table: "Chapter",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "ChapterImage");

            migrationBuilder.DropColumn(
                name: "CreateBy",
                table: "Chapter");

            migrationBuilder.DropColumn(
                name: "UpdateBy",
                table: "Chapter");
        }
    }
}
