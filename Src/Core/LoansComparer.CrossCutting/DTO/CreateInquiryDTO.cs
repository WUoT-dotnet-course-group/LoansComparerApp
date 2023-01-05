namespace LoansComparer.CrossCutting.DTO
{
    public class CreateInquiryDTO
    {
        public int LoanValue { get; set; }
        public short NumberOfInstallments { get; set; }
        public PersonalDataDTO PersonalData { get; set; } = default!;
    }
}
