﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadLook : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(Camera.main.transform.position - this.transform.position), 1);
	}
}
