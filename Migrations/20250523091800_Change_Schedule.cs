using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymManagementProject.Migrations
{
    /// <inheritdoc />
    public partial class Change_Schedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Customers_CustomerId",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Services_ServiceId",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_CustomerId",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Schedules");

            migrationBuilder.RenameColumn(
                name: "ServiceId",
                table: "Schedules",
                newName: "InvoiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Schedules_ServiceId",
                table: "Schedules",
                newName: "IX_Schedules_InvoiceId");

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerEntityId",
                table: "Schedules",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ServiceEntityId",
                table: "Schedules",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_CustomerEntityId",
                table: "Schedules",
                column: "CustomerEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_ServiceEntityId",
                table: "Schedules",
                column: "ServiceEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Customers_CustomerEntityId",
                table: "Schedules",
                column: "CustomerEntityId",
                principalTable: "Customers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Invoices_InvoiceId",
                table: "Schedules",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Services_ServiceEntityId",
                table: "Schedules",
                column: "ServiceEntityId",
                principalTable: "Services",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Customers_CustomerEntityId",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Invoices_InvoiceId",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Services_ServiceEntityId",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_CustomerEntityId",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_ServiceEntityId",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "CustomerEntityId",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "ServiceEntityId",
                table: "Schedules");

            migrationBuilder.RenameColumn(
                name: "InvoiceId",
                table: "Schedules",
                newName: "ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Schedules_InvoiceId",
                table: "Schedules",
                newName: "IX_Schedules_ServiceId");

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId",
                table: "Schedules",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_CustomerId",
                table: "Schedules",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Customers_CustomerId",
                table: "Schedules",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Services_ServiceId",
                table: "Schedules",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
