using System;

namespace BisinessLogicLayer.Domain
{
    public class UserAccount
    {
        public int Id { get; private set; }
        public string Username { get; private set; }
        public string Role { get; private set; }

        public UserAccount(int id, string username, string role)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Een geldig ID is vereist.");
            }

            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentException("Een gebruikersnaam is vereist.");
            }


            bool isEmployee = role is "employee";
            bool isUser = role is "user";

            if (isEmployee is false)
            {
                if (isUser is false)
                {
                    throw new ArgumentException("Ongeldige rol gedefinieerd.");
                }
            }

            Id = id;
            Username = username;
            Role = role;
        }

        public bool IsEmployee() => Role is "employee";
        public bool IsUser() => Role is "user";
    }
}