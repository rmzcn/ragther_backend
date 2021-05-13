using Microsoft.EntityFrameworkCore.Migrations;

namespace ragther.data.Migrations
{
    public partial class CommentTableUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "offerStatus",
                table: "Comments",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "offerStatus",
                table: "Comments");
        }
    }
}
