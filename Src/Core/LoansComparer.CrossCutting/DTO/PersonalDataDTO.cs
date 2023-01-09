namespace LoansComparer.CrossCutting.DTO;

public class PersonalDataDTO
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public DateTime BirthDate { get; set; }
    public string? Email { get; set; }
    public GovernmentDocumentDTO GovernmentDocument { get; set; } = default!;
    public JobDetailsDTO JobDetails { get; set; } = default!;
}
