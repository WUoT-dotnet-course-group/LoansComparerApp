namespace LoansComparer.CrossCutting.DTO;

public class GetInquiryDTO
{
    public int LoanValue { get; set; }
    public DateTime InquireDate { get; set; }
    public string OfferStatus { get; set; } = default!;
    public string ChosenBank { get; set; } = default!;
}
