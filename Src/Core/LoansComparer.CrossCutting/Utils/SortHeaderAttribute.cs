namespace LoansComparer.CrossCutting.Utils
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SortHeaderAttribute : Attribute
    {
        private readonly string _headerName;

        public SortHeaderAttribute(string headerName)
        {
            _headerName = headerName;
        }

        public string HeaderName { get => _headerName; }
    }
}
