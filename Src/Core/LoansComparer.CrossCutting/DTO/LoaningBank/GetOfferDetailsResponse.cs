using LoansComparer.CrossCutting.Enums;

namespace LoansComparer.CrossCutting.DTO.LoaningBank
{
    public class GetOfferDetailsResponse
    {
        public string InquiryID { get; set; } = default!;
        public string? OfferID { get; set; }
        public int LoanValue { get; set; }
        public short NumberOfInstallments { get; set; }
        public DateTime InquireDate { get; set; }
        public OfferStatus Status { get; set; }
        public string StatusDescription { get; set; } = default!;
        public float? Percentage { get; set; }
        public DateTime? OfferCreateDate { get; set; }
        public DateTime? OfferUpdateDate { get; set; }
        public string? ApprovedBy { get; set; }
    }
}