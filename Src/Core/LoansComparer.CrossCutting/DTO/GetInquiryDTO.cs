using LoansComparer.CrossCutting.Enums;

namespace LoansComparer.CrossCutting.DTO;

public class GetInquiryDTO
{
    public int LoanValue { get; set; }
    public DateTime InquireDate { get; set; }
    public OfferStatus OfferStatus { get; set; } = OfferStatus.Unknown;
    public string ChosenBank { get; set; } = "Test";
}
