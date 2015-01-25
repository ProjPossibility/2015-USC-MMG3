using UnityEngine;
using System.Collections;

public class NetworkManager: MonoBehaviour 
{
	public string connectionIP = "127.0.0.1";
	public int portNo = 8632;
	private bool connected = false;



	private void OnConnectedToServer()
	{
		//A client connected
		connected = true;
	}

	private void OnServerInitialized()
	{
		//A client connected
		connected = true;
	}
	private void OnDisconnectedFromServer()
	{
		//Connection is lost
		connected = false;
	}


	private void OnGUI()	
	{
		if (!connected) 
		{
						connectionIP = GUILayout.TextField (connectionIP);
						int.TryParse(GUILayout.TextField (portNo.ToString()),out portNo);

						if (GUILayout.Button ("Connect")) 
						{
								Network.Connect(connectionIP, portNo);
						}
						if (GUILayout.Button ("Host")) 
						{
					Debug.Log("Inside");
								Network.InitializeServer (4, portNo, true);
						}
		 } else 
		 {
				GUILayout.Label("Connections: "+ Network.connections.Length.ToString());

		 }

	}
	
	
}
