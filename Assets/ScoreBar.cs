using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBar : MonoBehaviour {

	public GameObject player;

	ScoreManager scoreManager;
	RectTransform scoreBar;

	// Use this for initialization
	void Start () {
		scoreManager = player.GetComponentInChildren<ScoreManager>();
		scoreBar = transform.Find("Score Bar").GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
		if(GameManager.gameActive){
			scoreBar.sizeDelta = new Vector2(200*scoreManager.score/GameManager.score,10);
		}
	}
}
