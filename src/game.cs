using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

//comment
class Game
{
	static void Main(string[] args)
	{
		while (true)
		{
			//skapar spelare
			Entity player = new Entity(1);

			//skapar ai
			Entity opponent = new Entity(2);

			choiceComponent playerChoiceComponent = new choiceComponent();
			choiceComponent opponentChoiceComponent = new choiceComponent();

			player.addComponent(playerChoiceComponent);
			opponent.addComponent(opponentChoiceComponent);


			gameSystem game = new gameSystem();
			WinnerSystem winner = new WinnerSystem();

			//startar spelet
			game.Input(player, "");

			//slumpa motst�ndarens val, buggar mycket om det �r system
			choiceComponent opponentChoice = opponent.getComponent<choiceComponent>();
			Random random = new Random();
			int index = random.Next(opponentChoice.choices.Length);

			//F�r motst�ndarens val
			opponentChoice.opponentChoice = opponentChoice.choices[index];

			//f�r spelarens val
			choiceComponent playerChoice = player.getComponent<choiceComponent>();

			//simpel kod f�r att skriva ut val
			Console.WriteLine("spelarens val: " + playerChoice.playerChoice);
			Console.WriteLine("motst�ndarens val: " + opponentChoice.opponentChoice);

			//h�mtar vinnaren och skriver ut vinnaren 
			string vinnaren = winner.getWinner(playerChoice, opponentChoice);
			Console.WriteLine(vinnaren);

			//simpel loop f�r att 
			Console.WriteLine("\nVill du spela en runda till? ja/nej");
			string playAgain = Console.ReadLine().ToLower();
			Console.Write("\n");
			if (playAgain != "ja")
			{
				break;
			}

		}
	}
}

public class choiceComponent
{

	public string playerChoice { get; set; }

	//os�ker p� om denna beh�vs
	public string opponentChoice { get; set; }

	//valen som slumpas f�r motst�ndaren
	public string[] choices = { "sten", "sax", "p�se" };
}


public class Entity
{
	public int id { get; }
	private Dictionary<Type, object> components = new Dictionary<Type, object>();

	public Entity(int id)
	{
		this.id = id;
	}

	public void addComponent<T>(T component)
	{
		components[typeof(T)] = component;
	}

	public T getComponent<T>() where T : class
	{
		if (components.TryGetValue(typeof(T), out var component))
		{
			return component as T;
		}
		return null;
	}

}

public class gameSystem
{
	public void Input(Entity entity, string input)
	{
		var choiceComponent = entity.getComponent<choiceComponent>();
		if (choiceComponent != null)
		{
			//fr�gar anv�ndaren och tar input fr�n anv�ndaren
			Console.WriteLine("v�lj sten sax eller p�se");
			input = Console.ReadLine().ToLower();

			//kollar s� input �r godk�nt
			if (input != "sten" && input != "sax" && input != "p�se")
			{
				//om det inte �r godk�nt
				Console.WriteLine("inte ett godk�nt alternativ");
				choiceComponent.playerChoice = "diskad";
			}
			else
			{
				//sparar input till playerchoice i choicecomponent
				choiceComponent.playerChoice = input;


			}

		}
	}
}

public class WinnerSystem
{
	//h�mtar playerchoihce och opponentChoice fr�n choicecomponent
	public string getWinner(choiceComponent playerChoice, choiceComponent opponentChoice)
	{
		//simpel if f�r att kolla vem som vann
		if (playerChoice.playerChoice == opponentChoice.opponentChoice)
		{
			return "oavgjort, det var inget kul";
		}
		else if ((playerChoice.playerChoice == "sten" && opponentChoice.opponentChoice == "sax") ||
				 (playerChoice.playerChoice == "sax" && opponentChoice.opponentChoice == "p�se") ||
				 (playerChoice.playerChoice == "p�se" && opponentChoice.opponentChoice == "sten"))
		{
			return "Vilken tur du har, du vann!";
		}
		else
		{
			return "japp, du f�rlorade";
		}
	}
}

