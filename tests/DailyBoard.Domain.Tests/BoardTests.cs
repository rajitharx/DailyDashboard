using Xunit;
using DailyBoard.Domain;

namespace DailyBoard.Domain.Tests
{
    public class BoardTests
    {
        [Fact]
        public void Create_ShouldCreateBoardWithValidNameAndOwner()
        {
            // Arrange
            var name = "My Board";
            var ownerId = Guid.NewGuid();

            // Act
            var board = Board.Create(name, ownerId);

            // Assert
            Assert.NotNull(board);
            Assert.Equal(name, board.Name);
            Assert.Equal(ownerId, board.OwnerId);
            Assert.NotEqual(Guid.Empty, board.Id);
        }

        [Fact]
        public void Rename_ShouldUpdateBoardName()
        {
            var board = Board.Create("Old Name", Guid.NewGuid());
            var newName = "New Name";
            board.Rename(newName);
            Assert.Equal(newName, board.Name);
        }

        [Fact]
        public void AddCard_ShouldAddCardToBoard()
        {
            var board = Board.Create("Board", Guid.NewGuid());
            var card = new Card(
                Guid.NewGuid(),
                board.Id,
                "Card 1",
                "Description",
                CardStatus.Todo,
                DateTime.UtcNow.AddDays(1),
                1
            );
            board.AddCard(card);
            Assert.Contains(card, board.Cards);
        }

        [Fact]
        public void RemoveCard_ShouldRemoveCardFromBoard()
        {
            var board = Board.Create("Board", Guid.NewGuid());
            var card = new Card(
                Guid.NewGuid(),
                board.Id,
                "Card 1",
                "Description",
                CardStatus.Todo,
                DateTime.UtcNow.AddDays(1),
                1
            );
            board.AddCard(card);
            board.RemoveCard(card.Id);
            Assert.DoesNotContain(card, board.Cards);
        }

        [Fact]
        public void Cards_ShouldBeReadOnly()
        {
            var board = Board.Create("Board", Guid.NewGuid());
            Assert.IsAssignableFrom<IReadOnlyCollection<Card>>(board.Cards);
        }
        
    }
}
