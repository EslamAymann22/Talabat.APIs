using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Serialization;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Repository;

namespace Talabat.APIsProject.Controllers
{

    public class ProductsController : APIBaseController
    {
        private readonly IGenericRepository<Product> _productRepo;

        public ProductsController(IGenericRepository<Product> ProductRepo)
        {
            _productRepo = ProductRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var Products = await _productRepo.GetAllAsync();

            return Ok(Products);

        }

        [HttpGet("{id}")]
        public async Task <ActionResult<Product>> GetProductById(int id)
        {
            var product = await _productRepo.GetByIdAsync(id);
            return Ok(product);
        }

    }
}
