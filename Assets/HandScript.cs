using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandScript : MonoBehaviour {

	public enum Hand {LEFT, RIGHT};
	public Hand hand;
	public float horzForce = 20f;

	Rigidbody rigidBody;

	// Use this for initialization
	void Awake () {
		rigidBody = GetComponent<Rigidbody>();
		//Debug.Log(controlStick.ToString()+"_Stick_V");
	}

	// Update is called once per frame
	void Update () {
		//Debug.Log(Input.GetAxis(controlStick.ToString()+"_Stick_V"));
		rigidBody.AddForce(Vector3.up * Input.GetAxis("Right_Stick_V") * horzForce);
		rigidBody.AddForce(Vector3.right * Input.GetAxis("Right_Stick_H") * horzForce);

		if ((hand == HandScript.Hand.LEFT && Input.GetAxis("Fire1") < 0) ||
			(hand == HandScript.Hand.RIGHT && Input.GetAxis("Fire2") < 0))
		{
			CharacterJoint[] joints = GetComponents<CharacterJoint>();
			for (int i = joints.Length-1; i > 0; --i)
				DestroyImmediate(joints[i]);
		}
	}

	void OnTriggerStay (Collider col) {
		if (col.gameObject.layer == 8)
			return;
		if ((hand == HandScript.Hand.LEFT && Input.GetAxis("Fire1") > 0) ||
			(hand == HandScript.Hand.RIGHT && Input.GetAxis("Fire2") > 0))
		{
			if (GetComponents<CharacterJoint>().Length == 1)
			{
				CharacterJoint characterJoint = gameObject.AddComponent<CharacterJoint>();
				characterJoint.anchor = new Vector3(0,1,0);
				characterJoint.connectedBody = col.GetComponent<Rigidbody>();

				SoftJointLimit lowLimit = new SoftJointLimit();
				lowLimit.limit = -90;
				characterJoint.lowTwistLimit = lowLimit;

				SoftJointLimit highLimit = new SoftJointLimit();
				highLimit.limit = 90;
				characterJoint.highTwistLimit = highLimit;

			}
		}
	}
}