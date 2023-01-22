using System.Text.Json.Serialization;

namespace LoansComparer.CrossCutting.DTO.LoaningBank
{
    public class CreateInquiryRequest
    {
        [JsonPropertyName("value")]
        public int LoanValue { get; set; }
        [JsonPropertyName("installmentsNumber")]
        public short NumberOfInstallments { get; set; }
        public PersonalData PersonalData { get; set; } = default!;
        public GovernmentDocument GovernmentDocument { get; set; } = default!;
        public JobDetails JobDetails { get; set; } = default!;
    }
}
