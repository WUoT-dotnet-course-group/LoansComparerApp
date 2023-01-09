namespace LoansComparer.CrossCutting.DTO
{
    public class PaginatedResponse<T>
    {
        public List<T> Items { get; set; } = default!;
        public int TotalNumber { get; set; }
    }
}
