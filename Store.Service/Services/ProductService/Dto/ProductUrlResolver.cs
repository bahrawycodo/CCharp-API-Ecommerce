using AutoMapper;
using Microsoft.Extensions.Configuration;
using Store.Data.Entities;

namespace Store.Service.Services.ProductService.Dto
{
    public class ProductUrlResolver : IValueResolver<Product,ProductDetailsDto,string>
    {
        private readonly IConfiguration _configuration;

        public ProductUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Product source, ProductDetailsDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
                return $"{_configuration["BaseUrl"]}{source.PictureUrl}";
            return String.Empty;
        }

    }
}
