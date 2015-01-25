using UnityEngine;
using System.Collections;

public class Networking : MonoBehaviour 
{
	string registeredGameName = "Akash_Networking_TestServer";
	bool isRefresing = false;
	float refreshRequestLength = 3.0f;
	HostData[] hostData;

	private void StartServer()
	{
		Network.InitializeServer (16, 25002, false);
		MasterServer.RegisterHost(registeredGameName, "Network Tutorial Akash Test", "Test implementation");

	}
	void OnServerInitialized()
	{
		Debug.Log ("Server Has Been Initialised");
	}
	void OnMasterServerEvent(MasterServerEvent masterServerEvent)
	{
		if (masterServerEvent == MasterServerEvent.RegistrationSucceeded)
						Debug.Log ("Registration Successful");

	}
	public IEnumerator RefreshHostList()
	{
		Debug.Log ("Refreshing...");
		MasterServer.RequestHostList(registeredGameName);
		float timeStarted = Time.time;
		float timeEnd = Time.time + refreshRequestLength;

		while (Time.time < timeEnd) 
		{
			hostData = MasterServer.PollHostList ();
			yield return new WaitForEndOfFrame();
		}
		if(hostData == null || hostData.Length == 0)
			Debug.Log ("No active servers have been found.");
		else
			Debug.Log (hostData.Length + "have been found");

	}  


	//// Use this for initialization
	//void Start () {
//	
//	}
//	
	// Update is called once per frame
//	void Update () {
//	
//	}




	public void OnGUI()
	{

		if (Network.isClient || Network.isServer)
						return;

		if (GUI.Button (new Rect (25f, 25f, 150f, 30f), "Start New Server"))
		{
			//Start server function here
			StartServer();
		}
		if (GUI.Button (new Rect (25f, 65f, 150f, 30f), "Refresh Server List"))
		{
			//Refresh server function here
			StartCoroutine("RefreshHostList");
		}

		if(hostData != null)
		{
			for(int i = 0; i <hostData.Length; i++)
			{
				if(GUI.Button(new Rect(Screen.width/2, 65f + (30f * i) ,300f, 30f), hostData[i].gameName))
				{
					Network.Connect(hostData[i]);
				}
			}
		}

	}


}
