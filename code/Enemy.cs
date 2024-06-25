using System;

public sealed class Enemy : Component, Component.ITriggerListener
{
	public List<TopDownGPC> Players { get; set; }
	[Property]
	public float Damage = 10f;

	[Property] 
	public float Health = 100f;

	[Property] 
	public float MaxHealth = 100f;

	public TimeSince timeBeen { get; set; } = 0f;

	[Property] 
	public ModelRenderer ZombieModel { get; set; }


	[Property]
	public NavMeshAgent agent;

	public void OnTriggerEnter( Collider other )
	{
		var player = other.Components.Get<TopDownGPC>();
		if ( player != null)
		{
			player.Health -= Damage;
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
		
		FetchAndOrderPlayersByDistanceToZombie();

		if (Health <= 0)
		{
			GameObject.Destroy();
			Log.Info("ZOMBIE DESTROYED!");
			Sound.Play("zombie_die", Transform.Position);
			return;
			
		}

		if (Players.Count == 0)
		{
			return; // Exit if no player is assigned
		}

		var enemyLoc = Transform.LocalPosition;
		var playerLoc = Players.First().Transform.LocalPosition;
		

		var direction = (playerLoc - enemyLoc);
	
		agent.MoveTo( playerLoc );
	}
	private void FetchAndOrderPlayersByDistanceToZombie()
	{
		var playerList = Scene.GetAllComponents<TopDownGPC>();
		var sortedPlayers = playerList.OrderBy(x => Vector3.DistanceBetween(Transform.LocalPosition,x.Transform.LocalPosition));
		Players = sortedPlayers.ToList();
	}
}
