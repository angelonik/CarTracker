using IntegrationTests.Extensions;
using FluentAssertions;
using Services.Dtos;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using System.Linq;
using IntegrationTests.DataSeed;

namespace IntegrationTests
{
    [Collection("server and client collection")]
    public class UserTests
    {
        private readonly HttpClient _client;

        public UserTests(ServerAndClientFixture fixture)
        {
            _client = fixture.Client;
        }

        [Fact]
        public async Task Users_should_return_all_users()
        {
            var users = await _client
                .GetAsJsonAsync<IEnumerable<UserDto>>("/api/users");

            users.Should().NotBeEmpty();
            users.Should().BeEquivalentTo(StaticData.Users,
                x => x.ExcludingMissingMembers());
        }

        [Fact]
        public async Task User_with_id_should_return_specified_user()
        {
            var expectedUser = StaticData.Users.First();

            var user = await _client.GetAsJsonAsync<UserWithCarsDto>("/api/users/" + expectedUser.Id);

            user.Should().NotBeNull();
            user.Cars.Should().NotBeNullOrEmpty();
            user.Id.Should().Be(expectedUser.Id);
            user.Name.Should().Be(expectedUser.Name);
            user.Email.Should().Be(expectedUser.Email);            
            user.Cars.Should().BeEquivalentTo(expectedUser.UserCars.Select(x => x.Car),
                x => x.ExcludingMissingMembers());
        }
    }
}
