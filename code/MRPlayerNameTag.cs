using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace BenjaGames.MR;
internal class MRPlayerNameTag : WorldPanel
{
	public MRPlayerNameTag()
	{
		StyleSheet.Load( "/styles/sandbox.scss" );
		Add.Label( $"{Game.UserName}" );
	}
}
