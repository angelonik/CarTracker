using IntegrationTests.Extensions;
using FluentAssertions;
using Microsoft.AspNetCore.TestHost;
using Services.Dtos;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests
{
    [Collection("server and client collection")]
    public class UserTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public UserTests(ServerAndClientFixture fixture)
        {
            _server = fixture.Server;
            _client = fixture.Client;
        }

        [Fact]
        public async Task Users_should_return_all_users()
        {
            // arrange

            // act
            var users = await _client
                .GetAsJsonAsync<IEnumerable<UserDto>>("/api/users");

            // assert
            users.Should().NotBeEmpty();
        }

        [Fact]
        public async Task User_with_id_should_return_specified_user()
        {
            // arrange

            // act
            var user = await _client.GetAsJsonAsync<UserDto>("/api/user/2");

            // assert
            user.Should().NotBeNull();
            user.Id.Should().Be(2);
        }
    }
}
