namespace LoansComparer.CrossCutting.DTO
{
    public class JobDetailsDTO
    {
        public DictionaryDTO JobType { get; set; } = default!;
        public DateTime JobStartDate { get; set; }
        public DateTime? JobEndDate { get; set; }
    }
}
