using AirFiTest.Controllers;
using FluentAssertions;
using Moq;
using Services;
using Services.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Controllers
{
    public class ManufacturersControllerTests
    {
        private readonly Mock<IManufacturerService> _mockManufactureService;
        private readonly ManufacturersController _controller;

        public ManufacturersControllerTests()
        {
            _mockManufactureService = new Mock<IManufacturerService>();
            _controller = new ManufacturersController(_mockManufactureService.Object);
        }

        [Fact]
        public async Task GetAllWithCars_should_return_all_manufacturers_available()
        {
            // Arrange
            var manufacturers = new[]
            {
                new ManufacturerWithCarsDto{ Id = 1, Name = "BMW" },
                new ManufacturerWithCarsDto{ Id = 2, Name = "Ferrari" },
            };
            _mockManufactureService.Setup(x => x.GetAllWithCars())
                .Returns(Task.FromResult(manufacturers.AsEnumerable()));

            // Act
            var result = await _controller.Get();

            // Assert
            result.Should().BeEquivalentTo(manufacturers);
        }

        [Fact]
        public async Task GetAllWithCars_should_return_empty_list_when_no_manufacturers_available()
        {
            // Arrange
            _mockManufactureService.Setup(x => x.GetAllWithCars())
                .Returns(Task.FromResult(Enumerable.Empty<ManufacturerWithCarsDto>()));

            // Act
            var result = await _controller.Get();

            // Assert
            result.Should().BeEmpty();
        }
    }
}
