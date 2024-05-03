using NUnit.Framework;
using System;
using System.IO;

[TestFixture]
public class GameTests
{
	private StringWriter _output;

	[SetUp]
	public void Setup()
	{
		_output = new StringWriter();
		Console.SetOut(_output);
	}

	[TearDown]
	public void TearDown()
	{
		_output.Dispose();
	}

	[Test]
	public void Game_RockVsScissors_PlayerWins()
	{
		// Arrange
		string input = "sten\nnej\n";
		StringReader reader = new StringReader(input);
		Console.SetIn(reader);

		// Act
		Game.Main(null);

		// Assert
		string[] outputLines = _output.ToString().Split(Environment.NewLine);
		Assert.AreEqual("spelarens val: sten", outputLines[5]);
		Assert.IsTrue(outputLines[6].Contains("motståndarens val: "));
		Assert.IsTrue(outputLines[7].Contains("Vilken tur du har, du vann!"));
	}

	[Test]
	public void Game_InvalidInput_PlayerDisqualified()
	{
		// Arrange
		string input = "invalid\nnej\n";
		StringReader reader = new StringReader(input);
		Console.SetIn(reader);

		// Act
		Game.Main(null);

		// Assert
		string[] outputLines = _output.ToString().Split(Environment.NewLine);
		Assert.AreEqual("inte ett godkänt alternativ", outputLines[5]);
		Assert.AreEqual("spelarens val: diskad", outputLines[6]);
	}
}
