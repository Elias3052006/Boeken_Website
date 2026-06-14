using BisinessLogicLayer.Domain;
using BisinessLogicLayer.DTOs;
using BisinessLogicLayer.Interfaces;
using System;
using System.Collections.Generic;

namespace BisinessLogicLayer.Services
{
    public class LoanService
    {
        private readonly ILoanRepository _loanRepo;
        private readonly IBookRepository _bookRepo;

        public LoanService(ILoanRepository loanRepo, IBookRepository bookRepo)
        {
            _loanRepo = loanRepo;
            _bookRepo = bookRepo;
        }

        public bool CreateLoan(LoanDTO dto)
        {
            try
            {
                var book = _bookRepo.GetBookById(dto.BookId);

                if (book == null) return false;

                
                book.ReduceStock(1);

                var loan = new Loan(dto.UserId, dto.BookId);

               
                return _loanRepo.ProcessLoan(loan, book);
            }
            catch (InvalidOperationException ex)
            {
                
                Console.WriteLine($"Domeinfout: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fout bij het aanmaken van lening: {ex.Message}");
                return false;
            }
        }

        public IEnumerable<Loan> GetLoansByUserId(int userId)
        {
            if (userId <= 0) return new List<Loan>();
            return _loanRepo.GetLoansByUserId(userId);
        }
    }
}