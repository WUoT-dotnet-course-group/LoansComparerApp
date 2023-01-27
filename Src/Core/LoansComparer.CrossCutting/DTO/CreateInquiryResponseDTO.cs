namespace LoansComparer.CrossCutting.DTO
{
    public class CreateInquiryResponseDTO
    {
        public Guid InquiryId { get; set; }
        public Dictionary<string, string> BankInquiries { get; set; } = default!;
    }
}
