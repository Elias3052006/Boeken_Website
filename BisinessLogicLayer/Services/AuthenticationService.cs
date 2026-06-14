using BisinessLogicLayer.Domain;
using BisinessLogicLayer.Interfaces;

namespace BisinessLogicLayer.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IEmployeeRepository _employeeRepo;
        private readonly IUserRepository _userRepo;

        public AuthenticationService(IEmployeeRepository employeeRepo, IUserRepository userRepo)
        {
            _employeeRepo = employeeRepo;
            _userRepo = userRepo;
        }

        public UserAccount Authenticate(string username, string password)
        {
           
            var employeeRole = _employeeRepo.GetRole(username, password);

            bool isEmployee = employeeRole is not null;

            if (isEmployee)
            {
                int id = _employeeRepo.GetEmployeeIdByName(username);
                return new UserAccount(id, username, "employee");
            }

            
            var userRole = _userRepo.GetRole(username, password);

            bool isUser = userRole is not null;

            if (isUser)
            {
                int id = _userRepo.GetUserIdByUsername(username);
                return new UserAccount(id, username, "user");
            }

            
            return null;
        }
    }
}