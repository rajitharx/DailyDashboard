using System;

namespace DailyBoard.Domain
{
    public class Board
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public Guid OwnerId { get; private set; }

        private Board(Guid id, string name, Guid ownerId)
        {
            Id = id;
            Name = name;
            OwnerId = ownerId;
        }

        public static Board Create(string name, Guid ownerId)
        {
            return new Board(Guid.NewGuid(), name, ownerId);
        }
    }
}
