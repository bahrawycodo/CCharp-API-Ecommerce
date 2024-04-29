namespace Store.Repository.Specification
{

    public interface ISpecificationOptions
    {
        public int PageIndex { get; set; }
        public int PageSize { get;set;}
        public  string? Search { get;set;}
        public string? Order { get; set; }
        public string OrderDir { get; set; }

    }
}
