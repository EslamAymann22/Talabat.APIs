using Talabat.Core.Entities;

namespace Talabat.APIsProject.DTOs
{
    public class ProductToReturnDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }

        //[ForeignKey("ProductBrand")]
        public int ProductBrandId { get; set; }
        public string ProductBrand { get; set; }


        //[ForeignKey("ProductType")]
        public int ProductTypeId { get; set; }
        public string ProductType { get; set; }

    }
}
