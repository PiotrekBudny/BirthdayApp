using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BirthdayTracker.Database.Migrations
{
    public partial class StoreDayOfBirthAsDateTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DayOfBirth",
                table: "BirthdayInfo");

            migrationBuilder.DropColumn(
                name: "MonthOfBirth",
                table: "BirthdayInfo");

            migrationBuilder.DropColumn(
                name: "YearOfBirth",
                table: "BirthdayInfo");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "BirthdayInfo",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "BirthdayInfo");

            migrationBuilder.AddColumn<int>(
                name: "DayOfBirth",
                table: "BirthdayInfo",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MonthOfBirth",
                table: "BirthdayInfo",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "YearOfBirth",
                table: "BirthdayInfo",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
