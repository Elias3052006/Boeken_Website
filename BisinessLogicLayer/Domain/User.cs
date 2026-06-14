using System;

namespace BisinessLogicLayer.Domain
{
    public class User
    {
        
        public int UserId { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public string Email { get; private set; }
        public string PostCode { get; private set; }
        public int HouseNumber { get; private set; }

        
        public User(string username, string password, string email, string postCode, int houseNumber)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("Gebruikersnaam verplicht.");
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Wachtwoord verplicht.");
            }
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("E-mail verplicht.");
            }
            if (string.IsNullOrWhiteSpace(postCode))
            {
                throw new ArgumentException("Postcode verplicht.");
            }
            if (houseNumber <= 0)
            {
                throw new ArgumentException("Huisnummer kan geen 0 zijn.");
            }
            Username = username;
            Password = password;
            Email = email;
            PostCode = postCode;
            HouseNumber = houseNumber;
        }

        
        public User(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("Gebruikersnaam verplicht.");
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Wachtwoord verplicht.");
            }
                

            Username = username;
            Password = password;
        }

        
        public bool ValidatePassword(string inputPassword)
        {
            return this.Password == inputPassword;
        }

        
        public void SetUserId(int userId)
        {
            if (userId <= 0) throw new ArgumentException("ID moet positief zijn.");
            UserId = userId;
        }
    }
}