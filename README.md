[![Review Assignment Due Date](https://classroom.github.com/assets/deadline-readme-button-22041afd0340ce965d47ae6ef1cefeee28c7c493a6346c4f15d667ab976d596c.svg)](https://classroom.github.com/a/hZIAsDPT)

# War Card Game

This is a console game of War in C#. Two to four players can play.

## How to Build

```bash
dotnet build
```

## How to Run

Just run:
```bash
dotnet run --project src/WarGame.Console
```

Or with a number of players:
```bash
dotnet run --project src/WarGame.Console -- 3
```

It will ask how many players (2-4) if you don't give it a number.

## Project Files

- `src/WarGame.Core/` - the game code
  - Card.cs - the cards
  - Deck.cs - the deck of cards
  - Hand.cs - each player's cards
  - WarEngine.cs - the main game logic
- `src/WarGame.Console/` - the console app to run the game
  - Program.cs - starts the game

## How the Game Works

Players take turns playing cards. The highest card wins all the cards in that round. If two players tie, they play again with just those cards. The game keeps going until one player has all the cards or we reach 10,000 rounds.

## Submission

Submitted via GitHub Classroom.

