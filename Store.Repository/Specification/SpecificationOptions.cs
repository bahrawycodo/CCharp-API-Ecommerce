namespace Store.Repository.Specification
{
    public class SpecificationOptions : ISpecificationOptions
    {
        private const int MAXPAGESIZE = 50;
        public int PageIndex { get; set; } = 1;
        private int _pageSize = 5;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MAXPAGESIZE) ? MAXPAGESIZE : value;
        }
        private string? _search;
        public string? Search
        {
            get => _search;
            set => _search = value?.Trim().ToLower();
        }
        public string? Order { get; set; }

        private string _orderDir = "ASC";
        public string OrderDir 
        { 
            get=>_orderDir;
            set=>_orderDir = (string.IsNullOrEmpty(value?.Trim()) ? "ASC" : value.Trim().ToLower() == "desc"? "DESC": "ASC") ; 
        }
    }
}
