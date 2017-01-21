using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandScript : MonoBehaviour {

	public float horzForce = 20f;

	Rigidbody rigidBody;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody>();
		//Debug.Log(controlStick.ToString()+"_Stick_V");
	}

	// Update is called once per frame
	void Update () {
		//Debug.Log(Input.GetAxis(controlStick.ToString()+"_Stick_V"));
		rigidBody.AddForce(Vector3.up * Input.GetAxis("Right_Stick_V") * horzForce);
		rigidBody.AddForce(Vector3.right * Input.GetAxis("Right_Stick_H") * horzForce);
	}
}