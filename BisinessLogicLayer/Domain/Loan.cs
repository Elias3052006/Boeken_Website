using System;

namespace BisinessLogicLayer.Domain
{
    public class Loan
    {
        
        public int LoanId { get; private set; }
        public int UserId { get; private set; }
        public int BookId { get; private set; }
        public DateTime LoanDate { get; private set; }
        public DateTime ReturnDate { get; private set; }

        
        public Loan(int userId, int bookId)
        {
            if (userId <= 0)
            {
                throw new ArgumentException("Ongeldige gebruiker.");
            }
            if (bookId <= 0)
            {
                throw new ArgumentException("Ongeldig boek.");
            }
            UserId = userId;
            BookId = bookId;
            LoanDate = DateTime.Now;
            ReturnDate = DateTime.Now.AddDays(14); 
        }


        public void ExtendLoan(int extraDays)
        {
            if (extraDays <= 0)
            {
                throw new ArgumentException("Dagen moeten positief zijn.");
            }
            ReturnDate = ReturnDate.AddDays(extraDays);
        }
    }
}