namespace LoansComparer.CrossCutting.DTO
{
    public class AuthDTO
    {
        public string EncryptedToken { get; set; } = default!;
        public string UserEmail { get; set; } = default!;
        public Guid UserId { get; set; }
    }
}
