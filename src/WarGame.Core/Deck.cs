using System;
using System.Collections.Generic;
using System.Linq;

namespace WarGame.Core
{
    public class Deck
    {
        private readonly Stack<Card> _cards;

        public int Count => _cards.Count;

        // Initialize deck with standard 52 cards (4 suits × 13 ranks) and shuffle
        public Deck()
        {
            var cards = new List<Card>();

            // Generate all combinations
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (Rank rank in Enum.GetValues(typeof(Rank)))
                {
                    cards.Add(new Card(suit, rank));
                }
            }

            // Shuffle using OrderBy(Random)
            var random = new Random();
            var shuffled = cards.OrderBy(_ => random.Next()).ToList();
            _cards = new Stack<Card>(shuffled);
        }

        // Deal one card from the top
        public Card Deal()
        {
            if (_cards.Count == 0)
                throw new InvalidOperationException("No more cards in the deck.");
            return _cards.Pop();
        }
    }
}
