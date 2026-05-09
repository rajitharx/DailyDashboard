using System;
using System.Threading;
using System.Threading.Tasks;
using DailyBoard.Application.Commands;
using DailyBoard.Application.Repositories;
using DailyBoard.Domain;
using Moq;
using Xunit;

namespace DailyBoard.Application.Tests
{
    public class CreateBoardCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldAddBoardAndReturnId()
        {
            var repoMock = new Mock<IBoardRepository>();
            var handler = new CreateBoardCommandHandler(repoMock.Object);
            var command = new CreateBoardCommand("Test Board", Guid.NewGuid());

            var result = await handler.Handle(command, CancellationToken.None);

            repoMock.Verify(r => r.AddAsync(It.IsAny<Board>(), It.IsAny<CancellationToken>()), Times.Once);
            Assert.NotEqual(Guid.Empty, result);
        }
    }
}
