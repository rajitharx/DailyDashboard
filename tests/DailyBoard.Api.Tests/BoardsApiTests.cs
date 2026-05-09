using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace DailyBoard.Api.Tests
{
    public class BoardsApiTests
    {
        [Fact(Skip = "API endpoint not implemented yet")]
        public async Task PostBoards_ShouldReturnCreatedAndBoardId()
        {
            // Arrange
            using var app = new TestApiFactory();
            var client = app.CreateClient();
            var request = new { Name = "Test Board", OwnerId = Guid.NewGuid() };

            // Act
            var response = await client.PostAsJsonAsync("/boards", request);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            var id = await response.Content.ReadFromJsonAsync<Guid>();
            Assert.NotEqual(Guid.Empty, id);
        }
    }
}
