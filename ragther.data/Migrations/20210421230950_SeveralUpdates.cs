using Microsoft.EntityFrameworkCore.Migrations;

namespace ragther.data.Migrations
{
    public partial class SeveralUpdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "Todos");

            migrationBuilder.AddColumn<string>(
                name: "LocationLatitude",
                table: "Todos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LocationLongitude",
                table: "Todos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocationLatitude",
                table: "Todos");

            migrationBuilder.DropColumn(
                name: "LocationLongitude",
                table: "Todos");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Todos",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);
        }
    }
}
