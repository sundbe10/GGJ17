using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandScript : MonoBehaviour {

	public float horzForce = 20f;

	Rigidbody rigidBody;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update () {
		rigidBody.AddForce(this.transform.up * Input.GetAxis("Vertical") * horzForce);
		rigidBody.AddForce(this.transform.forward * Input.GetAxis("Horizontal") * horzForce);
	}
}