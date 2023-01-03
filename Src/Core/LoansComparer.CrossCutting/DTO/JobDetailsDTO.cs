namespace LoansComparer.CrossCutting.DTO
{
    public class JobDetailsDTO : DictionaryDTO
    {
        public DateTime JobStartDate { get; set; }
        public DateTime? JobEndDate { get; set; }
    }
}
