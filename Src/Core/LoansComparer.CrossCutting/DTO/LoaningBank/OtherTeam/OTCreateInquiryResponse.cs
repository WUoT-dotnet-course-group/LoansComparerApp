using System.Text.Json.Serialization;

namespace LoansComparer.CrossCutting.DTO.LoaningBank.OtherTeam
{
    public class OTCreateInquiryResponse
    {
        [JsonPropertyName("applicationId")]
        public string InquiryId { get; set; } = default!;
        public DateTime CreateDate { get; set; }
    }
}
