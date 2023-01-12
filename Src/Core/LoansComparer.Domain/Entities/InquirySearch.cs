namespace LoansComparer.Domain.Entities
{
    public class InquirySearch
    {
        public Guid InquiryID { get; set; }
        public int LoanValue { get; set; }
        public short NumberOfInstallments { get; set; }
        public DateTime InquireDate { get; set; }
        public Guid ChosenOfferId { get; set; }
        public Guid UserID { get; set; }
        public Guid? BankID { get; set; }
        public string? BankName { get; set; }
    }
}
