using Sandbox;
using System;
using System.Timers;

public sealed class EnemyManager : Component
{


    private static int executionCount = 0;
    private const int MaxExecutions = 10;

	[Property] 
	public Enemy Zombie { get; set; }

	protected override void OnStart()
	{
		var timer = Zombie.timeBeen;
		// Log.Info(timer);

	}

	protected override void OnUpdate()
	{
		// Log.Info(timer);
	}

}
