using System.Collections.Generic;

namespace WarGame.Core
{
    // Player hand - FIFO queue of cards
    public class Hand
    {
        private readonly Queue<Card> _cards = new Queue<Card>();

        public int Count => _cards.Count;

        public void AddCard(Card card)
        {
            _cards.Enqueue(card);
        }

        public Card PlayCard()
        {
            return _cards.Dequeue();
        }

        public bool HasCards()
        {
            return _cards.Count > 0;
        }
    }
}
