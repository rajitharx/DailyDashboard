using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DailyBoard.Domain;
using DailyBoard.Application.Repositories;
using Microsoft.Extensions.Logging;

namespace DailyBoard.Infrastructure.Repositories
{
    public class InMemoryBoardRepository : IBoardRepository
    {
        private readonly ConcurrentDictionary<Guid, Board> _boards = new();
        private readonly ILogger<InMemoryBoardRepository> _logger;

        public InMemoryBoardRepository(ILogger<InMemoryBoardRepository> logger)
        {
            _logger = logger;
        }

        public Task<Board?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            _boards.TryGetValue(id, out var board);
            _logger.LogDebug("GetByIdAsync: {BoardId} -> {Found}", id, board is not null);
            return Task.FromResult(board);
        }

        public Task<IReadOnlyCollection<Board>> GetAllByOwnerAsync(Guid ownerId, CancellationToken cancellationToken)
        {
            var boards = _boards.Values.Where(b => b.OwnerId == ownerId).ToList();
            _logger.LogDebug("GetAllByOwnerAsync: owner={OwnerId} returned {Count} boards", ownerId, boards.Count);
            return Task.FromResult((IReadOnlyCollection<Board>)boards);
        }

        public Task AddAsync(Board board, CancellationToken cancellationToken)
        {
            _boards[board.Id] = board;
            _logger.LogInformation("Board added {BoardId}", board.Id);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Board board, CancellationToken cancellationToken)
        {
            _boards[board.Id] = board;
            _logger.LogInformation("Board updated {BoardId}", board.Id);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            _boards.TryRemove(id, out _);
            _logger.LogInformation("Board deleted {BoardId}", id);
            return Task.CompletedTask;
        }
    }
}
