using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UESAN.Ecommerce.CORE.Core.Entities;
using UESAN.Ecommerce.CORE.Core.Interfaces;
using UESAN.Ecommerce.CORE.Infrastructure.Data;

namespace UESAN.Ecommerce.CORE.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly StoreDbContext _context; // creando una variable global de la clase 

        public CategoryRepository(StoreDbContext context) // inyeccion de dependencias
        {
            _context = context; // lo instaciamos  de manera global evitar dependencias para no esta new new a cada rato 
        }
        //public IEnumerable<Category>GetCategories()
        //{
        //    //var context = new StoreDbContext();
        //    //var categories = context.Category.ToList();
        //    //return categories;
        //    return _context.Category.ToList();

        //}
        //mettod 100% asincrono que permite muchas colsultas y me devuelve todas las categorias

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _context.Category.ToListAsync();
        }
        //ahora que me devuelva una categoria por id
        public async Task<Category?> GetCategoryById(int id)
        {
            return await _context
                .Category
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();

            //return await _context
            //    .Category
            //    .FindAsync(id);
        }
        //metodo para obtener todas las categorias
        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return await _context.Category.ToListAsync();
        }
        //metodo para insertar una categoria
        public async Task<int> InsertCategory(Category category)
        {
            await _context.Category.AddAsync(category);
            var rows = await _context.SaveChangesAsync();
            return category.Id;
        }

        //metodo para eliminar una categoria de la raiz
        public async Task DeleteCategory(int id)
        {
            var category = await _context.Category.FindAsync(id);
            if (category == null)
            {
                _context.Category.Remove(category);
                await _context.SaveChangesAsync();
            }
        }

        //metodo para actualizar una categoria
        public async Task DeleteCategoryLogic(int id)
        {
            var category = await _context.Category.FindAsync(id);
            if (category != null)
            {
                category.IsActive = false;
                _context.Category.Update(category);
                await _context.SaveChangesAsync();
            }
        }

        //metodo para actualizar una categoria
        public async Task UpdateCategory(Category category)
        {
            _context.Category.Update(category);
            await _context.SaveChangesAsync();

        }




    }
}
