using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymManagementProject.Migrations
{
    /// <inheritdoc />
    public partial class Update_Schedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Schedules");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Schedules",
                newName: "PracticeDay");

            migrationBuilder.AddColumn<bool>(
                name: "IsCheckedIn",
                table: "Schedules",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCheckedIn",
                table: "Schedules");

            migrationBuilder.RenameColumn(
                name: "PracticeDay",
                table: "Schedules",
                newName: "StartDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Schedules",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
