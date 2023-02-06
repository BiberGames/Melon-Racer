using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace BenjaGames.MR;
public class Timer : Panel
{
	public Label Label;

	public Timer()
	{
		Label = Add.Label( "100", "value" );
	}

	public override void Tick()
	{
		var player = Game.LocalPawn;
		if ( player == null ) return;
		Label.Text = $"00 : 00 : 00";
	}
}
