using BisinessLogicLayer.Domain; 

namespace BisinessLogicLayer.Interfaces
{
    public interface IAuthenticationService
    {
        UserAccount Authenticate(string username, string password);
    }
}
