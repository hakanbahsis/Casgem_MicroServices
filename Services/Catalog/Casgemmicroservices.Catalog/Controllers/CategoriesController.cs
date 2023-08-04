using CasgemMicroservices.Catalog.Dtos.CategoryDtos;
using CasgemMicroservices.Catalog.Services.CategoryServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CasgemMicroservices.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("getCategoryList")]
        public async Task<IActionResult> CategoryList()
        {
            var values=await _categoryService.GetCategoryListAsync();
            return Ok(values);
        }

        [HttpGet("getCategoryById/{id}")]
        public async Task<IActionResult> GetCategoryById(string id)
        {
            var values=await _categoryService.GetCategoryByIdAsync(id);
            return Ok(values);
        }

        [HttpPost("addCategory")]
        public async Task<IActionResult> AddCategory(CreateCategoryDto categoryDto)
        {
            await _categoryService.CreateCategoryAsync(categoryDto);
            return Ok(categoryDto);
        }

        [HttpPut("updateCategory")]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto categoryDto)
        {
            await _categoryService.UpdateCategoryAsync(categoryDto);
            return Ok(categoryDto);
        }

        [HttpDelete("deleteCategory/{id}")]
        public async Task<IActionResult> DeleteCategory(string id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return Ok();
        }

    }
}
