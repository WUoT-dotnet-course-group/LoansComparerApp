namespace LoansComparer.CrossCutting.DTO
{
    public class CreateInquiryResponseDTO
    {
        public Guid InquiryId { get; set; }
        public string BankInquiryId { get; set; } = default!;
    }
}
