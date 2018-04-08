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
    public class ManufacturerTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public ManufacturerTests(ServerAndClientFixture fixture)
        {
            _server = fixture.Server;
            _client = fixture.Client;
        }

        [Fact]
        public async Task Manufacturers_should_return_all_manufacturers()
        {
            // arrange

            // act
            var manufacturers = await _client
                .GetAsJsonAsync<IEnumerable<ManufacturerWithCarsDto>>("/api/manufacturers");

            // assert
            manufacturers.Should().NotBeEmpty();
        }
    }
}
