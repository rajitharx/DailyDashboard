using System;
using System.Threading;
using System.Threading.Tasks;
using DailyBoard.Application.Repositories;
using DailyBoard.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DailyBoard.Application.Commands
{
    public class CreateBoardCommandHandler : IRequestHandler<CreateBoardCommand, Guid>
    {
        private readonly IBoardRepository _repo;
        private readonly ILogger<CreateBoardCommandHandler> _logger;
        public CreateBoardCommandHandler(IBoardRepository repo, ILogger<CreateBoardCommandHandler> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<Guid> Handle(CreateBoardCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling CreateBoardCommand for {Name} owner {OwnerId}", request.Name, request.OwnerId);
            var board = Board.Create(request.Name, request.OwnerId);
            await _repo.AddAsync(board, cancellationToken);
            _logger.LogInformation("Board created {BoardId}", board.Id);
            return board.Id;
        }
    }
}
