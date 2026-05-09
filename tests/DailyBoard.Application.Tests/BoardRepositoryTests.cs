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
    public class BoardRepositoryTests
    {
        [Fact]
        public async Task AddAsync_ShouldAddBoard()
        {
            var mockRepo = new Mock<IBoardRepository>();
            var board = Board.Create("Test Board", Guid.NewGuid());
            await mockRepo.Object.AddAsync(board, CancellationToken.None);
            mockRepo.Verify(r => r.AddAsync(board, CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnBoard()
        {
            var mockRepo = new Mock<IBoardRepository>();
            var boardId = Guid.NewGuid();
            var board = Board.Create("Test Board", Guid.NewGuid());
            mockRepo.Setup(r => r.GetByIdAsync(boardId, CancellationToken.None)).ReturnsAsync(board);
            var result = await mockRepo.Object.GetByIdAsync(boardId, CancellationToken.None);
            Assert.Equal(board, result);
        }

        [Fact]
        public async Task GetAllByOwnerAsync_ShouldReturnBoards()
        {
            var mockRepo = new Mock<IBoardRepository>();
            var ownerId = Guid.NewGuid();
            var boards = new List<Board> { Board.Create("Board1", ownerId) };
            mockRepo.Setup(r => r.GetAllByOwnerAsync(ownerId, CancellationToken.None)).ReturnsAsync(boards);
            var result = await mockRepo.Object.GetAllByOwnerAsync(ownerId, CancellationToken.None);
            Assert.Single(result);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateBoard()
        {
            var mockRepo = new Mock<IBoardRepository>();
            var board = Board.Create("Test Board", Guid.NewGuid());
            await mockRepo.Object.UpdateAsync(board, CancellationToken.None);
            mockRepo.Verify(r => r.UpdateAsync(board, CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldDeleteBoard()
        {
            var mockRepo = new Mock<IBoardRepository>();
            var boardId = Guid.NewGuid();
            await mockRepo.Object.DeleteAsync(boardId, CancellationToken.None);
            mockRepo.Verify(r => r.DeleteAsync(boardId, CancellationToken.None), Times.Once);
        }
    }
}
