using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using BisinessLogicLayer.Domain;
using BisinessLogicLayer.Interfaces;
using DataLayer.DataBaseContext;

namespace DataLayer.Repository
{
    public class LoanRepository : ILoanRepository
    {
        private readonly DatabaseContext _context;

        public LoanRepository(DatabaseContext context)
        {
            _context = context;
        }

        // 1. Lening verwerken met Transactie (Alles of niets)
        public bool ProcessLoan(Loan loan, Book book)
        {
            using (var connection = _context.GetConnection())
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // INSERT Lening
                        string insertQuery = @"INSERT INTO loan (Loan_date, Return_date, Book_idBook, User_iduser) 
                                               VALUES (@LoanDate, @ReturnDate, @BookId, @UserId);";

                        using (var cmd = new MySqlCommand(insertQuery, connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@LoanDate", loan.LoanDate);
                            cmd.Parameters.AddWithValue("@ReturnDate", loan.ReturnDate);
                            cmd.Parameters.AddWithValue("@BookId", loan.BookId);
                            cmd.Parameters.AddWithValue("@UserId", loan.UserId);
                            cmd.ExecuteNonQuery();
                        }

                        // UPDATE Voorraad
                        string updateQuery = @"UPDATE book SET Stock = @Stock WHERE idBook = @BookId;";
                        using (var cmd = new MySqlCommand(updateQuery, connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@Stock", book.Stock);
                            cmd.Parameters.AddWithValue("@BookId", book.BookId);
                            cmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        return true;
                    }
                    catch
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        // 2. Leningen ophalen per gebruiker
        public IEnumerable<Loan> GetLoansByUserId(int userId)
        {
            var loans = new List<Loan>();
            using (var connection = _context.GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM loan WHERE User_iduser = @UserId;";
                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Let op: dit gebruikt de constructor van Loan. 
                            // Je moet zorgen dat je Loan klasse dit ondersteunt.
                            loans.Add(new Loan(
                                reader.GetInt32("User_iduser"),
                                reader.GetInt32("Book_idBook")
                            ));
                        }
                    }
                }
            }
            return loans;
        }
    }
}