using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymManagementProject.Migrations
{
    /// <inheritdoc />
    public partial class Update_Ser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DateTrain",
                table: "Services",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateTrain",
                table: "Services");
        }
    }
}
