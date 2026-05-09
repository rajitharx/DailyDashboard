using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DailyBoard.Domain;
using DailyBoard.Application.Repositories;

namespace DailyBoard.Infrastructure.Repositories
{
    public class InMemoryBoardRepository : IBoardRepository
    {
        private readonly ConcurrentDictionary<Guid, Board> _boards = new();

        public Task<Board?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            _boards.TryGetValue(id, out var board);
            return Task.FromResult(board);
        }

        public Task<IReadOnlyCollection<Board>> GetAllByOwnerAsync(Guid ownerId, CancellationToken cancellationToken)
        {
            var boards = _boards.Values.Where(b => b.OwnerId == ownerId).ToList();
            return Task.FromResult((IReadOnlyCollection<Board>)boards);
        }

        public Task AddAsync(Board board, CancellationToken cancellationToken)
        {
            _boards[board.Id] = board;
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Board board, CancellationToken cancellationToken)
        {
            _boards[board.Id] = board;
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            _boards.TryRemove(id, out _);
            return Task.CompletedTask;
        }
    }
}
