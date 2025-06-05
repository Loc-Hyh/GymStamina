using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymManagementProject.Migrations
{
    /// <inheritdoc />
    public partial class Fix_Invoice4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Customers_CustomerId",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Services_ServicesId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "CreateByName",
                table: "Invoices");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_StaffId",
                table: "Invoices",
                column: "StaffId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Customers_CustomerId",
                table: "Invoices",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Services_ServicesId",
                table: "Invoices",
                column: "ServicesId",
                principalTable: "Services",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Staffs_StaffId",
                table: "Invoices",
                column: "StaffId",
                principalTable: "Staffs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Customers_CustomerId",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Services_ServicesId",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Staffs_StaffId",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_StaffId",
                table: "Invoices");

            migrationBuilder.AddColumn<Guid>(
                name: "CreateByName",
                table: "Invoices",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Customers_CustomerId",
                table: "Invoices",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Services_ServicesId",
                table: "Invoices",
                column: "ServicesId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
