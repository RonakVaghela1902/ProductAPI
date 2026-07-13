using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.Models.DTOs;
using ProductAPI.Models.Entities;
using ProductAPI.Repositories;

namespace ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ProductController : ControllerBase
    {
        private readonly IProduct _productRepo;
        public ProductController(IProduct productRepo)
        {
            _productRepo = productRepo;
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Product product = await _productRepo.CreateProduct(request);

            return CreatedAtAction(nameof(GetById), new { key = product.Key }, product);
        }
        [HttpGet("{key}")]
        public async Task<IActionResult> GetById(Guid key)
        {
            return Ok();
        }
    }
}
