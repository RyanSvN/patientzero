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

	}

	protected override void OnUpdate()
	{	
		var timer = Zombie.timeBeen;
		Log.Info(timer);
		if (executionCount > MaxExecutions)
		{
			executionCount++;
			Log.Info($"Execution {executionCount}:");

			PerformSequence();

			for (int i = 0; i < 10; i++)
			{
				// Simulate some work by printing the iteration number
				Log.Info($"Iteration {i + 1}");
			}
		}
		else
		{	
			Log.Info("Completed 10 executions. Timer stopped.");
		}
	}

	private static void PerformSequence()
	{
		// Simulate some work by iterating 10 times
		for (int i = 0; i < 10; i++)
		{
			// Simulate some work by printing the iteration number
			Console.WriteLine($"Iteration {i + 1}");
		}
	}
}
