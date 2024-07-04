using Sandbox;
using System;

public sealed class DoorController : Component, Component.ITriggerListener
{

	// public List<TopDownGPC> Players { get; set; }
	// [Property]

	[Property] public GameObject doorParticlesBlue { get; set; }
	[Property] public GameObject doorParticlesRed { get; set; }

	[Property] public GameObject doorMessage { get; set; }

	[Property] public float doorCost { get; set; } = 100f;

	

/* 
	protected override void OnUpdate()
	{

	}
 */
	public void OnTriggerEnter( Collider other )
    {
		var Players = other.Components.Get<TopDownGPC>();
        if (Players != null)
        {
            Players.doorInRange = this;
			// Log.Info(Players.playerInDoorRange);
			doorMessage.Enabled = true;
			doorParticlesRed.Enabled = true;
			if (Players.Currency >= doorCost)
			{
				doorParticlesRed.Enabled = false;
				doorParticlesBlue.Enabled = true;
				
			}
        }
    }

	public void OnTriggerExit( Collider other ) 
    {
		var Players = other.Components.Get<TopDownGPC>();
		if (Players != null)
        {
			Players.doorInRange = null;
			// Log.Info(Players.playerInDoorRange);
			doorParticlesBlue.Enabled = false;
			doorParticlesRed.Enabled = false;
			doorMessage.Enabled = false;
		}

    }

/* 
	protected override void OnUpdate() // Called on every frame
	{
		if (playerInDoorRange
	} */

	public void playerOpen(TopDownGPC Player)
	{
 		if (Player.IsValid)
		{
			Log.Info("Door Tried");
			if (Player.Currency >= doorCost)
			{
				Log.Info("Door Open");
				Player.Currency -= doorCost;
				GameObject.Destroy();
			}
		}
		else
		{
			Log.Info("Not Trying door");
		}		
	}
}
