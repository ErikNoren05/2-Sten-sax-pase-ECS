namespace Components
{


    /// <summary>
    /// Represents a choice component for the game entities.
    /// </summary>
    public class ChoiceComponent
    {
        public string PlayerChoice { get; set; }
        public string OpponentChoice { get; set; }
        public string[] Choices = { "rock", "paper", "scissors" };
    }


}