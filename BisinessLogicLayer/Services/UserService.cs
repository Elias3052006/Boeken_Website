using BisinessLogicLayer.Domain;
using BisinessLogicLayer.DTOs;
using BisinessLogicLayer.Interfaces;

namespace BisinessLogicLayer.Services
{
    public class UserService
    {
        private readonly IUserRepository _repo;

        public UserService(IUserRepository repo)
        {
            _repo = repo;
        }

        public bool SignUp(UserSignUpDto d)
        {
            
            if (_repo.UsernameExists(d.Username))
            {
                return false;
            }

           
            var user = new User(d.Username, d.Password, d.Email, d.PostCode, d.HouseNumber);

            
            return _repo.SignUp(user);
        }

        public bool Login(UserLoginDto d)
        {
            
            var user = _repo.GetByUsername(d.Username);

            
            if (user == null)
            {
                return false;
            }

            
            return user.ValidatePassword(d.Password);
        }

        public int GetUserIdByUsername(string username)
        {
            return _repo.GetUserIdByUsername(username);
        }
    }
}