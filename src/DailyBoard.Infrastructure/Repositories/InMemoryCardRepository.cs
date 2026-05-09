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
    public class InMemoryCardRepository : ICardRepository
    {
        private readonly ConcurrentDictionary<Guid, Card> _cards = new();
        private readonly ConcurrentDictionary<Guid, Guid> _cardToBoard = new();

        public Task<Card?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            _cards.TryGetValue(id, out var card);
            return Task.FromResult(card);
        }

        public Task<IReadOnlyCollection<Card>> GetAllByBoardAsync(Guid boardId, CancellationToken cancellationToken)
        {
            var cards = _cardToBoard.Where(x => x.Value == boardId)
                .Select(x => _cards[x.Key])
                .ToList();
            return Task.FromResult((IReadOnlyCollection<Card>)cards);
        }

        public Task AddAsync(Card card, CancellationToken cancellationToken)
        {
            _cards[card.Id] = card;
            _cardToBoard[card.Id] = card.BoardId;
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Card card, CancellationToken cancellationToken)
        {
            _cards[card.Id] = card;
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            _cards.TryRemove(id, out _);
            _cardToBoard.TryRemove(id, out _);
            return Task.CompletedTask;
        }
    }
}
