using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UESAN.Ecommerce.CORE.Core.DTOs;
using UESAN.Ecommerce.CORE.Core.Entities;
using UESAN.Ecommerce.CORE.Core.Interfaces;
namespace UESAN.Ecommerce.CORE.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<CategoryListDTO>> GetAllCategories()
        {
            var categories = await _categoryRepository.GetAllCategories();
            var categoryDTOS = new List<CategoryListDTO>();
            foreach (var category in categories)
            {
                var categoryDTO = new CategoryListDTO();
                categoryDTO.id = category.Id;
                categoryDTO.Description = category.Description;
                categoryDTOS.Add(categoryDTO);

            }
            return categoryDTOS;
        }
        // I want get by id
        public async Task<CategoryListDTO?> GetCategoryById(int id)
        {
            var category = await _categoryRepository.GetCategoryById(id);
            if (category == null)
            {
                return null;

            }
            var categoryDTO = new CategoryListDTO();
            categoryDTO.id = category.Id;
            categoryDTO.Description = category.Description;
            return categoryDTO;
        }
        public async Task<int> InsertCategory(CategoryCreateDTO categoryCreateDTO)
        {
            var category = new Category();
            category.Description = categoryCreateDTO.Description;
            category.IsActive = true;
            var newCategoryId = await _categoryRepository.InsertCategory(category);
            return newCategoryId;
        }
        public async Task UpdateCategory(CategoryListDTO categoryListDTO)
        {
            var category = new Category();
            category.Id = categoryListDTO.id;
            category.Description = categoryListDTO.Description;
            await _categoryRepository.GetCategoryById(category.Id);
        }
        public async Task DeleteCategory(int id)
        {
            await _categoryRepository.DeleteCategory(id);
        }
    }
}
