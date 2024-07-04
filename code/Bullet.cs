using System;

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
			ApplyTint(zombie);
			DelayedDestroy();
		}
	}

	private async void ApplyTint(Enemy zombie)
	{
		zombie.ZombieModel.Tint = "#5E0000";
        await Task.Delay(50); // Wait for 500 milliseconds (half a second)
        zombie.ZombieModel.Tint = "#FFFFFF";
	}

	private async void DelayedDestroy()
    {
        await Task.Delay(50); // Wait for 500 milliseconds (half a second)
        GameObject.Destroy();
    }
}
