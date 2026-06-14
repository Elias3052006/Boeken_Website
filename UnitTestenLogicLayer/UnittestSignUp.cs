using Xunit;
using Moq;
using BisinessLogicLayer.Services;
using BisinessLogicLayer.Interfaces;
using BisinessLogicLayer.Domain;
using BisinessLogicLayer.DTOs;

namespace UnittestenLogicLayer
{
    public class AccountServiceTests
    {
        [Fact]
        public void SignUp_ValidData_ReturnsTrueAndCallsRepository()
        {
            var mockRepo = new Mock<IUserRepository>();
            var service = new UserService(mockRepo.Object);

            var dto = new UserSignUpDto
            {
                Username = "Elias",
                Password = "Fiets46!",
                Email = "elias@test.nl",
                PostCode = "5223",
                HouseNumber = 11
            };

            mockRepo.Setup(r => r.UsernameExists("Elias")).Returns(false);
            mockRepo.Setup(r => r.SignUp(It.IsAny<User>())).Returns(true);

            bool resultaat = service.SignUp(dto);

            Assert.True(resultaat);

            mockRepo.Verify(r => r.SignUp(It.Is<User>(user =>
                user.Username == "Elias"
            )), Times.Once);

            mockRepo.Verify(r => r.SignUp(It.Is<User>(user =>
                user.Email == "elias@test.nl"
            )), Times.Once);

            mockRepo.Verify(r => r.SignUp(It.Is<User>(user =>
                user.HouseNumber == 11
            )), Times.Once);
        }

        [Fact]
        public void SignUp_ExistingUsername_ReturnsFalse()
        {
            var mockRepo = new Mock<IUserRepository>();
            var service = new UserService(mockRepo.Object);

            var dto = new UserSignUpDto
            {
                Username = "Elias",
                Password = "Fiets46!",
                Email = "elias@test.nl",
                PostCode = "5223",
                HouseNumber = 11
            };

            mockRepo.Setup(r => r.UsernameExists("Elias")).Returns(true);

            bool resultaat = service.SignUp(dto);

            Assert.False(resultaat);

            mockRepo.Verify(r => r.SignUp(It.IsAny<User>()), Times.Never);
        }
    }
}