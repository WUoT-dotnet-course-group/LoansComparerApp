using LoansComparer.CrossCutting.Enums;
using System.Text.Json.Serialization;

namespace LoansComparer.CrossCutting.DTO.LoaningBank
{
    public class GetInquiryResponse
    {
        [JsonPropertyName("inquireId")]
        public Guid ID { get; set; }

        [JsonPropertyName("createDate")]
        public DateTime InquireDate { get; set; }

        public Guid? OfferId { get; set; }

        [JsonPropertyName("statusId")]
        public OfferStatus OfferStatus { get; set; }
    }
}
