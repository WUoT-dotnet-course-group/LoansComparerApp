using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoansComparer.DataPersistence.Migrations
{
    /// <inheritdoc />
    public partial class RemoveAllBankReferences : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inquiries_Banks_BankID",
                table: "Inquiries");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Banks_BankID",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Banks");

            migrationBuilder.DropIndex(
                name: "IX_Users_BankID",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Inquiries_BankID",
                table: "Inquiries");

            migrationBuilder.DropColumn(
                name: "BankID",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "BankID",
                table: "Inquiries");

            migrationBuilder.AddColumn<string>(
                name: "ChosenBankId",
                table: "Inquiries",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.Sql(
                "DROP VIEW InquirySearch;"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChosenBankId",
                table: "Inquiries");

            migrationBuilder.AddColumn<Guid>(
                name: "BankID",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BankID",
                table: "Inquiries",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Banks",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Domain = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GetInquiryRoute = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GetOfferDocumentRoute = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GetOfferRoute = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostInquiryRoute = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostOfferDocumentRoute = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostOfferRoute = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banks", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_BankID",
                table: "Users",
                column: "BankID");

            migrationBuilder.CreateIndex(
                name: "IX_Inquiries_BankID",
                table: "Inquiries",
                column: "BankID");

            migrationBuilder.AddForeignKey(
                name: "FK_Inquiries_Banks_BankID",
                table: "Inquiries",
                column: "BankID",
                principalTable: "Banks",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Banks_BankID",
                table: "Users",
                column: "BankID",
                principalTable: "Banks",
                principalColumn: "ID");

            migrationBuilder.Sql(
                "CREATE VIEW InquirySearch as " +
                    "SELECT i.ID as InquiryID, i.LoanValue, i.NumberOfInstallments, i.InquireDate, i.ChosenOfferId, i.UserID, b.ID as BankID, b.Name as BankName " +
                    "FROM Inquiries i LEFT JOIN Banks b ON i.BankID = b.ID;"
            );
        }
    }
}
