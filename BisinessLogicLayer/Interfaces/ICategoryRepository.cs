using System.Collections.Generic;
using BisinessLogicLayer.Domain;

namespace BisinessLogicLayer.Interfaces
{
    public interface ICategoryRepository
    {
        List<Category> GetAllCategory();
        List<Book> GetAllBooksByCategory(int categoryId);
        bool AddCategory(Category category);
        bool UpdateCategory(Category category);
        Category GetCategoryById(int categoryId);
    }
}
