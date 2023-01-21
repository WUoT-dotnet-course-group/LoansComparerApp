namespace LoansComparer.CrossCutting.DTO
{
    public class ChooseOfferDTO
    {
        public string OfferId { get; set; } = default!;
        public Guid? BankId { get; set; } // not used as long as service handles only one bank
    }
}
