﻿using AutoMapper;
using Talabat.APIsProject.DTOs;
using Talabat.Core.Entities;

namespace Talabat.APIsProject.Helper
{
    public class MappingProfiles : Profile
    {

        public MappingProfiles() {
        
            CreateMap<Product,ProductToReturnDto>()
                .ForMember(D=>D.ProductBrand,O=>O.MapFrom(S=>S.ProductBrand.Name))
                .ForMember(D=>D.ProductType, O=>O.MapFrom(S=>S.ProductType.Name))
                .ForMember(D=>D.PictureUrl,O=>O.MapFrom<ProductUrlResolver>());

        }


    }
}
