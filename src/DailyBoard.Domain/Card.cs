using System;

namespace DailyBoard.Domain
{
    public enum CardStatus
    {
        Todo,
        InProgress,
        Done
    }

    public class Card
    {
        public Guid Id { get; private set; }
        public Guid BoardId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public CardStatus Status { get; private set; }
        public DateTime DueDate { get; private set; }
        public int Position { get; private set; }

        public Card(Guid id, Guid boardId, string title, string description, CardStatus status, DateTime dueDate, int position)
        {
            Id = id;
            BoardId = boardId;
            Title = title;
            Description = description;
            Status = status;
            DueDate = dueDate;
            Position = position;
        }

        public void Update(string title, string description, DateTime dueDate)
        {
            Title = title;
            Description = description;
            DueDate = dueDate;
        }

        public void ChangeStatus(CardStatus status)
        {
            Status = status;
        }

        public void Move(int newPosition)
        {
            Position = newPosition;
        }
    }
}
