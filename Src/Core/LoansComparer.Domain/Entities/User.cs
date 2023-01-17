using LoansComparer.CrossCutting.Enums;

namespace LoansComparer.Domain.Entities
{
    public class User
    {
        public Guid ID { get; set; }
        public string? Email { get; set; }
        public PersonalData? PersonalData { get; set; }
        public UserRole Role { get; set; }

        public Guid? BankID { get; set; }
        public virtual Bank? Bank { get; set; }

        public virtual ICollection<Inquiry> Inquiries { get; set; } = default!;
    }
}
