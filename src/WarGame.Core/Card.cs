using System;

namespace WarGame.Core
{
    // Suit enumeration for a standard deck
    public enum Suit
    {
        Hearts,
        Diamonds,
        Clubs,
        Spades
    }

    // Card ranks from 2 to Ace
    public enum Rank
    {
        Two = 2,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King,
        Ace
    }

    // Card class - compares only by rank, not suit
    public class Card : IComparable<Card>
    {
        public Suit Suit { get; }
        public Rank Rank { get; }

        public Card(Suit suit, Rank rank)
        {
            Suit = suit;
            Rank = rank;
        }

        // Compare cards by rank only (suits are ignored)
        public int CompareTo(Card other)
        {
            if (other == null)
                return 1;
            return Rank.CompareTo(other.Rank);
        }

        public override string ToString()
        {
            return Rank.ToString();
        }
    }
}
