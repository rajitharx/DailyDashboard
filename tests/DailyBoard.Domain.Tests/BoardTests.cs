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
    }
}
