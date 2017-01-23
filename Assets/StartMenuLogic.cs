using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuLogic : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton("Submit"))
			SceneManager.LoadScene("Scenes/Main");
	}
}
