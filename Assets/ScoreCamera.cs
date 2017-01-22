using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCamera : MonoBehaviour {

	private Transform thisTransform;
	private Transform parent;
	private Camera thisCamera;
	private Camera parentCamera;

	// Use this for initialization
	void Start () {
		thisTransform = GetComponent<Transform>();
		parent = thisTransform.parent;
		thisCamera = GetComponent<Camera>();
		parentCamera = parent.gameObject.GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		thisTransform.position = parent.position;
		thisTransform.rotation = parent.rotation;
		thisCamera.aspect = parentCamera.aspect;
		thisCamera.fieldOfView = parentCamera.fieldOfView;
	}
}
