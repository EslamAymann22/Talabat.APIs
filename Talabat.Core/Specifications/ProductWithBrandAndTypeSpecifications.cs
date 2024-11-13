﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class ProductWithBrandAndTypeSpecifications : BaseSpecifications<Product> 
    {

        public ProductWithBrandAndTypeSpecifications() :base() { 
            Includes.Add(P=>P.ProductBrand);
            Includes.Add(P=>P.ProductType);
        }
        public ProductWithBrandAndTypeSpecifications(int id) : base(p => p.Id == id)
        {
            Includes.Add(P => P.ProductBrand);
            Includes.Add(P => P.ProductType);
        }
    }
}
