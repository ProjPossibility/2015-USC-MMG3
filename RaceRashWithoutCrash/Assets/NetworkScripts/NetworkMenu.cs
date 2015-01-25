using UnityEngine;
using System.Collections;

public class NetworkMenu : MonoBehaviour {
	//Camera standbyCamera;

	SpawnSpot[] spawnSpots; 
	// Use this for initialization
	void Start () {
		spawnSpots = GameObject.FindObjectsOfType<SpawnSpot> ();
		Connect ();
	}

	void Connect()
	{
		PhotonNetwork.ConnectUsingSettings("CarRacing 1.0.0");
	}
	void OnGUI()
	{
		GUILayout.Label (PhotonNetwork.connectionStateDetailed.ToString ()); 
	}
	void OnJoinedLobby()
	{
		PhotonNetwork.JoinRandomRoom ();
	}
	void OnPhotonRandomJoinFailed()
	{
		Debug.Log ("OnPhotonRandomJoinFailed");
		PhotonNetwork.CreateRoom (null);
	}
	void OnJoinedRoom()
	{
		Debug.Log ("OnJoinedRoom");
		SpawnPlayer ();
	}
	void SpawnPlayer()
	{
		if (spawnSpots == null) 
		{
			Debug.LogError("Error in SpwanSpots array");
			return;
		}
		//Instantiate (playerPrefab);
		SpawnSpot mySpawnSpot = spawnSpots [Random.Range (0, spawnSpots.Length)]; 
		//Vector3 pos = new Vector3(834,3,712);
		GameObject myPlayerGO = (GameObject)PhotonNetwork.Instantiate ("First Person Controller", mySpawnSpot.transform.position, Quaternion.identity, 0);

		((MonoBehaviour)myPlayerGO.GetComponent("FPSInputController")).enabled = true;
		((MonoBehaviour)myPlayerGO.GetComponent("CharacterMotor")).enabled = true;
		myPlayerGO.transform.FindChild("Main Camera").gameObject.SetActive(true);
	}
}

