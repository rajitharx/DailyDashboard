using MediatR;
using Microsoft.AspNetCore.Mvc;
using DailyBoard.Application.Commands;
using DailyBoard.Application.Repositories;
using DailyBoard.Domain;
using DailyBoard.Api.DTOs;

namespace DailyBoard.Api.Endpoints;

public static class BoardEndpoints
{
    public static void MapBoardEndpoints(this WebApplication app)
    {
        app.MapPost("/boards", async (BoardCreateDto dto, IMediator mediator) =>
        {
            var command = new CreateBoardCommand(dto.Name, dto.OwnerId);
            var id = await mediator.Send(command);
            return Results.Created($"/boards/{id}", id);
        });

        app.MapGet("/boards/{id:guid}", async (Guid id, [FromServices] IBoardRepository repo, [FromServices] ICardRepository cardRepo, CancellationToken ct) =>
        {
            var board = await repo.GetByIdAsync(id, ct);
            if (board is null) return Results.NotFound();
            var cards = await cardRepo.GetAllByBoardAsync(board.Id, ct);
            return Results.Ok(new BoardDto
            {
                Id = board.Id,
                Name = board.Name,
                OwnerId = board.OwnerId,
                Cards = cards.Select(c => new CardDto
                {
                    Id = c.Id,
                    BoardId = c.BoardId,
                    Title = c.Title,
                    Description = c.Description,
                    Status = (int)c.Status,
                    DueDate = c.DueDate,
                    Position = c.Position
                }).ToArray()
            });
        });

        app.MapGet("/boards", async (Guid ownerId, [FromServices] IBoardRepository repo, [FromServices] ICardRepository cardRepo, CancellationToken ct) =>
        {
            var boards = await repo.GetAllByOwnerAsync(ownerId, ct);
            var result = new List<BoardDto>();
            foreach (var board in boards)
            {
                var cards = await cardRepo.GetAllByBoardAsync(board.Id, ct);
                result.Add(new BoardDto
                {
                    Id = board.Id,
                    Name = board.Name,
                    OwnerId = board.OwnerId,
                    Cards = cards.Select(c => new CardDto
                    {
                        Id = c.Id,
                        BoardId = c.BoardId,
                        Title = c.Title,
                        Description = c.Description,
                        Status = (int)c.Status,
                        DueDate = c.DueDate,
                        Position = c.Position
                    }).ToArray()
                });
            }
            return Results.Ok(result);
        });

        app.MapPut("/boards/{id:guid}", async (Guid id, BoardDto dto, [FromServices] IBoardRepository repo, CancellationToken ct) =>
        {
            if (id != dto.Id) return Results.BadRequest();
            var board = Board.Create(dto.Name, dto.OwnerId);
            typeof(Board).GetProperty("Id")?.SetValue(board, dto.Id);
            await repo.UpdateAsync(board, ct);
            return Results.NoContent();
        });

        app.MapDelete("/boards/{id:guid}", async (Guid id, [FromServices] IBoardRepository repo, CancellationToken ct) =>
        {
            await repo.DeleteAsync(id, ct);
            return Results.NoContent();
        });
    }
}
