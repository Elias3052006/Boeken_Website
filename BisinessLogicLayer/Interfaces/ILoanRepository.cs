using BisinessLogicLayer.Domain;

namespace BisinessLogicLayer.Interfaces
{
    
    public interface ILoanRepository
    {
        bool ProcessLoan(Loan loan, Book book);
        IEnumerable<Loan> GetLoansByUserId(int userId);
    }
}