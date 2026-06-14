using System;
using BisinessLogicLayer.Domain;

namespace BisinessLogicLayer.Interfaces
{
    public interface IUserRepository
    {
        User GetByUsername(string username);
        bool SignUp(User user);
        bool UsernameExists(string username);
        int GetUserIdByUsername(string username);
        string GetRole(string username, string password);
        UserAccount GetUserByUsername(string username);
    }
}
