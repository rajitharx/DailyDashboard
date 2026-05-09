using System;

namespace DailyBoard.Domain
{
    public class Board
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public Guid OwnerId { get; private set; }

        private readonly List<Card> _cards = new();
        public IReadOnlyCollection<Card> Cards => _cards.AsReadOnly();

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

        public void Rename(string newName)
        {
            Name = newName;
        }

        public void AddCard(Card card)
        {
            if (!_cards.Contains(card))
                _cards.Add(card);
        }

        public void RemoveCard(Guid cardId)
        {
            var card = _cards.FirstOrDefault(c => c.Id == cardId);
            if (card != null)
                _cards.Remove(card);
        }
    }
}
