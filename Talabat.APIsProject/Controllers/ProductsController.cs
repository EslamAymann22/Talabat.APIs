using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Serialization;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Repository;
using Talabat.Core;
using Talabat.Core.Specifications;
using System.Linq.Expressions;
using AutoMapper;
using Talabat.APIsProject.DTOs;
using Talabat.APIsProject.Errors;

namespace Talabat.APIsProject.Controllers
{

    public class ProductsController : APIBaseController
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> ProductRepo , IMapper mapper)
        {
            _productRepo = ProductRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var Spec = new ProductWithBrandAndTypeSpecifications();
            var Products = await _productRepo.GetAllAsyncGeneric(Spec);
            var MappedProduct = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductToReturnDto>>(Products);
            //var Products = await _productRepo.GetAllAsyncGeneric(new BaseSpecification);

            //return Ok(Products);
            return Ok(MappedProduct);

        }

        [HttpGet("{id}")]
        public async Task <ActionResult<Product>> GetProductById(int id)
        {
            var Spec = new ProductWithBrandAndTypeSpecifications(id);
            var product = await _productRepo.GetByIdAsyncGeneric(Spec);
            //if (product is null) return NotFound(new ApiResponses(404));
            //if (product is null) return BadRequest("not Found this id");
            if (product is null) return NotFound("not Found this id");
            var MappedProduct = _mapper.Map<Product, ProductToReturnDto>(product);
            //return Ok(product);
            return Ok(MappedProduct);
        }

    }
}
