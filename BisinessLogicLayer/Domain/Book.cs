using System;

namespace BisinessLogicLayer.Domain
{
    public class Book
    {
        
        public int BookId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public bool Is_Available { get; private set; }
        public string Author { get; private set; }
        public int CategoryId { get; private set; }
        public int Stock { get; private set; }

        
        public Book(string title, string description, decimal price, string author, int categoryId, int stock)
        {
            Validate(title, price, author, stock);

            Title = title;
            Description = description;
            Price = price;
            Author = author;
            CategoryId = categoryId;
            Stock = stock;
            Is_Available = (Stock > 0);
        }

        
        private void Validate(string title, decimal price, string author, int stock)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Titel is verplicht.");
            }
            if (price < 0)
            {
                throw new ArgumentException("Prijs kan niet negatief zijn.");
            }
            if (string.IsNullOrWhiteSpace(author))
            {
                throw new ArgumentException("Auteur is verplicht.");
            }
            if (stock < 0)
            {
                throw new ArgumentException("Voorraad kan niet negatief zijn.");
            }
        }



        public void UpdateDetails(string title, string description, decimal price, string author, int categoryId, int stock)
        {
            Validate(title, price, author, stock);

            Title = title;
            Description = description;
            Price = price;
            Author = author;
            CategoryId = categoryId;
            Stock = stock;
            Is_Available = (Stock > 0);
        }

        
        public bool IsInStock(int quantity) => Stock >= quantity;

        public void ReduceStock(int quantity)
        {
            if (quantity <= 0)
            {
                throw new ArgumentException("Hoeveelheid moet positief zijn.");
            }
            if (IsInStock(quantity) == false)
            {
                throw new InvalidOperationException("Onvoldoende voorraad voor deze bestelling.");
            }

                Stock -= quantity;
            Is_Available = Stock > 0;
        }

        
        public void SetBookId(int bookId)
        {
            if (bookId <= 0)
            {
                throw new ArgumentException("Ongeldig ID.");
            }
                BookId = bookId;
        }
    }
}