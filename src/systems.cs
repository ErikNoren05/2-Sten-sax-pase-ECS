namespace Systems
{

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





}


