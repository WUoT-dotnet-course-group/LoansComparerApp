using LoansComparer.CrossCutting.Enums;

namespace LoansComparer.CrossCutting.DTO;

public class PersonalDataDTO
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string GovernmentId { get; set; } = default!;
    public GovernmentIdType GovernmentIdType { get; set; }
}
