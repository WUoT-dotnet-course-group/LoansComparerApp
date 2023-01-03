using LoansComparer.CrossCutting.Enums;

namespace LoansComparer.CrossCutting.DTO;

public class GetInquiryDTO
{
    public int LoanValue { get; set; }
    public DateTime InquireDate { get; set; }
    public string OfferStatus { get; set; } = Enums.OfferStatus.Unknown.GetEnumDescription();
    public string ChosenBank { get; set; } = "Test";
}
