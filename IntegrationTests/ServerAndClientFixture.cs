using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Net.Http;
using Xunit;

namespace IntegrationTests
{
    public class ServerAndClientFixture : IDisposable
    {
        public TestServer Server { get; }
        public HttpClient Client { get; }

        public ServerAndClientFixture()
        {
            Server = new TestServer
            (
                new WebHostBuilder().UseStartup<TestStartup>()
            );

            Client = Server.CreateClient();
            Client.BaseAddress = new Uri("https://localhost");
        }

        public void Dispose()
        {
            Server?.Dispose();
            Client?.Dispose();
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
