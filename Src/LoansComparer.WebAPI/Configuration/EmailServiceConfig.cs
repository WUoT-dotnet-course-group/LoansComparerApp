namespace LoansComparer.WebAPI.Configuration
{
    public class EmailServiceConfig
    {
        public const string SectionName = "EmailService";

        public string Endpoint { get; set; } = default!;
        public string AccessKey { get; set; } = default!;
        public string Domain { get; set; } = default!;
    }
}
