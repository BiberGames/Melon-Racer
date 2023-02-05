using Sandbox;
using Editor;

namespace BenjaGames.MR;

[Library( "trigger_new_lap" )]
[Title( "New Lap Trigger" )]
[HammerEntity]
public partial class LapTrigger : BaseTrigger
{
	public override void EndTouch( Entity other )
	{
		if ( other is MRPlayer Player )
		{
			Player.OnLapFinish( this );
		}

		base.StartTouch( other );
	}
}
