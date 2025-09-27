using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UESAN.Ecommerce.CORE.Core.Interfaces;

namespace UESAN.Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly  ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryRepository.GetAllCategories();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _categoryRepository.GetCategoryById(id);
            if (category == null)
                return NotFound();
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> InsertCategory(UESAN.Ecommerce.CORE.Core.Entities.Category category)
        {
            var id = await _categoryRepository.InsertCategory(category);
            return CreatedAtAction("GetCategoryById", new { id = id }, category);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _categoryRepository.GetCategoryById(id);
            if (category == null)
                return NotFound();
            await _categoryRepository.DeleteCategory(id);
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory(UESAN.Ecommerce.CORE.Core.Entities.Category category)
        {
            var existingCategory = await _categoryRepository.GetCategoryById(category.Id);
            if (existingCategory == null)
                return NotFound();
            await _categoryRepository.UpdateCategory(category);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCategoryLogic(int id)
        {
            var category = await _categoryRepository.GetCategoryById(id);
            if (category == null)
                return NotFound();
            await _categoryRepository.DeleteCategoryLogic(id);
            return NoContent();
        }
    }
}
