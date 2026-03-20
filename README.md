[![Review Assignment Due Date](https://classroom.github.com/assets/deadline-readme-button-22041afd0340ce965d47ae6ef1cefeee28c7c493a6346c4f15d667ab976d596c.svg)](https://classroom.github.com/a/hZIAsDPT)

# War Card Game Simulation

A console-based implementation of the card game **War** in C#. This project demonstrates core data structures (Stack, Queue, Dictionary, List) and game state management.

## Overview

This is a two-project solution:
- **WarGame.Core**: Game logic library containing Card, Deck, Hand, and WarEngine classes
- **WarGame.Console**: Thin console app that accepts player count and runs the simulation

The game supports 2, 3, or 4 players and continues until one player holds all cards or the 10,000-round limit is reached.

## Build

```bash
dotnet build
```

## Run

**Interactive mode** (prompts for player count):
```bash
dotnet run --project src/WarGame.Console
```

**Command-line argument** (specify 2-4 players):
```bash
dotnet run --project src/WarGame.Console -- 3
```

## Player Count Selection

The console app follows this logic:
1. If a command-line argument is provided and is a valid integer 2-4, use it directly
2. Otherwise, prompt the user: `Enter number of players (2-4):` 
3. Keep prompting until a valid number is entered

## Game Rules Implemented

- Standard 52-card deck (4 suits × 13 ranks)
- Rank order: 2 (low) through Ace (high); suits ignored
- Each round: all players reveal top card; highest rank wins and collects pot
- **Ties**: Only tied players play a tiebreaker round (one card each)
- **Pot**: All cards from tied rounds go into shared pot; eventual winner takes entire pot
- **Elimination**: Players with 0 cards are eliminated
- **Round limit**: 10,000 rounds maximum; winner is player with most cards (or draw if tied)

## Project Structure

```
src/
├── WarGame.Core/          # Game library
│   ├── Card.cs            # Card: Suit, Rank, IComparable
│   ├── Deck.cs            # Deck: Stack<Card>, 52-card shuffle
│   ├── Hand.cs            # Hand: Queue<Card> per player
│   └── WarEngine.cs       # Main game engine + round logic
└── WarGame.Console/       # Console runner
    └── Program.cs         # Input handling + engine startup

UML.png                    # Class diagram (included)
```

## Example Output

```
Round 12
Player 1: K
Player 2: 5
Player 3: K
Tie between Player 1 and Player 3!
Pot includes: K, 5, K
Tiebreaker: Player 1: 9 | Player 3: 2
Winner: Player 1 (Cards: Player 1=26, Player 2=12, Player 3=14)
```

## Submission

This repository is submitted via GitHub Classroom. See the assignment link above.
