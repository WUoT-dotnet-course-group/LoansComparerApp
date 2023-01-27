namespace LoansComparer.Domain.Options
{
    public class OtherTeamBankConfig
    {
        public static string SectionName => "OtherTeamBank";

        public string Domain { get; set; } = default!;
        public string AuthToken { get; set; } = default!;
    }
}
