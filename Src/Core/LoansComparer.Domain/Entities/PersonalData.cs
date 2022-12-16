namespace LoansComparer.Domain.Entities
{
    public class PersonalData
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string GovernmentId { get; set; } = default!;
        public GovernmentIdType GovernmentIdType { get; set; }
    }
}
