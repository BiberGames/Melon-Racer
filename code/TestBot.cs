using Sandbox;
using static Sandbox.Event;

//
// You don't need to put things in a namespace, but it doesn't hurt.
//
namespace BenjaGames.MR;
public class TestBot : Bot
{
	[ConCmd.Admin( "bot_custom", Help = "Spawn my custom bot." )]
	internal static void SpawnCustomBot()
	{
		Game.AssertServer();

		// Create an instance of your custom bot.
		_ = new TestBot();
	}

	public override void BuildInput()
	{
		// Here we can choose / modify the bot's input each tick.
		// We'll make them constantly attack by holding down the PrimaryAttack button.
		Input.SetButton( InputButton.Forward, true );
	}

	public override void Tick()
	{
		// Here we can do something with the bot each tick.
		// Here we'll print our bot's name every tick.
	}
}
