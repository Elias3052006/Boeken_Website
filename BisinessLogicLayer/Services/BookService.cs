using System;
using System.Collections.Generic;
using BisinessLogicLayer.Domain;
using BisinessLogicLayer.DTOs;
using BisinessLogicLayer.Interfaces;

namespace BisinessLogicLayer.Services
{
    public class BookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        
        public List<Book> GetAllBooks()
        {
            return _bookRepository.GetAllBooks();
        }

        public Book GetBookById(int id)
        {
            var book = _bookRepository.GetBookById(id);
            if (book == null)
                throw new KeyNotFoundException($"Boek met ID {id} niet gevonden.");

            return book;
        }

        
        public bool AddBook(BookDTO dto)
        {
            
            var book = new Book(dto.Title, dto.Description, dto.Price, dto.Author, dto.CategoryId, dto.Stock);
            return _bookRepository.AddBook(book);
        }

        
        public void UpdateBook(int bookId, BookDTO dto)
        {
            
            var book = GetBookById(bookId);

            
            book.UpdateDetails(dto.Title, dto.Description, dto.Price, dto.Author, dto.CategoryId, dto.Stock);

            
            _bookRepository.UpdateBook(book);
        }

        
        public void ProcessStockReduction(int bookId, int quantity)
        {
            var book = GetBookById(bookId);

            
            book.ReduceStock(quantity);

            
            _bookRepository.UpdateBook(book);
        }
    }
}