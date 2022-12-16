namespace LoansComparer.Domain.Entities
{
    public abstract class Inquiry
    {
        public Guid Id { get; set; }
        public int LoanValue { get; set; }
        public short NumberOfInstallments { get; set; }
        public DateTime InquireDate { get; set; }
        public PersonalData PersonalData { get; set; } = default!;
        public Guid ChosenBankId { get; set; }
        public Guid ChosenBankInquiryId { get; set; }
        public Guid UserId { get; set; }
    }
}
