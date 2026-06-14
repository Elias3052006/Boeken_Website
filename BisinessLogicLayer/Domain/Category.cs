using System;

namespace BisinessLogicLayer.Domain
{
    public class Category
    {
        public int CategoryId { get; private set; }
        public string CategoryName { get; private set; }

        public Category(string categoryName)
        {
            ValidateName(categoryName);
            CategoryName = categoryName;
        }

        public void Rename(string newName)
        {
            ValidateName(newName);
            CategoryName = newName;
        }

        public void SetCategoryId(int categoryId)
        {
            if (categoryId <= 0)
                throw new ArgumentException("Ongeldig Category ID.");

            if (CategoryId != 0)
                throw new InvalidOperationException("Het ID van een bestaande categorie is al toegewezen.");

            CategoryId = categoryId;
        }

        private void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("De categorienaam mag niet leeg zijn!");

            if (name.Trim().Length < 3)
                throw new ArgumentException("De categorienaam moet minimaal 3 tekens bevatten!");
        }
    }
}