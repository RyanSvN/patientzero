using System;
using System.Collections;
using Sandbox;


public sealed class Bullet : Component, Component.ICollisionListener, Component.ITriggerListener
{

	[Property]
	public GameObject zombie { get; set; }

	[Property]
	public float damage { get; set; } = 10f;


	
/* 	public void OnCollisionStart( Collider other )
	{
		var zombie = other.Components.Get<Enemy>();
		if ( zombie != null)
		{
			Log.Info("Zombie");
			zombie.Health -= damage;
			zombie.Health = Math.Clamp ( zombie.Health, 0, zombie.MaxHealth);
		}
	} */

	public void OnTriggerEnter( Collider other )
	{
		var zombie = other.Components.Get<Enemy>();
		if ( zombie != null)
		{
			Log.Info("Zombie Hit!");
			zombie.Health -= damage;
			zombie.Health = Math.Clamp ( zombie.Health, 0, zombie.MaxHealth);
		}
	}

	
}
