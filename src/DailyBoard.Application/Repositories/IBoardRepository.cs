using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DailyBoard.Domain;

namespace DailyBoard.Application.Repositories
{
    public interface IBoardRepository
    {
        Task<Board?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<Board>> GetAllByOwnerAsync(Guid ownerId, CancellationToken cancellationToken);
        Task AddAsync(Board board, CancellationToken cancellationToken);
        Task UpdateAsync(Board board, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
