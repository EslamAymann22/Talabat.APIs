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
        private readonly IGenericRepository<ProductType> _productTypeRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;

        public ProductsController(IGenericRepository<Product> ProductRepo
                                , IMapper mapper
                                ,IGenericRepository<ProductType>productTypeRepo
                                , IGenericRepository<ProductBrand> productBrandRepo)
        {
            _productRepo = ProductRepo;
            _mapper = mapper;
            _productTypeRepo = productTypeRepo;
            _productBrandRepo = productBrandRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetProducts()
        {
            var Spec = new ProductWithBrandAndTypeSpecifications();
            var Products = await _productRepo.GetAllAsyncGeneric(Spec);
            var MappedProduct = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(Products);
            //var Products = await _productRepo.GetAllAsyncGeneric(new BaseSpecification);

            //return Ok(Products);
            return Ok(MappedProduct);

        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponses),StatusCodes.Status404NotFound)]
        public async Task <ActionResult<ProductToReturnDto>> GetProductById(int id)
        {
            var Spec = new ProductWithBrandAndTypeSpecifications(id);
            var product = await _productRepo.GetByIdAsyncGeneric(Spec);
            if (product is null) return NotFound(new ApiResponses(404));
            //if (product is null) return BadRequest("not Found this id");
            //if (product is null) return NotFound("not Found this id");
            var MappedProduct = _mapper.Map<Product, ProductToReturnDto>(product);
            //return Ok(product);
            return Ok(MappedProduct);
        }

        [HttpGet("Types")]
        public async Task<ActionResult<IEnumerable<ProductType>>> GetAllProductTypes()
        {
            var productsType = await _productTypeRepo.GetAllAsync();
            if (productsType is null) return NotFound(new ApiResponses(404));
            return Ok(productsType);
        }
        [HttpGet("Type/{id}")]
        public async Task< ActionResult<ProductType>> GetProductTypesBtId(int id)
        {
            var productType = await _productTypeRepo.GetByIdAsync(id);
            if (productType is null) return NotFound(new ApiResponses(404));
            return Ok(productType);
        }
        [HttpGet("Brands")]
        public async Task<ActionResult<IEnumerable<ProductBrand>>> GetAllProductBrands()
        {
            var productsBrands = await _productBrandRepo.GetAllAsync();
            if (productsBrands is null) return NotFound(new ApiResponses(404));
            return Ok(productsBrands);
        }
        [HttpGet("Brand/{id}")]
        public async Task<ActionResult<ProductBrand>> GetProductBrandsBtId(int id)
        {
            var productBrand = await _productBrandRepo.GetByIdAsync(id);
            if (productBrand is null) return NotFound(new ApiResponses(404));
            return Ok(productBrand);
        }
        ///[HttpGet("Not")]
        ///public ActionResult getNotFound()
        ///{
        ///    //var Spec 
        ///    //return Ok(_productRepo.GetByIdAsync(1));
        ///    //int y = 0;
        ///    //int x = 5 / y;
        ///    return NotFound(new ApiResponses(404));
        ///    //return NotFound();
        ///}

    }
}
