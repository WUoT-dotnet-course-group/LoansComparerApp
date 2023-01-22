namespace LoansComparer.Domain.Entities
{
    public class Inquiry
    {
        public Guid ID { get; set; }
        public int LoanValue { get; set; }
        public short NumberOfInstallments { get; set; }
        public DateTime InquireDate { get; set; }
        public string? ChosenBankId { get; set; }
        public string? ChosenOfferId { get; set; }

        public Guid UserID { get; set; }
        public virtual User User { get; set; } = default!;
    }
}
