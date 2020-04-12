using System;
using System.IO;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

namespace Todo.IntegrationTests
{
    public class TestFixture : IDisposable
    {
        private readonly TestServer _testServer;

        public HttpClient Client { get; set; }

        public TestFixture()
        {
            var builder = new WebHostBuilder()
                    .UseStartup<Todo.Startup>()
                    .ConfigureAppConfiguration((context, config) => 
                        {
                            config.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(),
                                "..\\..\\..\\..\\Todo"));

                                config.AddJsonFile("appsettings.json");
                        }
                    );

            _testServer = new TestServer(builder);


            Client = _testServer.CreateClient();
            Client.BaseAddress = new Uri("http://localhost:8888");
        }

        public void Dispose()
        {
            Client.Dispose();
            _testServer.Dispose();   
        }        
    }
}