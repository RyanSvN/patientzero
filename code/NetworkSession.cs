using Sandbox.Network;

public sealed class NetworkSession : Component
{
	protected override void OnStart()
	{
		//
		// Create a lobby if we're not connected
		//
		if ( !GameNetworkSystem.IsActive )
		{
			GameNetworkSystem.CreateLobby();
		}
	}
}
