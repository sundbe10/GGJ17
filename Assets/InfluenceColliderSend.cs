using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfluenceColliderSend : MonoBehaviour {

	private ScoreManager scoreManager = null;

	void Awake () {
		scoreManager = GetComponentInParent<ScoreManager>();
	}
	
	void OnTriggerEnter (Collider col)
	{
		scoreManager.OnObjectEnter(col);
	}

	void OnTriggerExit (Collider col)
	{
		scoreManager.OnObjectExit(col);
	}
}
