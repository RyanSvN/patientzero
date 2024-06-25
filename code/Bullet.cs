using System;
using System.Collections.ObjectModel;

public sealed class Bullet : Component, Component.ITriggerListener
{
	[Property] public float Damage = 10f;

	public void OnTriggerEnter( Collider other )
	{
		var zombie = other.Components.Get<Enemy>();
		if ( zombie != null )
		{
			Log.Info( "Zombie Hit!" );
			zombie.Health -= Damage;
			zombie.Health = Math.Clamp( zombie.Health, 0, zombie.MaxHealth );
			Sound.Play("zombie_die", zombie.Transform.Position);
			zombie.ZombieModel.Tint = "#5E0000";
		}
	}
}
