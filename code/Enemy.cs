using System;
using System.Collections;
using Sandbox;

public sealed class Enemy : Component, Component.ITriggerListener
{
	[Property]
	public GameObject player { get; set; }

	//Zombie Damage
	[Property]
	public float damage { get; set; } = 10f;

	[Property] 
	public float Health { get; set; } = 100f;

	[Property] 
	public float MaxHealth { get; set; } = 100f;

	public TimeSince timeBeen { get; set; } = 0f;



	[Property]
	public NavMeshAgent agent;

	public void OnTriggerEnter( Collider other )
	{
		var player = other.Components.Get<TopDownGPC>();
		if ( player != null)
		{
			player.Health -= damage;
			player.Health = Math.Clamp ( player.Health, 0, player.MaxHealth);
		}
	}

	public void OnTriggerExit( Collider other )
	{
		Log.Info(other);
	}
	
	protected override void OnStart()
	{	

	}

	protected override void OnUpdate()
	{	
		Log.Info(Health);

		if (Health <= 0)
        {
            GameObject.Destroy();
			Log.Info("ZOMBIE DESTROYED!");
            return;
        }

		if (player == null)
        {
            return; // Exit if no player is assigned
        }
		else
		{
			// Log.Info("Hello!");
		}

		var enemyLoc = Transform.LocalPosition;
		var enemyRot = Transform.LocalRotation;
		var playerLoc = player.Transform.LocalPosition;
		
		// Log.Info(enemyRot);
		// Log.Info(playerLoc);

		var direction = (playerLoc - enemyLoc);
		// Transform.LocalPosition += direction * speed * Time.Delta;
		
	
		agent.MoveTo( playerLoc );

		// Transform.LocalRotation = Rotation.LookAt(playerLoc);
		var velocity = agent.Velocity;
		// Log.Info(velocity); 
	}

}
