using DataAccess;
using DomainModel;
using FluentAssertions;
using Services;
using Services.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Services
{
    [Collection("Mapping collection")]
    public class ManufacturerServiceTests : TestsBase
    {
        [Fact]
        public async Task GetAllWithCars_should_return_all_available_manufacturers_with_their_associated_cars()
        {
            // arrange
            var manufacturers = new[]
            {
                new Manufacturer
                {
                    Name = "bmw", Country = "germany", Cars = new []
                    {
                        new Car{ Model = "m3", Year = 2016 }
                    }
                },
                new Manufacturer
                {
                    Name = "fiat", Country = "italy", Cars = new []
                    {
                        new Car{ Model = "punto", Year = 2014 }
                    }
                }
            };

            SetupMockDbContext(manufacturers);

            IEnumerable<ManufacturerWithCarsDto> returnedManufacturers;
            using (var context = new ApplicationContext(_dbContextOptions))
            {
                var service = new ManufacturerService(context);

                // act
                returnedManufacturers = await service.GetAllWithCars();
            }

            // assert
            returnedManufacturers.Should().HaveSameCount(manufacturers);

            returnedManufacturers
                .Should()
                .BeEquivalentTo(manufacturers,
                    options => options.ExcludingMissingMembers());

            returnedManufacturers
                .SelectMany(x => x.Cars)
                .Should()
                .BeEquivalentTo(manufacturers
                    .SelectMany(x => x.Cars),
                    options => options.ExcludingMissingMembers());
        }

        [Fact]
        public async Task GetAllWithCars_should_return_all_available_manufacturers_with_empty_list_of_cars_when_no_cars_associated()
        {
            // arrange
            var manufacturers = new[] { new Manufacturer { Name = "subaru" } };

            SetupMockDbContext(manufacturers);

            IEnumerable<ManufacturerWithCarsDto> returnedManufacturers;
            using (var context = new ApplicationContext(_dbContextOptions))
            {
                var service = new ManufacturerService(context);

                // act
                returnedManufacturers = await service.GetAllWithCars();
            }

            // assert
            returnedManufacturers.Should().HaveSameCount(manufacturers);

            returnedManufacturers
                .Should()
                .BeEquivalentTo(manufacturers,
                    options => options.ExcludingMissingMembers());

            returnedManufacturers.SelectMany(x => x.Cars).Should().BeEmpty();
        }

        [Fact]
        public async Task GetAllWithCars_should_return_empty_list_when_no_manufacturers_in_the_database()
        {
            // arrange
            IEnumerable<ManufacturerWithCarsDto> returnedManufacturers;
            using (var context = new ApplicationContext(_dbContextOptions))
            {
                var service = new ManufacturerService(context);

                // act
                returnedManufacturers = await service.GetAllWithCars();
            }

            // assert
            returnedManufacturers.Should().BeEmpty();
        }
    }
}
