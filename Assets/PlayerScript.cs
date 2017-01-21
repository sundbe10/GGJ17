using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

	public float upForce = 100f;
	public float horzForce = 20f;

	Rigidbody rigidBody;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetAxis("Vertical") > 0){
			Debug.Log("up");
			rigidBody.AddForce(Vector3.up*upForce);
		}
		rigidBody.AddForce(this.transform.right* Input.GetAxis("Horizontal") * horzForce);
	}
}
