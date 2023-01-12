using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoansComparer.DataPersistence.Migrations
{
    /// <inheritdoc />
    public partial class AddInquirySearchView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "CREATE VIEW InquirySearch as " +
                    "SELECT i.ID as InquiryID, i.LoanValue, i.NumberOfInstallments, i.InquireDate, i.ChosenOfferId, i.UserID, b.ID as BankID, b.Name as BankName " +
                    "FROM Inquiries i LEFT JOIN Banks b ON i.BankID = b.ID;"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "DROP VIEW InquirySearch;"
            );
        }
    }
}
