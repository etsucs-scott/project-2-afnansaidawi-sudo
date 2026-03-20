using System;
using WarGame.Core;

// This console app is only responsible for input and delegating to WarEngine.
// All game logic must remain in WarGame.Core.
var playerCount = 0;

// Check command-line arguments first.
if (args.Length > 0 && int.TryParse(args[0], out var value) && value >= 2 && value <= 4)
{
    playerCount = value;
}

// If argument is missing or invalid, prompt until we get valid input.
while (playerCount < 2 || playerCount > 4)
{
    Console.Write("Enter number of players (2-4): ");
    var input = Console.ReadLine();
    if (!int.TryParse(input, out value) || value < 2 || value > 4)
    {
        Console.WriteLine("Invalid entry. Please enter a number between 2 and 4.");
        continue;
    }

    playerCount = value;
}

Console.WriteLine($"Starting War with {playerCount} players!");

var engine = new WarEngine();
engine.StartGame(playerCount);

