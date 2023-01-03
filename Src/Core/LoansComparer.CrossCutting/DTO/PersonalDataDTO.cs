using LoansComparer.CrossCutting.Enums;

namespace LoansComparer.CrossCutting.DTO;

public class PersonalDataDTO
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public DateTime BirthDate { get; set; }
    public string GovernmentId { get; set; } = default!;
    public GovernmentIdType GovernmentIdType { get; set; }
    public JobType JobType { get; set; }
    public DateTime JobStartDate { get; set; }
    public DateTime? JobEndDate { get; set; }
}
