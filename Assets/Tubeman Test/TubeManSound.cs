using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TubeManSound : MonoBehaviour {

	private AudioSource audioSource = null;
	private Rigidbody headRigidbody = null;
	private float headVelocity;

	// Use this for initialization
	void Awake () {
		audioSource = GetComponent<AudioSource>();
		headRigidbody = GetComponent<Rigidbody>();
		headVelocity = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		headVelocity = headRigidbody.velocity.magnitude;
		//Debug.Log(headVelocity);
		audioSource.volume = Mathf.Clamp(headVelocity/3f, 0f, 1f);
		audioSource.pitch = Mathf.Clamp(headVelocity/7f, .8f, 1.3f);
	}
}
