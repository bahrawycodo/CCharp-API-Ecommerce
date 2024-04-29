using Store.Data.Entities;

namespace Store.Repository.Specification.ProductSpecification
{
    public class ProductWithSpecifications : BaseSpecification<Product>
    {
        public ProductWithSpecifications(ProductSpecification specs) : base(
            product=>(!specs.BrandId.HasValue || product.BrandId == specs.BrandId.Value) &&
                     (!specs.TypeId.HasValue || product.TypeId == specs.TypeId.Value )&&
                     (string.IsNullOrEmpty(specs.Search) || product.Name.Trim().ToLower().Contains(specs.Search) )&&
                     (string.IsNullOrEmpty(specs.Search) || product.Description.Trim().ToLower().Contains(specs.Search))

            )
        {
            AddInclude(x => x.Brand);
            AddInclude(x => x.Type);

            ApplyPagination((specs.PageIndex-1) * specs.PageSize,specs.PageSize);
            AddOrderByDir(specs.OrderDir);

            if (!string.IsNullOrEmpty(specs.Order))
            {
                switch (specs.Order.Trim().ToLower())
                {
                    case "name":
                        AddOrderBy(x => x.Name);
                        break;
                    case "price":
                        AddOrderBy(x => x.Price);
                        break;
                    default:
                        AddOrderBy(x => x.Id);
                        break;
                }
                
            }
            else
                AddOrderBy(x => x.Id);
        } 
        public ProductWithSpecifications(int? id) : base(product=>product.Id == id)
        {
            AddInclude(x => x.Brand);
            AddInclude(x => x.Type);
        }

    }
}
