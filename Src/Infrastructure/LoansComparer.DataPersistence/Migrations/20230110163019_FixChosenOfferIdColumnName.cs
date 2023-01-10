using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoansComparer.DataPersistence.Migrations
{
    /// <inheritdoc />
    public partial class FixChosenOfferIdColumnName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ChosenBankInquiryId",
                table: "Inquiries",
                newName: "ChosenOfferId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ChosenOfferId",
                table: "Inquiries",
                newName: "ChosenBankInquiryId");
        }
    }
}
