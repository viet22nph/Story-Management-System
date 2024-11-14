using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineStory.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class removecolumndeletetablecomment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Comment");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Comment",
                type: "bit",
                nullable: true);
        }
    }
}
