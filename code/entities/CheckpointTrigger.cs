using Sandbox;
using Editor;

namespace BenjaGames.MR;

[Library( "trigger_checkpoint" )]
[Title( "Checkpont" )]
[HammerEntity]
public partial class CheckpointTrigger : BaseTrigger
{
	public override void StartTouch( Entity other )
	{
		if ( other is MRPlayer Player )
		{
			Player.OnCheckpoint( this );
		}

		base.StartTouch( other );
	}
}
