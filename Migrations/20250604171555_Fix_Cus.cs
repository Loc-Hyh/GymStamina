using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymManagementProject.Migrations
{
    /// <inheritdoc />
    public partial class Fix_Cus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Trainers_TrainerId",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_TrainerId",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "TrainerId",
                table: "Schedules");

            migrationBuilder.AddColumn<Guid>(
                name: "TrainerId",
                table: "Invoices",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Customers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_TrainerId",
                table: "Invoices",
                column: "TrainerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Trainers_TrainerId",
                table: "Invoices",
                column: "TrainerId",
                principalTable: "Trainers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Trainers_TrainerId",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_TrainerId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "TrainerId",
                table: "Invoices");

            migrationBuilder.AddColumn<Guid>(
                name: "TrainerId",
                table: "Schedules",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Customers",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_TrainerId",
                table: "Schedules",
                column: "TrainerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Trainers_TrainerId",
                table: "Schedules",
                column: "TrainerId",
                principalTable: "Trainers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
