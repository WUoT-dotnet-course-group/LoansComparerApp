using LoansComparer.CrossCutting.Enums;
using System.Text.Json.Serialization;

namespace LoansComparer.CrossCutting.DTO.LoaningBank
{
    public class GetInquiryResponse
    {
        [JsonPropertyName("inquireId")]
        public string ID { get; set; } = default!;

        [JsonPropertyName("createDate")]
        public DateTime InquireDate { get; set; }

        public string? OfferId { get; set; }

        [JsonPropertyName("statusId")]
        public OfferStatus OfferStatus { get; set; }
    }
}
