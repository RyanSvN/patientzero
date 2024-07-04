
public sealed class Gun : Component
{
	[Property] public GameObject ObjectToSpawn { get; set; }

	[Property] public int NumberOfMags = 3;
	[Property] public int AmmoPerMag = 16;
	[Property] public int CurrentAmmoInMag;
	[Property] public int CurrentMags;
	private Vector3 SoundLocation;
	private bool IsReloading = false;

	protected override void OnUpdate()
	{
		if ( IsProxy )
			return;
		
		var pc = Components.GetInAncestors<TopDownGPC>();
		if ( pc is null )
			return;

		//SoundLocation = pc.Camera.Transform.Position;
		SoundLocation = Transform.Position;


		if ( Input.Pressed( "Attack1" ) )
		{
			if ( CurrentAmmoInMag != 0 )
			{
				var lookDirection = pc.EyeAngles.ToRotation();
				if ( !IsReloading )
				{
					OnShoot( lookDirection );
				}
			}
			else
			{
				Sound.Play( "gun_empty", SoundLocation );
			}
		}

		if ( Input.Pressed( "reload" ) )
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
		Sound.Play("gun_shoot", SoundLocation);
		CurrentAmmoInMag -= 1;
	}

	private void OnReload()
	{
		if ( CurrentMags == 0 )
		{
			return;
		}

		IsReloading = true;

		CurrentAmmoInMag = AmmoPerMag;
		CurrentMags -= 1;
		Sound.Play("gun_reload", SoundLocation);
		
		IsReloading = false; // TODO: Don't allow shooting while reloading?
	}


	protected override void OnStart()
	{
		CurrentAmmoInMag = AmmoPerMag;
		CurrentMags = NumberOfMags;
	}
}
