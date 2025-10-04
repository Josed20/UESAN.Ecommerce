using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UESAN.Ecommerce.CORE.Core.DTOs;
using UESAN.Ecommerce.CORE.Core.Entities;
using UESAN.Ecommerce.CORE.Core.Interfaces;
using UESAN.Ecommerce.CORE.Infrastructure.Repositories;

namespace UESAN.Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        //private readonly ICategoryRepository _categoryRepository;
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Obtiene todas las categorías disponibles.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategories();
            return Ok(categories);
        }

        /// <summary>
        /// Obtiene una categoría por su ID.
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _categoryService.GetCategoryById(id);
            if (category == null)
                return NotFound($"No se encontró la categoría con ID {id}");

            return Ok(category);
        }

        /// <summary>
        /// Inserta una nueva categoría.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> InsertCategory([FromBody] CategoryCreateDTO categoryCreateDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var id = await _categoryService.InsertCategory(categoryCreateDTO);
            return CreatedAtAction(nameof(GetCategoryById), new { id = id }, categoryCreateDTO);
        }

        /// <summary>
        /// Actualiza una categoría existente.
        /// </summary>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryListDTO categoryListDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingCategory = await _categoryService.GetCategoryById(id);
            if (existingCategory == null)
                return NotFound($"No se encontró la categoría con ID {id}");

            existingCategory.Description = categoryListDTO.Description;
            

            await _categoryService.UpdateCategory(existingCategory);
            return NoContent();
        }

        /// <summary>
        /// Elimina físicamente una categoría por su ID.
        /// </summary>
        [HttpDelete("delete/{id:int}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _categoryService.GetCategoryById(id);
            if (category == null)
                return NotFound($"No se encontró la categoría con ID {id}");

            await _categoryService.DeleteCategory(id);
            return NoContent();
        }

        /// <summary>
        /// Elimina lógicamente una categoría (soft delete).
        /// </summary>
        [HttpDelete("delete-logic/{id:int}")]
        public async Task<IActionResult> DeleteCategoryLogic(int id)
        {
            var category = await _categoryService.GetCategoryById(id);
            if (category == null)
                return NotFound($"No se encontró la categoría con ID {id}");

            await _categoryService.DeleteCategory(id);
            return NoContent();
        }
    }
}
