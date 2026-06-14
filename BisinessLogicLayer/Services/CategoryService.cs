using System.Collections.Generic;
using System.Linq;
using BisinessLogicLayer.Domain;
using BisinessLogicLayer.DTOs;
using BisinessLogicLayer.Interfaces;

namespace BisinessLogicLayer.Services
{
    public class CategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public List<CategoryDTO> GetAllCategory()
        {
            var categories = _categoryRepository.GetAllCategory();

            return categories.Select(c => new CategoryDTO
            {
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName,
                Books = _categoryRepository.GetAllBooksByCategory(c.CategoryId)
            }).ToList();
        }

        public bool AddCategory(CategoryDTO dto)
        {
            
            var category = new Category(dto.CategoryName);

            return _categoryRepository.AddCategory(category);
        }

        public bool UpdateCategory(int categoryId, CategoryDTO dto)
        {
            var category = _categoryRepository.GetCategoryById(categoryId);

            if (category == null)
                return false;

            
            category.Rename(dto.CategoryName);

            return _categoryRepository.UpdateCategory(category);
        }
    }
}