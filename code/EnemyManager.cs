using System;

public sealed class EnemyManager : Component
{
	public int Wave;
	public int TotalEnemies;
	public float SpawnMultiplier = 1.5f;
	public int AmountOfEnemiesToSpawn = 2;

	public List<GameObject> SpawnPoints;

	[Property] private GameObject EnemyType;

	protected override void OnUpdate()
	{
	}

	protected override void OnFixedUpdate()
	{
		FetchAllEnemies();
		HasWaveEnded();
	}

	protected override void OnStart()
	{
		if ( !EnemyType.IsValid() )
		{
			Log.Error( "Assign an enemy to the enemy manager" );
		}

		FetchAllSpawnPoints();
		FetchAllEnemies();
	}

	private void HasWaveEnded()
	{
		if ( Wave == 0 )
		{
			AmountOfEnemiesToSpawn = 1;
		}

		if ( TotalEnemies == 0 )
		{
			Wave += 1;
			SpawnNextWave();
		}
	}

	private void SpawnNextWave()
	{
		AmountOfEnemiesToSpawn = (int)Math.Round( AmountOfEnemiesToSpawn * SpawnMultiplier );
		for ( int i = 0; i < AmountOfEnemiesToSpawn; i++ )
		{
			var nextSpawnPosition = SpawnPoints[RandomSpawnPosition()].Transform.LocalPosition;
			SpawnEnemy( nextSpawnPosition );
		}
	}

	private int RandomSpawnPosition()
	{
		Random rnd = new Random();
		return rnd.Next( SpawnPoints.Count );
	}

	private void SpawnEnemy( Vector3 spawnPosition )
	{
		if ( Connection.All.Count > 0 )
		{
			var o = EnemyType.Clone( spawnPosition );
			o.Enabled = true;
			o.NetworkSpawn();
		}
		else
		{
			Log.Error( "No connection or there are no players connected to the lobby" );
		}
	}

	private void FetchAllEnemies()
	{
		var enemies = Scene.GetAllObjects( true ).Where( x => x.Tags.Has( "enemy" ) ).Where( x => x.Active ).ToList();
		TotalEnemies = enemies.Count;
	}

	private void FetchAllSpawnPoints()
	{
		Log.Info( Scene.GetAllObjects( true ).Where( x => x.Tags.Has( "enemy_spawn" ) ) );
		var spawnPoints = Scene.GetAllObjects( true ).Where( x => x.Tags.Has( "enemy_spawn" ) ).ToList();
		if ( spawnPoints.Count == 0 )
		{
			Log.Error( "Missing enemy spawn points. Create an object with tag 'enemy_spawn' to continue" );
		}
		else
		{
			SpawnPoints = spawnPoints;
		}
	}
}
