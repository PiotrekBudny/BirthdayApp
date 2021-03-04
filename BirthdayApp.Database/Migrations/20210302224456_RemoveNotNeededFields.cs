using Microsoft.EntityFrameworkCore.Migrations;

namespace BirthdayTracker.Database.Migrations
{
    public partial class RemoveNotNeededFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "BirthdayInfo");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "BirthdayInfo");

            migrationBuilder.DropColumn(
                name: "SecondName",
                table: "BirthdayInfo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "BirthdayInfo",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "BirthdayInfo",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecondName",
                table: "BirthdayInfo",
                type: "TEXT",
                nullable: true);
        }
    }
}
