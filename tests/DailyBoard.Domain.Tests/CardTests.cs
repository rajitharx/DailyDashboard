using System;
using Xunit;
using DailyBoard.Domain;

namespace DailyBoard.Domain.Tests
{
    public class CardTests
    {
        [Fact]
        public void CreateCard_ShouldSetProperties()
        {
            var id = Guid.NewGuid();
            var boardId = Guid.NewGuid();
            var title = "Test Card";
            var description = "Description";
            var status = CardStatus.Todo;
            var dueDate = DateTime.UtcNow.AddDays(1);
            var position = 1;

            var card = new Card(id, boardId, title, description, status, dueDate, position);

            Assert.Equal(id, card.Id);
            Assert.Equal(boardId, card.BoardId);
            Assert.Equal(title, card.Title);
            Assert.Equal(description, card.Description);
            Assert.Equal(status, card.Status);
            Assert.Equal(dueDate, card.DueDate);
            Assert.Equal(position, card.Position);
        }

        [Fact]
        public void Update_ShouldChangeTitleAndDescriptionAndDueDate()
        {
            var card = new Card(Guid.NewGuid(), Guid.NewGuid(), "Title", "Desc", CardStatus.Todo, DateTime.UtcNow, 1);
            var newTitle = "New Title";
            var newDesc = "New Desc";
            var newDue = DateTime.UtcNow.AddDays(2);
            card.Update(newTitle, newDesc, newDue);
            Assert.Equal(newTitle, card.Title);
            Assert.Equal(newDesc, card.Description);
            Assert.Equal(newDue, card.DueDate);
        }

        [Fact]
        public void ChangeStatus_ShouldUpdateStatus()
        {
            var card = new Card(Guid.NewGuid(), Guid.NewGuid(), "Title", "Desc", CardStatus.Todo, DateTime.UtcNow, 1);
            card.ChangeStatus(CardStatus.InProgress);
            Assert.Equal(CardStatus.InProgress, card.Status);
        }

        [Fact]
        public void Move_ShouldUpdatePosition()
        {
            var card = new Card(Guid.NewGuid(), Guid.NewGuid(), "Title", "Desc", CardStatus.Todo, DateTime.UtcNow, 1);
            card.Move(5);
            Assert.Equal(5, card.Position);
        }
    }
}
