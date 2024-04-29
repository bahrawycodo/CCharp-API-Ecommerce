using Store.Data.Entities;

namespace Store.Repository.Specification.ProductSpecification
{
    public class ProductWithFilterAndCountSpecification : BaseSpecification<Product>
    {
        public ProductWithFilterAndCountSpecification(ProductSpecification specs) : base(
            product => (!specs.BrandId.HasValue || product.BrandId == specs.BrandId.Value) &&
                     (!specs.TypeId.HasValue || product.TypeId == specs.TypeId.Value) &&
                     (string.IsNullOrEmpty(specs.Search) || product.Name.Trim().ToLower().Contains(specs.Search))&&
                     (string.IsNullOrEmpty(specs.Search) || product.Description.Trim().ToLower().Contains(specs.Search))

            )
        {
        }
    }
}
