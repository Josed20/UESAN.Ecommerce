using UESAN.Ecommerce.CORE.Core.DTOs;

namespace UESAN.Ecommerce.CORE.Core.Interfaces
{
    public interface ICategoryService
    {
        Task DeleteCategory(int id);
        Task<IEnumerable<CategoryListDTO>> GetAllCategories();
        Task<CategoryListDTO?> GetCategoryById(int id);
        Task<int> InsertCategory(CategoryCreateDTO categoryCreateDTO);
        Task UpdateCategory(CategoryListDTO categoryListDTO);
    }
}