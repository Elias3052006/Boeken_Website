using System.Collections.Generic;
using BisinessLogicLayer.Domain;

namespace BisinessLogicLayer.Interfaces
{
    public interface IBookRepository
    {
        bool AddBook(Book book);
        Book GetBookById(int bookId);
        List<Book> GetAllBooks();
        bool UpdateBook(Book book);
        
    }
}
