using System;
using System.Collections.Generic;
using System.Linq;

namespace WarGame.Core
{
    // Main game engine for War card game
    public class WarEngine
    {
        // Player hands keyed by player name
        public Dictionary<string, Hand> PlayerHands { get; } = new Dictionary<string, Hand>();

        // Shared pot of cards
        public List<Card> pot { get; } = new List<Card>();

        // Cards played this round
        public Dictionary<string, Card> played { get; } = new Dictionary<string, Card>();

        private const int RoundLimit = 10_000;

        // Start game with specified number of players
        public void StartGame(int playerCount)
        {
            if (playerCount < 2 || playerCount > 4)
            {
                throw new ArgumentOutOfRangeException(nameof(playerCount), "Player count must be between 2 and 4.");
            }

            var deck = new Deck();

            for (int i = 1; i <= playerCount; i++)
            {
                PlayerHands[$"Player {i}"] = new Hand();
            }

            // Deal cards round-robin
            while (deck.Count > 0)
            {
                foreach (var player in PlayerHands.Keys)
                {
                    if (deck.Count == 0)
                    {
                        break;
                    }

                    PlayerHands[player].AddCard(deck.Deal());
                }
            }

            int round = 0;

            while (true)
            {
                // Eliminate players with no cards
                var activePlayers = PlayerHands.Where(kvp => kvp.Value.Count > 0).Select(kvp => kvp.Key).ToList();

                if (activePlayers.Count == 1)
                {
                    var winner = activePlayers[0];
                    Console.WriteLine($"Winner: {winner} (Cards: {GetPlayerCardCountSummary()})");
                    break;
                }

                if (round >= RoundLimit)
                {
                    int maxCount = PlayerHands.Max(kvp => kvp.Value.Count);
                    var topPlayers = PlayerHands.Where(kvp => kvp.Value.Count == maxCount).Select(kvp => kvp.Key).ToList();
                    if (topPlayers.Count == 1)
                    {
                        Console.WriteLine("Round limit reached.");
                        Console.WriteLine($"Winner: {topPlayers[0]} (Cards: {GetPlayerCardCountSummary()})");
                    }
                    else
                    {
                        Console.WriteLine("Round limit reached. Draw.");
                        Console.WriteLine($"Draw among: {string.Join(" and ", topPlayers)} (Cards: {GetPlayerCardCountSummary()})");
                    }

                    break;
                }

                round++;

                Console.WriteLine($"Round {round}");
                PlayRound(activePlayers, pot, false);
            }
        }

        // Play one round of the game
        public void PlayRound(List<string> activePlayers, List<Card> pot, bool isTiebreaker = false)
        {
            // played temporary map for the round
            var currentPlayed = new Dictionary<string, Card>();

            foreach (var player in activePlayers)
            {
                if (!PlayerHands.ContainsKey(player) || !PlayerHands[player].HasCards())
                {
                    continue;
                }

                var card = PlayerHands[player].PlayCard();
                currentPlayed[player] = card;
                pot.Add(card);
            }

            // Output depends on whether this is a tiebreaker round
            if (isTiebreaker)
            {
                var tiebreakerCards = string.Join(" | ", currentPlayed.Select(kvp => $"{kvp.Key}: {kvp.Value.Rank}"));
                Console.WriteLine($"Tiebreaker: {tiebreakerCards}");
            }
            else
            {
                foreach (var kvp in currentPlayed)
                {
                    Console.WriteLine($"{kvp.Key}: {kvp.Value.Rank}");
                }
            }

            if (currentPlayed.Count == 0)
            {
                return;
            }

            var highestRank = currentPlayed.Values.Max(card => card.Rank);
            var tiedPlayers = currentPlayed.Where(kvp => kvp.Value.Rank == highestRank).Select(kvp => kvp.Key).ToList();

            if (tiedPlayers.Count == 1)
            {
                var winner = tiedPlayers[0];
                GivePotToWinner(winner, pot);
                Console.WriteLine($"Winner: {winner} (Cards: {GetPlayerCardCountSummary()})");
                return;
            }

            // Tie handling
            Console.WriteLine($"Tie between {string.Join(" and ", tiedPlayers)}!");
            Console.WriteLine($"Pot includes: {string.Join(", ", pot.Select(card => card.Rank.ToString()))}");

            // Eliminate tied players who have no cards left to fight
            var tiedWithCards = tiedPlayers.Where(p => PlayerHands[p].HasCards()).ToList();

            if (tiedWithCards.Count == 1)
            {
                var winner = tiedWithCards[0];
                GivePotToWinner(winner, pot);
                Console.WriteLine($"Winner: {winner} (Cards: {GetPlayerCardCountSummary()})");
                return;
            }

            if (tiedWithCards.Count == 0)
            {
                // Unlikely: everyone tied is out of cards; choose the player with max cards.
                var fallbackWinner = PlayerHands.OrderByDescending(kvp => kvp.Value.Count).First().Key;
                GivePotToWinner(fallbackWinner, pot);
                Console.WriteLine($"Winner: {fallbackWinner} (Cards: {GetPlayerCardCountSummary()})");
                return;
            }

            // Continue with tied players in a recursive tiebreaker round
            PlayRound(tiedWithCards, pot, true);
        }

        // Give all pot cards to winner
        public void GivePotToWinner(string winner, List<Card> pot)
        {
            if (!PlayerHands.ContainsKey(winner))
            {
                return;
            }

            foreach (var card in pot)
            {
                PlayerHands[winner].AddCard(card);
            }

            pot.Clear();
        }

        private string GetPlayerCardCountSummary()
        {
            var ordered = PlayerHands.Keys.OrderBy(name => name);
            // Abbreviate player names (e.g., "Player 1" -> "P1")
            return string.Join(", ", ordered.Select(name => 
            {
                var abbrev = name.Replace("Player ", "P");
                return $"{abbrev}={PlayerHands[name].Count}";
            }));
        }
    }
}
