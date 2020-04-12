using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Todo.IntegrationTests
{
    public class TodoRouteShould : IClassFixture<TestFixture>
    {
        private readonly HttpClient _client;

        public TodoRouteShould(TestFixture testFixture)
        {
            _client = testFixture.Client;
        }

        [Fact]
        public async Task ChallangeAnnonymousUser()
        {
            // Arrange
            var request = new HttpRequestMessage(HttpMethod.Get, "/todo");

            // Act
            var response = await _client.SendAsync(request);

            //Asset
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);

            Assert.Equal("http://localhost:8888/Identity/Account/Login?ReturnUrl=%2Ftodo", response.Headers.Location.ToString());
        }
    }
}