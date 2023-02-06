using Sandbox;
using Sandbox.UI;
using System;
using System.Linq;
using static Sandbox.Event;

namespace BenjaGames.MR;

partial class MRPlayer : Player
{
	private int CheckpontCount { get; set; } = 0;

	[Net]
	private int MyLap { get; set; } = 0;

	private bool[] Checkponts;
	public LapTrigger Lap { get; set; }
	public override void Respawn()
	{
		base.Respawn();

		CheckpontCount = Sandbox.Entity.All.OfType<CheckpointTrigger>().Count();

		Checkponts = new bool[CheckpontCount];

		SetModel( "models/sbox_props/watermelon/watermelon.vmdl" );

		Controller = new MRController();

		Health = 1;

		UsePhysicsCollision = true;
		EnableAllCollisions = true;
		EnableShadowCasting = true;

		SetupPhysicsFromModel( PhysicsMotionType.Dynamic );
	}

	[ConCmd.Server( "MRReport" )]
	public static void Report()
	{
		if ( ConsoleSystem.Caller.Pawn is MRPlayer basePlayer )
		{
			Log.Info( "---===Report===----------" );
			Log.Info( "CheckpontCount: " + basePlayer.CheckpontCount );
			Log.Info( "MyLap: " + basePlayer.MyLap );

			for ( int CPI = 0; CPI < basePlayer.CheckpontCount; CPI++ )
			{
				Log.Info( $"Checkpont {CPI}: {basePlayer.Checkponts[CPI	]}" );
			}
		}
	}

	public virtual void OnCheckpoint( CheckpointTrigger _CheckpointTrigger )
	{
		Checkponts[_CheckpointTrigger.Name.ToInt()-1] = true;
		//Log.Info( $"Chekpoint {_CheckpointTrigger.Name.ToInt()} {Checkponts[_CheckpointTrigger.Name.ToInt()]}!" );
	}

	public virtual void OnLapFinish( LapTrigger _LapTrigger )
	{
		// Checks if every checkpoint has been activated
		for ( int CPI = 0; CPI < Checkponts.Length; CPI++ )
		{
			if ( Checkponts[CPI] == false )
				return;

			Checkponts = new bool[CheckpontCount];
			MyLap++;

			//Log.Info( $"Lap {MyLap}!" );
		}
	}

	[Event.Client.Frame]
	void LocalCamera()
	{
		if ( this.IsLocalPawn )
			ThirdPersonCamera();
	}

	public int GetCurrentLap()
	{ 
		return MyLap;
	}

	public override void Simulate( IClient cl )
	{
		base.Simulate( cl );

		// A Hack to make the camera work when player has physics enabled
		//ThirdPersonCamera();
	}

	public override void FrameSimulate( IClient cl )
	{
		base.FrameSimulate( cl );

		// FIX THE CAMERA!!!!!!!!!!!
		//ThirdPersonCamera();
	}


	[ConCmd.Admin( "kill" )]
	static void DoPlayerSuicide()
	{
		if ( ConsoleSystem.Caller.Pawn is MRPlayer basePlayer )
		{
			basePlayer.TakeDamage( new DamageInfo { Damage = basePlayer.Health * 99 } );
		}
	}

	public override void TakeDamage( DamageInfo info )
	{
		base.TakeDamage( info );
	}

	public override void OnKilled()
	{
		base.OnKilled();

		// Spawns Melon Gibs
		Particles.Create( "particles/impact.flesh-big.vpcf", Position );
		Particles.Create( "particles/impact.flesh-big.vpcf", Position );

		SetModel( "models/sbox_props/watermelon/watermelon_gib01.vmdl" );
		PhysicsGroup.Velocity = Velocity;

		var ragdoll = new ModelEntity();
		ragdoll.SetModel( "models/sbox_props/watermelon/watermelon_gib10.vmdl" );
		ragdoll.Position = Position;
		ragdoll.Rotation = Rotation;
		ragdoll.SetupPhysicsFromModel( PhysicsMotionType.Dynamic, false );
		ragdoll.PhysicsGroup.Velocity = Velocity;

		var ragdoll2 = new ModelEntity();
		ragdoll2.SetModel( "models/sbox_props/watermelon/watermelon_gib13.vmdl" );
		ragdoll2.Position = Position;
		ragdoll2.Rotation = Rotation;
		ragdoll2.SetupPhysicsFromModel( PhysicsMotionType.Dynamic, false );
		ragdoll2.PhysicsGroup.Velocity = Velocity;
	}

	void ThirdPersonCamera()
	{
		Camera.FirstPersonViewer = null;

		Camera.Rotation = ViewAngles.ToRotation();

		var pos = base.CollisionWorldSpaceCenter + Vector3.Up * 10;
		var targetPos = pos + Camera.Rotation.Backward * 100;

		var tr = Trace.Ray( pos, targetPos ).WithAnyTags( "solid" ).Ignore( this ).Radius( 8 ).Run();

		Camera.Position = tr.EndPosition;
	}
}
