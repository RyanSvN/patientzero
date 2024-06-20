using System;
using System.Collections;
using Sandbox;

public sealed class Enemy : Component
{
	[Property]
	public GameObject player { get; set; }

	private float speed = 1f; // Adjust the speed as needed


	[Property]
	public NavMeshAgent agent;
	


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
			Log.Info("Hello!");
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
		Log.Info(velocity);

		
	}

}
