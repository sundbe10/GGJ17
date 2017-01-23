using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> {

	enum State{
		ACTIVE,
		ENDED
	}

	State _state = State.ACTIVE;

	public static int score = 1000;
	public static bool gameActive = true;

	GameObject[] players;

	// Use this for initialization
	void Start () {
		players = GameObject.FindGameObjectsWithTag("Player");
		Debug.Log(players[0]);
	}
	
	// Update is called once per frame
	void Update () {
		switch(_state){
		case State.ACTIVE:
			CheckScores();
			break;
		case State.ENDED:
			if (Input.GetButton("Submit"))
				SceneManager.LoadScene("Scenes/Main");
			break;
		}
	}

	void CheckScores(){
		foreach(GameObject player in players){
			if(player.GetComponentInChildren<ScoreManager>().score > score){
				EndGame(player);
			}
		}
	}

	void EndGame(GameObject winner){
		gameActive = false;
		_state = State.ENDED;
		foreach(GameObject player in players){
			player.GetComponentsInChildren<PlayerScript>()[0].SetAI();
		}
		WanderingCamera cameraWander =  GameObject.Find("CameraWander").GetComponent<WanderingCamera>();
		cameraWander.enabled = true;
		var lookTarget = GameObject.Find("Look Target").transform;
		lookTarget.position = new Vector3(winner.transform.position.x, lookTarget.transform.position.y, winner.transform.position.z);
		GameObject.Find("Winner").GetComponent<Image>().enabled = true;
	}

}
