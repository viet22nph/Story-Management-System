using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineStory.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updatetablecomment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Comment",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Left",
                table: "Comment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Right",
                table: "Comment",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "Left",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "Right",
                table: "Comment");
        }
    }
}
