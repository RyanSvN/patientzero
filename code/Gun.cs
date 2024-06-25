using System.Runtime.InteropServices.JavaScript;

public sealed class Gun : Component
{
	[Property] public GameObject ObjectToSpawn { get; set; }

	[Property] public int NumberOfMags = 3;
	[Property] public int AmmoPerMag = 8;
	[Property] public int CurrentAmmoInMag;
	[Property] public int CurrentMags;

	protected override void OnUpdate()
	{
		if ( IsProxy )
			return;

		if ( Input.Pressed( "Attack1" ) )
		{
			if ( CurrentAmmoInMag != 0 )
			{
				var pc = Components.GetInAncestors<TopDownGPC>();
				if ( pc is null )
					return;

				var lookDirection = pc.EyeAngles.ToRotation();
				OnShoot( lookDirection );
			}
		}

		if ( Input.Pressed( "Reload" ) )
		{
			OnReload();
		}
	}

	private void OnShoot( Rotation lookDirection )
	{
		var pos = Transform.Position + Vector3.Up * 40.0f + lookDirection.Forward.WithZ( 0.0f ) * 50.0f;

		var o = ObjectToSpawn.Clone( pos );
		o.Enabled = true;

		var p = o.Components.Get<Rigidbody>();
		p.Velocity = lookDirection.Forward * 750.0f + Vector3.Up * 0.0f;

		o.NetworkSpawn();
	}

	private void OnReload()
	{
		if ( CurrentMags == 0 )
		{
			return;
		}

		CurrentAmmoInMag = AmmoPerMag;
		CurrentMags -= 1;
	}


	protected override void OnStart()
	{
		CurrentAmmoInMag = AmmoPerMag;
		CurrentMags = NumberOfMags;
	}
}
