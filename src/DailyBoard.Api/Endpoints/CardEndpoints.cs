using Microsoft.AspNetCore.Mvc;
using DailyBoard.Application.Repositories;
using DailyBoard.Domain;
using DailyBoard.Api.DTOs;

namespace DailyBoard.Api.Endpoints;

public static class CardEndpoints
{
    public static void MapCardEndpoints(this WebApplication app)
    {
        app.MapGet("/boards/{boardId:guid}/cards", async (Guid boardId, [FromServices] ICardRepository repo, ILoggerFactory loggerFactory, CancellationToken ct) =>
        {
            var logger = loggerFactory.CreateLogger("CardEndpoints");
            logger.LogInformation("Listing cards for board {BoardId}", boardId);
            var cards = await repo.GetAllByBoardAsync(boardId, ct);
            return Results.Ok(cards.Select(c => new CardDto
            {
                Id = c.Id,
                BoardId = c.BoardId,
                Title = c.Title,
                Description = c.Description,
                Status = (int)c.Status,
                DueDate = c.DueDate,
                Position = c.Position
            }));
        });

        app.MapPost("/boards/{boardId:guid}/cards", async (Guid boardId, CardDto dto, [FromServices] ICardRepository repo, ILoggerFactory loggerFactory, CancellationToken ct) =>
        {
            var logger = loggerFactory.CreateLogger("CardEndpoints");
            logger.LogInformation("Adding card {CardTitle} to board {BoardId}", dto.Title, boardId);
            if (dto.BoardId != boardId) return Results.BadRequest();
            var card = new Card(dto.Id, dto.BoardId, dto.Title, dto.Description, (CardStatus)dto.Status, dto.DueDate, dto.Position);
            await repo.AddAsync(card, ct);
            logger.LogInformation("Added card {CardId}", card.Id);
            return Results.Created($"/cards/{card.Id}", card.Id);
        });

        app.MapGet("/cards/{id:guid}", async (Guid id, [FromServices] ICardRepository repo, ILoggerFactory loggerFactory, CancellationToken ct) =>
        {
            var logger = loggerFactory.CreateLogger("CardEndpoints");
            logger.LogInformation("Fetching card {CardId}", id);
            var card = await repo.GetByIdAsync(id, ct);
            return card is not null ? Results.Ok(new CardDto
            {
                Id = card.Id,
                BoardId = card.BoardId,
                Title = card.Title,
                Description = card.Description,
                Status = (int)card.Status,
                DueDate = card.DueDate,
                Position = card.Position
            }) : Results.NotFound();
        });

        app.MapPut("/cards/{id:guid}", async (Guid id, CardDto dto, [FromServices] ICardRepository repo, ILoggerFactory loggerFactory, CancellationToken ct) =>
        {
            var logger = loggerFactory.CreateLogger("CardEndpoints");
            logger.LogInformation("Updating card {CardId}", id);
            if (id != dto.Id) return Results.BadRequest();
            var card = new Card(dto.Id, dto.BoardId, dto.Title, dto.Description, (CardStatus)dto.Status, dto.DueDate, dto.Position);
            await repo.UpdateAsync(card, ct);
            return Results.NoContent();
        });

        app.MapDelete("/cards/{id:guid}", async (Guid id, [FromServices] ICardRepository repo, ILoggerFactory loggerFactory, CancellationToken ct) =>
        {
            var logger = loggerFactory.CreateLogger("CardEndpoints");
            logger.LogInformation("Deleting card {CardId}", id);
            await repo.DeleteAsync(id, ct);
            return Results.NoContent();
        });
    }
}
