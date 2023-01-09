namespace LoansComparer.CrossCutting.DTO.LoaningBank
{
    public class JobDetails : DictionaryDTO
    {
        public DateTime JobStartDate { get; set; }
        public DateTime? JobEndDate { get; set; }
    }
}
