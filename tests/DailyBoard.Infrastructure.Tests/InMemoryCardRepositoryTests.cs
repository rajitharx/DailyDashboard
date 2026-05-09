using System;
using System.Threading;
using System.Threading.Tasks;
using DailyBoard.Domain;
using DailyBoard.Infrastructure.Repositories;
using Xunit;

namespace DailyBoard.Infrastructure.Tests
{
    public class InMemoryCardRepositoryTests
    {
        [Fact]
        public async Task AddAndGetByIdAsync_ShouldReturnCard()
        {
            var repo = new InMemoryCardRepository();
            var card = new Card(Guid.NewGuid(), Guid.NewGuid(), "Card 1", "Desc", CardStatus.Todo, DateTime.UtcNow, 1);
            await repo.AddAsync(card, CancellationToken.None);
            var result = await repo.GetByIdAsync(card.Id, CancellationToken.None);
            Assert.Equal(card, result);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateCard()
        {
            var repo = new InMemoryCardRepository();
            var card = new Card(Guid.NewGuid(), Guid.NewGuid(), "Card 1", "Desc", CardStatus.Todo, DateTime.UtcNow, 1);
            await repo.AddAsync(card, CancellationToken.None);
            card.Update("Updated", "Updated Desc", DateTime.UtcNow.AddDays(1));
            await repo.UpdateAsync(card, CancellationToken.None);
            var result = await repo.GetByIdAsync(card.Id, CancellationToken.None);
            Assert.Equal("Updated", result.Title);
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemoveCard()
        {
            var repo = new InMemoryCardRepository();
            var card = new Card(Guid.NewGuid(), Guid.NewGuid(), "Card 1", "Desc", CardStatus.Todo, DateTime.UtcNow, 1);
            await repo.AddAsync(card, CancellationToken.None);
            await repo.DeleteAsync(card.Id, CancellationToken.None);
            var result = await repo.GetByIdAsync(card.Id, CancellationToken.None);
            Assert.Null(result);
        }
    }
}
