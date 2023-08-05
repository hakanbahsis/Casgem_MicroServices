using CasgemMicroservices.Catalog.Dtos.ProductDtos;
using CasgemMicroservices.Catalog.Services.ProductServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CasgemMicroservices.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("getProductList")]
        public async Task<IActionResult> ProductList()
        {
            var values = await _productService.GetProductListsAsync();
            return Ok(values);
        }

        [HttpGet("getProductById/{id}")]
        public async Task<IActionResult> GetProductById(string id)
        {
            var values=await _productService.GetProductByIdAsync(id);
            return Ok(values);
        }

        [HttpPost("addCategory")]
        public async Task<IActionResult> AddProduct(CreateProductDto product)
        {
            await _productService.CreateProductAsync(product);
            return Ok(product);
        }

        [HttpPut("updateProduct")]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto productDto)
        {
            await _productService.UpdateProductAsync(productDto);
            return Ok(productDto);
        }

        [HttpDelete("deleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            await _productService.DeleteProductAsync(id);
            return Ok();
        }

    }
}
