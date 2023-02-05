using Sandbox;
using System;
using System.Linq;

namespace BenjaGames.MR;

partial class MRPlayer : Player
{
	private int myLap = 0;
	public LapTrigger Lap { get; set; }
	public override void Respawn()
	{
		base.Respawn();

		SetModel( "models/sbox_props/watermelon/watermelon.vmdl" );

		Controller = new MRController();
		Health = 1;

		UsePhysicsCollision = true;
		EnableAllCollisions = true;
		EnableShadowCasting = true;

		SetupPhysicsFromModel( PhysicsMotionType.Dynamic );
	}
	public virtual void OnLapFinish( LapTrigger _LapTrigger )
	{
		Log.Info( $"{_LapTrigger.TouchingEntities.First().Name} {myLap.ToString()}" );
		Health++;
	}
	public override void Simulate( IClient cl )
	{
		base.Simulate( cl );

		// A Hack to make the camera work when player has physics enabled
		ThirdPersonCamera();
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

		Particles.Create( "particles/impact.flesh-big.vpcf", Position );

		// Spawns Melon Gibs
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
		if ( Client.IsBot )
			return;

		Camera.FirstPersonViewer = this;

		Camera.Rotation = ViewAngles.ToRotation();

		var pos = base.CollisionWorldSpaceCenter + Vector3.Up * 10;
		var targetPos = pos + Camera.Rotation.Backward * 100;

		var tr = Trace.Ray( pos, targetPos ).WithAnyTags( "solid" ).Ignore( this ).Radius( 8 ).Run();

		Camera.Position = tr.EndPosition;
	}
}
