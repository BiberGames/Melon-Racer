using Sandbox.UI;
using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenjaGames.MR;

public partial class SandboxHud : HudEntity<RootPanel>
{
	public SandboxHud()
	{
		if ( !Game.IsClient )
			return;

		RootPanel.StyleSheet.Load( "/styles/sandbox.scss" );

		//RootPanel.AddChild<Timer>();
		RootPanel.AddChild<LapCounter>();
		//RootPanel.AddChild<VoiceSpeaker>();
		//RootPanel.AddChild<KillFeed>();
		//RootPanel.AddChild<Scoreboard<ScoreboardEntry>>();
		//RootPanel.AddChild<Health>();
	}
}
