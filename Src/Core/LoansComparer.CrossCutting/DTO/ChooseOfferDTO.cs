namespace LoansComparer.CrossCutting.DTO
{
    public class ChooseOfferDTO
    {
        public Guid OfferId { get; set; }
        public Guid? BankId { get; set; } // not used as long as service handles only one bank
    }
}
