using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandScript : MonoBehaviour {

	public enum Hand {LEFT, RIGHT};
	public Hand hand;
	public float horzForce = 20f;
	public bool isAttached = false;

	int playerNum;
	bool isAi;
	LayerMask selfLayer;
	Rigidbody rigidBody;

	// Use this for initialization
	void Awake () {
		rigidBody = GetComponent<Rigidbody>();
		selfLayer = this.gameObject.layer;
		playerNum = GetComponentsInParent<PlayerScript>()[0].playerNum;
		isAi = GetComponentsInParent<PlayerScript>()[0].isAi;
		//Debug.Log(controlStick.ToString()+"_Stick_V");
	}

	// Update is called once per frame
	void Update () {
		if(!isAi){
			MoveHands();
		}
	}

	void OnTriggerStay (Collider col) {
		if (col.gameObject.layer == selfLayer.value || isAi)
			return;
		if ((hand == HandScript.Hand.LEFT && Input.GetAxis("Fire1_"+playerNum) > 0) ||
			(hand == HandScript.Hand.RIGHT && Input.GetAxis("Fire2_"+playerNum) > 0))
		{
			if (GetComponents<CharacterJoint>().Length == 1)
			{
				CharacterJoint characterJoint = gameObject.AddComponent<CharacterJoint>();
				characterJoint.anchor = new Vector3(0,0,0);
				characterJoint.connectedBody = col.GetComponent<Rigidbody>();

				SoftJointLimit lowLimit = new SoftJointLimit();
				lowLimit.limit = -90;
				characterJoint.lowTwistLimit = lowLimit;

				SoftJointLimit highLimit = new SoftJointLimit();
				highLimit.limit = 90;
				characterJoint.highTwistLimit = highLimit;

			}
			isAttached = true;
		}
	}

	void MoveHands(){
		rigidBody.AddForce(Vector3.up * Input.GetAxis("Right_Stick_V_"+playerNum) * horzForce);
		rigidBody.AddForce(Vector3.right * Input.GetAxis("Right_Stick_H_"+playerNum) * horzForce);

		if ((hand == HandScript.Hand.LEFT && Input.GetAxis("Fire1_"+playerNum) < 0) ||
			(hand == HandScript.Hand.RIGHT && Input.GetAxis("Fire2_"+playerNum) < 0))
		{
			CharacterJoint[] joints = GetComponents<CharacterJoint>();
			isAttached = false;
			for (int i = joints.Length-1; i > 0; --i)
				DestroyImmediate(joints[i]);
		}
	}
}