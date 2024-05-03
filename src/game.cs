using System;
using Systems;
using Components;
using Entities;
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
