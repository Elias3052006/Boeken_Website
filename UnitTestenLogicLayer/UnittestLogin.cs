using Xunit;
using Moq;
using BisinessLogicLayer.Services;
using BisinessLogicLayer.Interfaces;
using BisinessLogicLayer.Domain;

namespace UnittestenLogicLayer
{
    public class AuthenticationServiceTests
    {
        [Fact]
        public void Authenticate_ValidUserCredentials_ReturnsUserAccount()
        {
            
            var mockEmployeeRepo = new Mock<IEmployeeRepository>();
            var mockUserRepo = new Mock<IUserRepository>();

            
            mockUserRepo.Setup(repo => repo.GetRole("Elias", "Fiets46!")).Returns("user");
            mockUserRepo.Setup(repo => repo.GetUserIdByUsername("Elias")).Returns(1);

            var service = new AuthenticationService(mockEmployeeRepo.Object, mockUserRepo.Object);

            
            var result = service.Authenticate("Elias", "Fiets46!");

            
            Assert.NotNull(result);
            Assert.Equal("Elias", result.Username);
            Assert.True(result.IsUser());
        }

        [Fact]
        public void Authenticate_ValidEmployeeCredentials_ReturnsEmployeeAccount()
        {
            
            var mockEmployeeRepo = new Mock<IEmployeeRepository>();
            var mockUserRepo = new Mock<IUserRepository>();

            
            mockEmployeeRepo.Setup(repo => repo.GetRole("Jan", "Wachtwoord123")).Returns("employee");
            mockEmployeeRepo.Setup(repo => repo.GetEmployeeIdByName("Jan")).Returns(99);

            var service = new AuthenticationService(mockEmployeeRepo.Object, mockUserRepo.Object);

            
            var result = service.Authenticate("Jan", "Wachtwoord123");

            
            Assert.NotNull(result);
            Assert.Equal("Jan", result.Username);
            Assert.True(result.IsEmployee());
        }
        [Fact]
        public void Authenticate_InvalidPassword_ReturnsNull()
        {
            
            var mockEmployeeRepo = new Mock<IEmployeeRepository>();
            var mockUserRepo = new Mock<IUserRepository>();

            
            mockUserRepo.Setup(repo => repo.GetRole("Elias", "1234"))
                        .Returns((string)null);

            var service = new AuthenticationService(mockEmployeeRepo.Object, mockUserRepo.Object);

            
            var result = service.Authenticate("Elias", "1234");

            
            Assert.Null(result); 
        }
        [Fact]
        public void Authenticate_ValidPassword_ReturnsUserAccount()
        {
           
            var mockUserRepo = new Mock<IUserRepository>();


            mockUserRepo.Setup(repo => repo.GetRole("Elias", "Geheim123")).Returns("user");
            mockUserRepo.Setup(repo => repo.GetUserIdByUsername("Elias")).Returns(1);

            var service = new AuthenticationService(new Mock<IEmployeeRepository>().Object, mockUserRepo.Object);

            
            var result = service.Authenticate("Elias", "Geheim123");

            
            Assert.NotNull(result);
        }
    }
}