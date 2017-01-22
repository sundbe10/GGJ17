using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {
	public int widthResolution = 32;
	public int heightResolution = 18;
	public LayerMask layerMask;

	// Use this for initialization
	void Awake () {
	}
	
	// Update is called once per frame
	void Update () {

		//if (Input.GetMouseButtonDown(0))
		//	ScoreScreenView();
	}

	void ScoreScreenView() {
		int numBlue = 0;
		int numRed = 0;
		for (int i = 0; i < widthResolution; ++i)
			for (int j = 0; j < heightResolution; ++j)
			{
				RaycastHit hit;
				Ray ray = Camera.main.ScreenPointToRay(new Vector2(i*Screen.currentResolution.width/widthResolution,
																j*Screen.currentResolution.height/heightResolution));
				
				if (Physics.Raycast(ray, out hit, 20f, layerMask))
				{
					if (hit.transform.root.gameObject.layer == 8)
						++numRed;
					else if (hit.transform.root.gameObject.layer == 9)
						++numBlue;
				}
			}
		//Debug.Log("NumRed: " + numRed);
		//Debug.Log("NumBlue: " + numBlue);
	}
		
}
