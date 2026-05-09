using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DailyBoard.Domain;

namespace DailyBoard.Application.Repositories
{
    public interface ICardRepository
    {
        Task<Card?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<Card>> GetAllByBoardAsync(Guid boardId, CancellationToken cancellationToken);
        Task AddAsync(Card card, CancellationToken cancellationToken);
        Task UpdateAsync(Card card, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
