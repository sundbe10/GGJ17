using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandScript : MonoBehaviour {

	public enum Hand {LEFT, RIGHT};
	public Hand hand;
	public float horzForce = 20f;
	public bool isAttached = false;
	public GameObject grabbedObject = null;
	private ScoreManager scoreManager=  null;

	int playerNum;
	bool isAi;
	int selfLayer;
	Rigidbody rigidBody;

	// Use this for initialization
	void Awake () {
		rigidBody = GetComponent<Rigidbody>();
		selfLayer = this.gameObject.layer;
		playerNum = GetComponentInParent<PlayerScript>().playerNum;
		isAi = GetComponentInParent<PlayerScript>().isAi;
		scoreManager = GetComponentInParent<ScoreManager>();
		//Debug.Log(controlStick.ToString()+"_Stick_V");
	}

	// Update is called once per frame
	void Update () {
		if(!isAi){
			MoveHands();
		}
	}

	void OnTriggerStay (Collider col) {
		if (col.gameObject.layer == selfLayer || col.gameObject.layer == 11 || isAi)
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
			grabbedObject = col.gameObject;
			GrabBonusScript grabBonus = grabbedObject.GetComponent<GrabBonusScript>();
			if (grabBonus != null && !isAttached)
			{
				grabBonus.SetGrabbed(true);
				scoreManager.AddBonusObject(grabBonus);
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
			if (grabbedObject != null)
			{
				GrabBonusScript grabBonus = grabbedObject.GetComponent<GrabBonusScript>();
				if (grabBonus != null && isAttached)
				{
					grabBonus.SetGrabbed(false);
					scoreManager.RemoveBonusObject(grabBonus);
				}
				isAttached = false;
				grabbedObject = null;
				for (int i = joints.Length-1; i > 0; --i)
					DestroyImmediate(joints[i]);
			}
		}
	}
}