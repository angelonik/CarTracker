using AirFiTest.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Services;
using Services.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Controllers
{
    public class UsersControllerTests
    {
        private readonly Mock<IUserService> _mockUserService;
        private readonly UsersController _controller;

        public UsersControllerTests()
        {
            _mockUserService = new Mock<IUserService>();
            _controller = new UsersController(_mockUserService.Object);
        }

        [Fact]
        public async Task Get_should_return_all_users()
        {
            // Arrange
            var users = new[]
            {
                new UserDto{ Id = 1 ,Name = "Andrew Black", Email = "andrew.black@gmail.com" },
                new UserDto{ Id = 1 ,Name = "Jennifer Fetler", Email = "jennifer.fetler@gmail.com" }
            };
            _mockUserService.Setup(x => x.GetAll())
                .Returns(Task.FromResult(users.AsEnumerable()));

            // Act
            var result = await _controller.Get();

            // Assert
            result.Should().BeEquivalentTo(users);
        }

        [Fact]
        public async Task Get_should_return_empty_list_when_no_users_available()
        {
            // Arrange
            _mockUserService.Setup(x => x.GetAll())
                .Returns(Task.FromResult(Enumerable.Empty<UserDto>()));

            // Act
            var result = await _controller.Get();

            // Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task Get_with_id_should_return_the_specific_user()
        {
            // Arrange
            var userId = 1;
            var user = new UserWithCarsDto
            {
                Id = userId,
                Name = "Andrew Black",
                Email = "andrew.black@gmail.com"
            };

            _mockUserService.Setup(x => x.GetUserWithCars(userId))
                .Returns(Task.FromResult(user));

            // Act
            var result = await _controller.Get(userId);

            // Assert
            result.Value.Should().BeEquivalentTo(user);
        }

        [Fact]
        public async Task Get_with_id_should_return_not_found_when_no_user_exists_with_that_id()
        {
            // Arrange
            var userId = 1;

            _mockUserService.Setup(x => x.GetUserWithCars(userId))
                .Returns(Task.FromResult<UserWithCarsDto>(null));

            // Act
            var result = await _controller.Get(userId);

            // Assert
            var okResult = result.Result.Should().BeOfType<NotFoundResult>();
        }

    }
}
