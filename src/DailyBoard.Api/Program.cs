using MediatR;
using DailyBoard.Application.Commands;
using DailyBoard.Application.Repositories;
using DailyBoard.Infrastructure.Repositories;
using DailyBoard.Domain;
using Microsoft.AspNetCore.Mvc;
using DailyBoard.Api.DTOs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateBoardCommand>());
builder.Services.AddSingleton<IBoardRepository, InMemoryBoardRepository>();
builder.Services.AddSingleton<ICardRepository, InMemoryCardRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}


// Board endpoints
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
    return Results.Ok(new BoardDto {
        Id = board.Id,
        Name = board.Name,
        OwnerId = board.OwnerId,
        Cards = cards.Select(c => new CardDto {
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
        result.Add(new BoardDto {
            Id = board.Id,
            Name = board.Name,
            OwnerId = board.OwnerId,
            Cards = cards.Select(c => new CardDto {
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

// Card endpoints
app.MapGet("/boards/{boardId:guid}/cards", async (Guid boardId, [FromServices] ICardRepository repo, CancellationToken ct) =>
{
    var cards = await repo.GetAllByBoardAsync(boardId, ct);
    return Results.Ok(cards.Select(c => new CardDto {
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
    return card is not null ? Results.Ok(new CardDto {
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

app.Run();

// For integration testing with WebApplicationFactory
public partial class Program { }

