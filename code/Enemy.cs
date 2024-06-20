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

		Vector3 direction = (playerLoc - enemyLoc);
		// Transform.LocalPosition += direction * speed * Time.Delta;

		agent.MoveTo( playerLoc );

		// Transform.LocalRotation = Rotation.LookAt(playerLoc);
		var velocity = agent.Velocity;
		// Log.Info(velocity); 
	}

}
