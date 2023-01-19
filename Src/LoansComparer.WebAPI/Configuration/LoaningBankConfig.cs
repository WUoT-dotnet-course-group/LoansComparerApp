namespace LoansComparer.WebAPI.Configuration
{
    public class LoaningBankConfig
    {
        public static string SectionName => "LoaningBank";

        public string Domain { get; set; } = default!;
        public string ClientId { get; set; } = default!;
        public string ClientSecret { get; set; } = default!;
    }
}
