using AutoMapper;
using Store.Data.Entities;
using Store.Repository.Interfaces;
using Store.Repository.Specification.ProductSpecification;
using Store.Service.Helper;
using Store.Service.Services.ProductService.Dto;

namespace Store.Service.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IReadOnlyList<BrandTypeDetailsDto>> GetAllBrandsAsync()
        {
           var brands= await _unitOfWork.Repository<ProductBrand, int>().GetAllAsync();
            return _mapper.Map<IReadOnlyList<BrandTypeDetailsDto>>(brands);
        }

        public async Task<PaginatedResultDto<ProductDetailsDto>> GetAllProductsAsync(ProductSpecification input)
        {
            var specs = new ProductWithSpecifications(input);
            var countSpecs = new ProductWithFilterAndCountSpecification(input);
            var products = await _unitOfWork.Repository<Product, int>().GetAllWithSpecificationAsync(specs);
            var productsCount = await _unitOfWork.Repository<Product, int>().CountWithSpecificationAsync(countSpecs);
            var mappedProducts = _mapper.Map<IReadOnlyList<ProductDetailsDto>>(products);
            return new PaginatedResultDto<ProductDetailsDto>(input.PageIndex,input.PageSize,productsCount, mappedProducts);
        }

        public async Task<IReadOnlyList<BrandTypeDetailsDto>> GetAllTypesAsync()
        {
            var types = await _unitOfWork.Repository<ProductType, int>().GetAllAsync();
            return _mapper.Map<IReadOnlyList<BrandTypeDetailsDto>>(types);
        }

        public async Task<ProductDetailsDto> GetProductByIdAsync(int? id)
        {
            if (id is null)
                throw new Exception("Id is Null");
            var specs = new ProductWithSpecifications(id);
            var product = await _unitOfWork.Repository<Product, int>().GetWithSpecificationByIdAsync(specs);
            if (product is null)
                throw new Exception("Product Not Found");

            return _mapper.Map<ProductDetailsDto>(product);
        }
    }
}
