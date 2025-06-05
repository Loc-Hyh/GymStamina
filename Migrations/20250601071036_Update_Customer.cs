using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymManagementProject.Migrations
{
    /// <inheritdoc />
    public partial class Update_Customer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Branches_BranchId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_BranchId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "Customers");

            migrationBuilder.AddColumn<Guid>(
                name: "BranchEntityId",
                table: "Customers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_BranchEntityId",
                table: "Customers",
                column: "BranchEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Branches_BranchEntityId",
                table: "Customers",
                column: "BranchEntityId",
                principalTable: "Branches",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Branches_BranchEntityId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_BranchEntityId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "BranchEntityId",
                table: "Customers");

            migrationBuilder.AddColumn<Guid>(
                name: "BranchId",
                table: "Customers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Customers_BranchId",
                table: "Customers",
                column: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Branches_BranchId",
                table: "Customers",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
