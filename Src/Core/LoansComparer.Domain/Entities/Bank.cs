namespace LoansComparer.Domain.Entities
{
    public class Bank
    {
        public Guid ID { get; set; }
        public string Name { get; set; } = default!;
        public string Domain { get; set; } = default!;

        public string PostInquiryRoute { get; set; } = default!;
        public string GetInquiryRoute { get; set; } = default!;

        public string PostOfferRoute { get; set; } = default!;
        public string GetOfferRoute { get; set; } = default!;

        public string PostOfferDocumentRoute { get; set; } = default!;
        public string GetOfferDocumentRoute { get; set; } = default!;

        public ICollection<Inquiry> Inquiries { get; set; } = default!;
        public ICollection<User> Employees { get; set; } = default!;
    }
}
