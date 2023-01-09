using LoansComparer.CrossCutting.Enums;
using System.Text.Json.Serialization;

namespace LoansComparer.CrossCutting.DTO.LoaningBank
{
    public class GetOfferResponse
    {
        public Guid Id { get; set; }

        [JsonPropertyName("requestedValue")]
        public int LoanValue { get; set; }

        [JsonPropertyName("requestedPeriodInMonth")]
        public short LoanPeriod { get; set; }

        [JsonPropertyName("statusId")]
        public OfferStatus Status { get; set; }

        [JsonPropertyName("inquireId")]
        public Guid InquiryId { get; set; }

        public float Percentage { get; set; }
        public float MonthlyInstallment { get; set; }
        public string StatusDescription { get; set; } = default!;
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string? ApprovedBy { get; set; }
        public string DocumentLink { get; set; } = default!;
        public DateTime DocumentLinkValidDate { get; set; }
    }
}
