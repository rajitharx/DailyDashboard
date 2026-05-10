using System;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;
using DailyBoard.Api;
using DailyBoard.Api.DTOs;

namespace DailyBoard.Api.Tests;

public class BoardsAndCardsApiTests
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
    public async Task Board_CRUD_Works()
    {
        using var app = new TestApiFactory();
        var client = app.CreateClient();
        var ownerId = Guid.NewGuid();
        // Create
        var create = new { Name = "Test Board", OwnerId = ownerId };
        var createResp = await client.PostAsJsonAsync("/boards", create);
        Assert.Equal(HttpStatusCode.Created, createResp.StatusCode);
        var boardId = await createResp.Content.ReadFromJsonAsync<Guid>();
        // Get by id
        var getResp = await client.GetAsync($"/boards/{boardId}");
        Assert.Equal(HttpStatusCode.OK, getResp.StatusCode);
        var board = await getResp.Content.ReadFromJsonAsync<BoardDto>();
        Assert.Equal("Test Board", board.Name);
        // Get all by owner
        var allResp = await client.GetAsync($"/boards?ownerId={ownerId}");
        Assert.Equal(HttpStatusCode.OK, allResp.StatusCode);
        var boards = await allResp.Content.ReadFromJsonAsync<BoardDto[]>();
        Assert.Contains(boards, b => b.Id == boardId);
        // Update
        var updated = new BoardDto { Id = boardId, Name = "Updated Board", OwnerId = ownerId, Cards = Array.Empty<CardDto>() };
        var putResp = await client.PutAsJsonAsync($"/boards/{boardId}", updated);
        Assert.Equal(HttpStatusCode.NoContent, putResp.StatusCode);
        var getUpdated = await client.GetFromJsonAsync<BoardDto>($"/boards/{boardId}");
        Assert.Equal("Updated Board", getUpdated.Name);
        // Delete
        var delResp = await client.DeleteAsync($"/boards/{boardId}");
        Assert.Equal(HttpStatusCode.NoContent, delResp.StatusCode);
        var getDeleted = await client.GetAsync($"/boards/{boardId}");
        Assert.Equal(HttpStatusCode.NotFound, getDeleted.StatusCode);
    }

    [Fact]
    public async Task Card_CRUD_Works()
    {
        using var app = new TestApiFactory();
        var client = app.CreateClient();
        var ownerId = Guid.NewGuid();
        // Create board
        var createBoard = new { Name = "CardTest Board", OwnerId = ownerId };
        var boardResp = await client.PostAsJsonAsync("/boards", createBoard);
        var boardId = await boardResp.Content.ReadFromJsonAsync<Guid>();
        // Add card
        var cardId = Guid.NewGuid();
        var card = new CardDto {
            Id = cardId,
            BoardId = boardId,
            Title = "Test Card",
            Description = "Desc",
            Status = 0,
            DueDate = DateTime.UtcNow,
            Position = 1
        };
        var addResp = await client.PostAsJsonAsync($"/boards/{boardId}/cards", card);
        Assert.Equal(HttpStatusCode.Created, addResp.StatusCode);
        // Get all cards for board
        var getCards = await client.GetFromJsonAsync<CardDto[]>($"/boards/{boardId}/cards");
        Assert.Contains(getCards, c => c.Id == cardId);
        // Get card by id
        var getCard = await client.GetFromJsonAsync<CardDto>($"/cards/{cardId}");
        Assert.Equal("Test Card", getCard.Title);
        // Update card
        card.Title = "Updated Card";
        var putResp = await client.PutAsJsonAsync($"/cards/{cardId}", card);
        Assert.Equal(HttpStatusCode.NoContent, putResp.StatusCode);
        var getUpdated = await client.GetFromJsonAsync<CardDto>($"/cards/{cardId}");
        Assert.Equal("Updated Card", getUpdated.Title);
        // Delete card
        var delResp = await client.DeleteAsync($"/cards/{cardId}");
        Assert.Equal(HttpStatusCode.NoContent, delResp.StatusCode);
        var getDeleted = await client.GetAsync($"/cards/{cardId}");
        Assert.Equal(HttpStatusCode.NotFound, getDeleted.StatusCode);
    }
}
