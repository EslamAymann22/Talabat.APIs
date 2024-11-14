using AutoMapper;
using Talabat.APIsProject.DTOs;
using Talabat.Core.Entities;

namespace Talabat.APIsProject.Helper
{
    public class ProductUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
    {
        private readonly IConfiguration _configuration;

        public ProductUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            return (!string.IsNullOrEmpty(source.PictureUrl)) ?
                $"{_configuration["ApiBaseUrl"]}/{source.PictureUrl}" : "";
        }
    }
}
