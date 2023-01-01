namespace LoansComparer.Domain.Entities
{
    public class User
    {
        public Guid ID { get; set; }
        public string Email { get; set; } = default!;
        public PersonalData? PersonalData { get; set; }

        public Guid? BankID { get; set; }
        public Bank? Bank { get; set; } = default!;

        public ICollection<Inquiry> Inquiries { get; set; } = default!;
    }
}
