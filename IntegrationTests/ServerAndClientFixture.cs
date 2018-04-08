using CarTracker;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;
using Xunit;

namespace IntegrationTests
{
    public class ServerAndClientFixture
    {
        public TestServer Server { get; private set; }
        public HttpClient Client { get; private set; }

        public ServerAndClientFixture()
        {
            Server = new TestServer
            (
                new WebHostBuilder().UseStartup<Startup>()
            );

            Client = Server.CreateClient();
        }
    }

    [CollectionDefinition("server and client collection")]
    public class DatabaseCollection : ICollectionFixture<ServerAndClientFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}
