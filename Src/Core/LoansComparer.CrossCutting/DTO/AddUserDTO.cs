namespace LoansComparer.CrossCutting.DTO
{
    public class AddUserDTO
    {
        public string Email { get; set; } = default!;
        public PersonalDataDTO? PersonalData { get; set; }
    }
}
