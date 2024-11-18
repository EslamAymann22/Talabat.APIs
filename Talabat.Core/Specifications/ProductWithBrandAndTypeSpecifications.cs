using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class ProductWithBrandAndTypeSpecifications : BaseSpecifications<Product> 
    {

        public ProductWithBrandAndTypeSpecifications(ProductSpecParams Params) :
            base(P =>
                (!Params.BrandId.HasValue || P.ProductBrandId == Params.BrandId)
                && (!Params.TypeId.HasValue || P.ProductTypeId == Params.TypeId)
                && (string.IsNullOrEmpty(Params.Search) || P.Name.ToLower().Contains(Params.Search))
                ) { 

            Includes.Add(P=>P.ProductBrand);
            Includes.Add(P=>P.ProductType);
            if(!string.IsNullOrEmpty(Params.Sort))
                switch (Params.Sort.ToLower())
                {
                    case "price":
                        AddOrderBy(P => P.Price);
                        break;
                    case "pricedesc":
                        AddOrderByDesc(P => P.Price);
                        break;
                    default:
                        AddOrderBy(P => P.Name);
                        break;
                }

            {
                int Skip = (Params.PageIndex - 1) * Params.PageSize;
                int Take = Params.PageSize;
                ApplyPagination(Skip, Take);
            }



        }
        public ProductWithBrandAndTypeSpecifications(int id) : base(p => p.Id == id)
        {
            Includes.Add(P => P.ProductBrand);
            Includes.Add(P => P.ProductType);
        }
    }
}
