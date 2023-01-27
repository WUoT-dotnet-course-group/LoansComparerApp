namespace LoansComparer.Domain.Options
{
    public class LecturerBankConfig
    {
        public static string SectionName => "LecturerBank";

        public string Domain { get; set; } = default!;
        public string ClientId { get; set; } = default!;
        public string ClientSecret { get; set; } = default!;
    }
}
