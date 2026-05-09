using MediatR;
using DailyBoard.Application.Commands;
using DailyBoard.Application.Repositories;
using DailyBoard.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateBoardCommand>());
builder.Services.AddScoped<IBoardRepository, InMemoryBoardRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapPost("/boards", async (CreateBoardCommand command, IMediator mediator) =>
{
    var id = await mediator.Send(command);
    return Results.Created($"/boards/{id}", id);
});

app.Run();

// For integration testing with WebApplicationFactory
public partial class Program { }
