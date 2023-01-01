namespace LoansComparer.CrossCutting.DTO
{
    public class SaveUserDTO
    {
        public string Email { get; set; } = default!;
        public PersonalDataDTO? PersonalData { get; set; }
    }
}
