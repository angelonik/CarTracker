using IntegrationTests.Extensions;
using FluentAssertions;
using Services.Dtos;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using IntegrationTests.DataSeed;

namespace IntegrationTests
{
    [Collection("server and client collection")]
    public class ManufacturerTests
    {
        private readonly HttpClient _client;

        public ManufacturerTests(ServerAndClientFixture fixture)
        {
            _client = fixture.Client;
        }

        [Fact]
        public async Task Manufacturers_should_return_all_manufacturers()
        {
            var manufacturers = await _client
                .GetAsJsonAsync<IEnumerable<ManufacturerWithCarsDto>>("/api/manufacturers");

            manufacturers.Should().NotBeEmpty();
            manufacturers.Should().BeEquivalentTo(StaticData.Manufacturers, 
                x => x.ExcludingMissingMembers());
        }
    }
}
