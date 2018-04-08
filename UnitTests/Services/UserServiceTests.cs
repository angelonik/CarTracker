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
    public class UserServiceTests : TestsBase
    {
        [Fact]
        public async Task GetAll_should_return_all_available_users()
        {
            // arrange
            var users = new[] 
            {
                new User { Name = "john black", Email = "john.black@gmail.com" },
                new User { Name = "Helen Gordon", Email = "helen.gordon@live.com" },
            };

            SetupMockDbContext(users);

            IEnumerable<UserDto> returnedUsers;
            using (var context = new ApplicationContext(_dbContextOptions))
            {
                var service = new UserService(context);

                // act
                returnedUsers = await service.GetAll();
            }

            // assert
            returnedUsers.Should().HaveSameCount(users);

            returnedUsers
                .Should()
                .BeEquivalentTo(users,
                    options => options.ExcludingMissingMembers());
        }

        [Fact]
        public async Task GetAll_should_return_empty_list_when_no_available_users()
        {
            // arrange
            IEnumerable<UserDto> returnedUsers;
            using (var context = new ApplicationContext(_dbContextOptions))
            {
                var service = new UserService(context);

                // act
                returnedUsers = await service.GetAll();
            }

            // assert
            returnedUsers.Should().BeEmpty();
        }

        [Fact]
        public async Task GetUserWithCars_should_return_all_available_users_with_their_associated_cars_and_manufactures()
        {
            // arrange
            var user = new User
            {
                Name = "john black", Email = "john.black@gmail.com",
                UserCars = new []
                {
                    new UserCar
                    {
                        PlateNumber = "XY-5638",
                        Car = new Car
                        {
                            Model = "A3", Year = 2016,
                            Manufacturer = new Manufacturer { Name = "Audi", Country = "Germany" }
                        }
                    }
                }
            };

            SetupMockDbContext(new[] { user });

            UserWithCarsDto returnedUser;
            using (var context = new ApplicationContext(_dbContextOptions))
            {
                var service = new UserService(context);

                // act
                returnedUser = await service.GetUserWithCars(user.Id);
            }

            // assert
            returnedUser.Should().NotBeNull();

            returnedUser
                .Should()
                .BeEquivalentTo(user,
                    options => options.ExcludingMissingMembers());

            returnedUser
                .Cars
                .Should()
                .BeEquivalentTo(user.UserCars
                    .Select(x => x.Car),
                        options => options.ExcludingMissingMembers());

            returnedUser
                .Cars
                .Select(x => x.Manufacturer)
                .Should()
                .BeEquivalentTo(user.UserCars
                    .Select(x => x.Car.Manufacturer),
                        options => options.ExcludingMissingMembers());
        }

        [Fact]
        public async Task GetUserWithCars_should_return_all_available_users_with_no_cars_if_users_dont_have_associated_cars()
        {
            // arrange
            var user = new User
            {
                Name = "john black",
                Email = "john.black@gmail.com",
            };

            SetupMockDbContext(new[] { user });

            UserWithCarsDto returnedUser;
            using (var context = new ApplicationContext(_dbContextOptions))
            {
                var service = new UserService(context);

                // act
                returnedUser = await service.GetUserWithCars(user.Id);
            }

            // assert
            returnedUser.Should().NotBeNull();

            returnedUser
                .Should()
                .BeEquivalentTo(user,
                    options => options.ExcludingMissingMembers());

            returnedUser
                .Cars
                .Should()
                .BeEmpty();
        }

        [Fact]
        public async Task GetUserWithCars_should_return_null_when_no_users_in_the_database()
        {
            // arrange
            UserWithCarsDto returnedUser;
            using (var context = new ApplicationContext(_dbContextOptions))
            {
                var service = new UserService(context);

                // act
                returnedUser = await service.GetUserWithCars(1);
            }

            // assert
            returnedUser.Should().BeNull();
        }
    }
}
