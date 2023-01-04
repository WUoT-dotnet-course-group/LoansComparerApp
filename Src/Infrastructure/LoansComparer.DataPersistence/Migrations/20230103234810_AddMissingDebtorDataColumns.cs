using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoansComparer.DataPersistence.Migrations
{
    /// <inheritdoc />
    public partial class AddMissingDebtorDataColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "JobEndDate",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "JobStartDate",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "JobType",
                table: "Users",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "JobEndDate",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "JobStartDate",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "JobType",
                table: "Users");
        }
    }
}
