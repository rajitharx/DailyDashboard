using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using DailyBoard.Domain;
using DailyBoard.Application.Repositories;
using Moq;
using Xunit;

namespace DailyBoard.Application.Tests
{
    public class CardRepositoryTests
    {
        [Fact]
        public async Task AddAsync_ShouldAddCard()
        {
            var mockRepo = new Mock<ICardRepository>();
            var card = new Card(Guid.NewGuid(), Guid.NewGuid(), "Card 1", "Desc", CardStatus.Todo, DateTime.UtcNow, 1);
            await mockRepo.Object.AddAsync(card, CancellationToken.None);
            mockRepo.Verify(r => r.AddAsync(card, CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnCard()
        {
            var mockRepo = new Mock<ICardRepository>();
            var cardId = Guid.NewGuid();
            var boardId = Guid.NewGuid();
            var card = new Card(cardId, boardId, "Card 1", "Desc", CardStatus.Todo, DateTime.UtcNow, 1);
            mockRepo.Setup(r => r.GetByIdAsync(cardId, CancellationToken.None)).ReturnsAsync(card);
            var result = await mockRepo.Object.GetByIdAsync(cardId, CancellationToken.None);
            Assert.Equal(card, result);
        }

        [Fact]
        public async Task GetAllByBoardAsync_ShouldReturnCards()
        {
            var mockRepo = new Mock<ICardRepository>();
            var boardId = Guid.NewGuid();
            var cards = new List<Card> { new Card(Guid.NewGuid(), boardId, "Card 1", "Desc", CardStatus.Todo, DateTime.UtcNow, 1) };
            mockRepo.Setup(r => r.GetAllByBoardAsync(boardId, CancellationToken.None)).ReturnsAsync(cards);
            var result = await mockRepo.Object.GetAllByBoardAsync(boardId, CancellationToken.None);
            Assert.Single(result);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateCard()
        {
            var mockRepo = new Mock<ICardRepository>();
            var card = new Card(Guid.NewGuid(), Guid.NewGuid(), "Card 1", "Desc", CardStatus.Todo, DateTime.UtcNow, 1);
            await mockRepo.Object.UpdateAsync(card, CancellationToken.None);
            mockRepo.Verify(r => r.UpdateAsync(card, CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldDeleteCard()
        {
            var mockRepo = new Mock<ICardRepository>();
            var cardId = Guid.NewGuid();
            await mockRepo.Object.DeleteAsync(cardId, CancellationToken.None);
            mockRepo.Verify(r => r.DeleteAsync(cardId, CancellationToken.None), Times.Once);
        }
    }
}
