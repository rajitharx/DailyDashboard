using System;
using System.Threading;
using System.Threading.Tasks;
using DailyBoard.Domain;
using DailyBoard.Infrastructure.Repositories;
using Xunit;

namespace DailyBoard.Infrastructure.Tests
{
    public class InMemoryBoardRepositoryTests
    {
        [Fact]
        public async Task AddAndGetByIdAsync_ShouldReturnBoard()
        {
            var repo = new InMemoryBoardRepository();
            var board = Board.Create("Board", Guid.NewGuid());
            await repo.AddAsync(board, CancellationToken.None);
            var result = await repo.GetByIdAsync(board.Id, CancellationToken.None);
            Assert.Equal(board, result);
        }

        [Fact]
        public async Task GetAllByOwnerAsync_ShouldReturnBoards()
        {
            var repo = new InMemoryBoardRepository();
            var ownerId = Guid.NewGuid();
            var board1 = Board.Create("Board1", ownerId);
            var board2 = Board.Create("Board2", ownerId);
            await repo.AddAsync(board1, CancellationToken.None);
            await repo.AddAsync(board2, CancellationToken.None);
            var boards = await repo.GetAllByOwnerAsync(ownerId, CancellationToken.None);
            Assert.Contains(board1, boards);
            Assert.Contains(board2, boards);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateBoard()
        {
            var repo = new InMemoryBoardRepository();
            var board = Board.Create("Board", Guid.NewGuid());
            await repo.AddAsync(board, CancellationToken.None);
            board.Rename("Updated");
            await repo.UpdateAsync(board, CancellationToken.None);
            var result = await repo.GetByIdAsync(board.Id, CancellationToken.None);
            Assert.NotNull(result);
            Assert.Equal("Updated", result.Name);
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemoveBoard()
        {
            var repo = new InMemoryBoardRepository();
            var board = Board.Create("Board", Guid.NewGuid());
            await repo.AddAsync(board, CancellationToken.None);
            await repo.DeleteAsync(board.Id, CancellationToken.None);
            var result = await repo.GetByIdAsync(board.Id, CancellationToken.None);
            Assert.Null(result);
        }
    }
}
