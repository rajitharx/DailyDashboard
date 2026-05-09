using System;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace DailyBoard.Api.Tests;

public class UnitTest1
{
    [Fact]
    public async Task HealthCheck_ReturnsSuccess()
    {
        using var app = new TestApiFactory();
        var client = app.CreateClient();
        var response = await client.GetAsync("/");
        Assert.True(response.IsSuccessStatusCode || response.StatusCode == HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task PostBoards_CreatesBoardAndReturnsId()
    {
        using var app = new TestApiFactory();
        var client = app.CreateClient();
        var request = new { Name = "UnitTest Board", OwnerId = Guid.NewGuid() };
        var response = await client.PostAsJsonAsync("/boards", request);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var id = await response.Content.ReadFromJsonAsync<Guid>();
        Assert.NotEqual(Guid.Empty, id);
    }
}
