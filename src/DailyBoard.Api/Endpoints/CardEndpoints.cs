using Microsoft.AspNetCore.Mvc;
using DailyBoard.Application.Repositories;
using DailyBoard.Domain;
using DailyBoard.Api.DTOs;

namespace DailyBoard.Api.Endpoints;

public static class CardEndpoints
{
    public static void MapCardEndpoints(this WebApplication app)
    {
        app.MapGet("/boards/{boardId:guid}/cards", async (Guid boardId, [FromServices] ICardRepository repo, CancellationToken ct) =>
        {
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

        app.MapPost("/boards/{boardId:guid}/cards", async (Guid boardId, CardDto dto, [FromServices] ICardRepository repo, CancellationToken ct) =>
        {
            if (dto.BoardId != boardId) return Results.BadRequest();
            var card = new Card(dto.Id, dto.BoardId, dto.Title, dto.Description, (CardStatus)dto.Status, dto.DueDate, dto.Position);
            await repo.AddAsync(card, ct);
            return Results.Created($"/cards/{card.Id}", card.Id);
        });

        app.MapGet("/cards/{id:guid}", async (Guid id, [FromServices] ICardRepository repo, CancellationToken ct) =>
        {
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

        app.MapPut("/cards/{id:guid}", async (Guid id, CardDto dto, [FromServices] ICardRepository repo, CancellationToken ct) =>
        {
            if (id != dto.Id) return Results.BadRequest();
            var card = new Card(dto.Id, dto.BoardId, dto.Title, dto.Description, (CardStatus)dto.Status, dto.DueDate, dto.Position);
            await repo.UpdateAsync(card, ct);
            return Results.NoContent();
        });

        app.MapDelete("/cards/{id:guid}", async (Guid id, [FromServices] ICardRepository repo, CancellationToken ct) =>
        {
            await repo.DeleteAsync(id, ct);
            return Results.NoContent();
        });
    }
}
