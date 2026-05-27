using DailyBoard.Application.Commands;
using DailyBoard.Application.Repositories;
using DailyBoard.Infrastructure.Repositories;
using DailyBoard.Api.Endpoints;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateBoardCommand>());
builder.Services.AddSingleton<IBoardRepository, InMemoryBoardRepository>();
builder.Services.AddSingleton<ICardRepository, InMemoryCardRepository>();

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.MapBoardEndpoints();
app.MapCardEndpoints();

app.Run();

// For integration testing with WebApplicationFactory
public partial class Program { }

