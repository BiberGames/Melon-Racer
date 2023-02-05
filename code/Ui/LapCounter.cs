using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace BenjaGames.MR;
public class LapCounter : Panel
{
	public Label Label;

	public LapCounter()
	{
		Label = Add.Label( "100", "value" );
	}

	public override void Tick()
	{
		var player = Game.LocalPawn;
		if ( player == null ) return;

		Label.Text = $"Lap   {player.Health.CeilToInt()-1}";
	}
}
