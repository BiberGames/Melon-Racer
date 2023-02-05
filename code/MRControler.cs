using Sandbox;

namespace BenjaGames.MR;

class MRController : BasePlayerController
{
	// An example BuildInput method within a player's Pawn class.
	[ClientInput] public Vector3 InputDirection { get; protected set; }
	[ClientInput] public Angles ViewAngles { get; set; }
	
	public override void BuildInput()
	{
		InputDirection = Input.AnalogMove;
	
		var look = Input.AnalogLook;
	
		var viewAngles = ViewAngles;
		viewAngles += look;
		ViewAngles = viewAngles.Normal;
	}
	
	public override void FrameSimulate()
	{
		base.FrameSimulate();
	}
	
	public override void Simulate()
	{
		// Create a direction vector from the input from the client
		var direction = new Vector3( Input.AnalogMove.x, Input.AnalogMove.y, 0 );

		// Rotate the vector so forward is the way we're facing
		direction *= Camera.Rotation;

		// Normalize it and multiply by speed
		direction = direction.Normal * 500;

		// Apply the move
		Velocity += direction * Time.Delta;
	}
}
