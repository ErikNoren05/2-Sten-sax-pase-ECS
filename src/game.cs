```csharp
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

/// <summary>
/// Represents a simple game of rock-paper-scissors.
/// </summary>
class Game
{
    static void Main(string[] args)
    {
        while (true)
        {
            // Create player entity
            Entity player = new Entity(1);

            // Create AI opponent entity
            Entity opponent = new Entity(2);

            // Initialize choice components for player and opponent
            ChoiceComponent playerChoiceComponent = new ChoiceComponent();
            ChoiceComponent opponentChoiceComponent = new ChoiceComponent();

            // Add choice components to entities
            player.AddComponent(playerChoiceComponent);
            opponent.AddComponent(opponentChoiceComponent);

            // Initialize game system and winner system
            GameSystem game = new GameSystem();
            WinnerSystem winner = new WinnerSystem();

            // Start the game
            game.Input(player, "");

            // Randomize opponent's choice
            ChoiceComponent opponentChoice = opponent.GetComponent<ChoiceComponent>();
            Random random = new Random();
            int index = random.Next(opponentChoice.Choices.Length);
            opponentChoice.OpponentChoice = opponentChoice.Choices[index];

            // Get player's choice
            ChoiceComponent playerChoice = player.GetComponent<ChoiceComponent>();

            // Print out choices
            Console.WriteLine("Player's choice: " + playerChoice.PlayerChoice);
            Console.WriteLine("Opponent's choice: " + opponentChoice.OpponentChoice);

            // Determine the winner and print the result
            string winnerMessage = winner.GetWinner(playerChoice, opponentChoice);
            Console.WriteLine(winnerMessage);

            // Prompt for another round
            Console.WriteLine("\nDo you want to play another round? yes/no");
            string playAgain = Console.ReadLine().ToLower();
            Console.Write("\n");
            if (playAgain != "yes")
            {
                break;
            }
        }
    }
}

/// <summary>
/// Represents a choice component for the game entities.
/// </summary>
public class ChoiceComponent
{
    public string PlayerChoice { get; set; }
    public string OpponentChoice { get; set; }
    public string[] Choices = { "rock", "paper", "scissors" };
}

/// <summary>
/// Represents an entity in the game.
/// </summary>
public class Entity
{
    public int Id { get; }
    private Dictionary<Type, object> components = new Dictionary<Type, object>();

    public Entity(int id)
    {
        this.Id = id;
    }

    public void AddComponent<T>(T component)
    {
        components[typeof(T)] = component;
    }

    public T GetComponent<T>() where T : class
    {
        if (components.TryGetValue(typeof(T), out var component))
        {
            return component as T;
        }
        return null;
    }
}

/// <summary>
/// Represents the game system for handling player input.
/// </summary>
public class GameSystem
{
    public void Input(Entity entity, string input)
    {
        var choiceComponent = entity.GetComponent<ChoiceComponent>();
        if (choiceComponent != null)
        {
            // Prompt user and get input
            Console.WriteLine("Choose rock, paper, or scissors:");
            input = Console.ReadLine().ToLower();

            // Check if input is valid
            if (input != "rock" && input != "paper" && input != "scissors")
            {
                // If input is not valid
                Console.WriteLine("Invalid choice");
                choiceComponent.PlayerChoice = "disqualified";
            }
            else
            {
                // Save input to player choice in choice component
                choiceComponent.PlayerChoice = input;
            }
        }
    }
}

/// <summary>
/// Represents the winner system for determining the game winner.
/// </summary>
public class WinnerSystem
{
    /// <summary>
    /// Determines the winner based on player and opponent choices.
    /// </summary>
    /// <param name="playerChoice">Player's choice component.</param>
    /// <param name="opponentChoice">Opponent's choice component.</param>
    /// <returns>Message indicating the winner.</returns>
    public string GetWinner(ChoiceComponent playerChoice, ChoiceComponent opponentChoice)
    {
        if (playerChoice.PlayerChoice == opponentChoice.OpponentChoice)
        {
            return "It's a tie, how boring!";
        }
        else if ((playerChoice.PlayerChoice == "rock" && opponentChoice.OpponentChoice == "scissors") ||
                 (playerChoice.PlayerChoice == "scissors" && opponentChoice.OpponentChoice == "paper") ||
                 (playerChoice.PlayerChoice == "paper" && opponentChoice.OpponentChoice == "rock"))
        {
            return "Congratulations, you won!";
        }
        else
        {
            return "Sorry, you lost.";
        }
    }
}
```
