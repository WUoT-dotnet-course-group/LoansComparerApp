namespace LoansComparer.Domain.Entities
{
    public class Inquiry
    {
        public Guid ID { get; set; }
        public int LoanValue { get; set; }
        public short NumberOfInstallments { get; set; }
        public DateTime InquireDate { get; set; }
        public Guid ChosenBankInquiryId { get; set; }

        public Guid UserID { get; set; }
        public User User { get; set; } = default!;

        public Guid? BankID { get; set; }
        public Bank? Bank { get; set; } = default!;
    }
}
