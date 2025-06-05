using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymManagementProject.Migrations
{
    /// <inheritdoc />
    public partial class Fix_Invoices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Staffs_StaffId",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_StaffId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "StaffId",
                table: "Invoices");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_CreateById",
                table: "Invoices",
                column: "CreateById");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Staffs_CreateById",
                table: "Invoices",
                column: "CreateById",
                principalTable: "Staffs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Staffs_CreateById",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_CreateById",
                table: "Invoices");

            migrationBuilder.AddColumn<Guid>(
                name: "StaffId",
                table: "Invoices",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_StaffId",
                table: "Invoices",
                column: "StaffId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Staffs_StaffId",
                table: "Invoices",
                column: "StaffId",
                principalTable: "Staffs",
                principalColumn: "Id");
        }
    }
}
