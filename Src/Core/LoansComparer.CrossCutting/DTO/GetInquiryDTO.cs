using LoansComparer.CrossCutting.Utils;

namespace LoansComparer.CrossCutting.DTO;

public class GetInquiryDTO
{
    [SortHeader("loanValue")]
    [EntityPropertyName("LoanValue")]
    public int LoanValue { get; set; }

    [SortHeader("installments")]
    [EntityPropertyName("NumberOfInstallments")]
    public short NumberOfInstallments { get; set; }

    [SortHeader("inquireDate")]
    [EntityPropertyName("InquireDate")]
    public DateTime InquireDate { get; set; }
}
