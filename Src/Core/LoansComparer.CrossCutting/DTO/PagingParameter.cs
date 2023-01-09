namespace LoansComparer.CrossCutting.DTO
{
    public class PagingParameter
    {
        public string? SortOrder { get; set; }
        public string? SortHeader { get; set; }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
