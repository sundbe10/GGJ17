using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

	public float upForce = 100f;
	public float horzForce = 20f;
	public float armForceFactor = .05f;

	private GameObject[] bodySegments;
	private bool isInflating;

	// Use this for initialization
	void Awake () {
		isInflating = false;
		Debug.Log(transform.childCount);
		bodySegments = new GameObject[transform.childCount];
		for (int i = 0; i < transform.childCount; ++i){
			bodySegments[i] = transform.GetChild(i).gameObject;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(Input.GetButton("Jump") && !isInflating){
			StartCoroutine(Inflate());
		}
		else
		{
			StopAllCoroutines();
			isInflating = false;
		}
	}

	IEnumerator Inflate() {
		isInflating = true;
		int segmentCount = bodySegments.Length;
		Vector3 previousSegmentUp = Vector3.up;

		for (int i = 0; i < segmentCount; ++i)
		{
			GameObject bodysegment = bodySegments[i];
			Rigidbody rb = bodysegment.GetComponent<Rigidbody>();

			if (rb != null)
			{
				float currentSegmentAlignment = Vector3.Dot(-bodysegment.transform.right.normalized, previousSegmentUp);
				float segmentForceFactor = (1 - currentSegmentAlignment);
				Vector3 totalForce = Vector3.up * upForce * segmentForceFactor +
									Vector3.forward * Input.GetAxis("Left_Stick_V") * horzForce +
									Vector3.right * Input.GetAxis("Left_Stick_H") * horzForce;
				rb.AddForce(totalForce);

				//Debug.Log(i + " = " + segmentForceFactor + "  " + bodysegment.transform.up.normalized + " / " + Vector3.up);
				previousSegmentUp = bodysegment.transform.up.normalized;
			}
			else if(bodysegment.tag == "LeftArm")
				foreach (Rigidbody armSegment in bodysegment.GetComponentsInChildren<Rigidbody>())
				{
					float currentSegmentAlignment = Vector3.Dot(armSegment.transform.up.normalized, previousSegmentUp);
					float segmentForceFactor = (1 - currentSegmentAlignment);
					armSegment.AddForce(-transform.right * upForce * armForceFactor * segmentForceFactor);
				}
			else if(bodysegment.tag == "RightArm")
				foreach (Rigidbody armSegment in bodysegment.GetComponentsInChildren<Rigidbody>())
				{
					float currentSegmentAlignment = Vector3.Dot(armSegment.transform.up.normalized, previousSegmentUp);
					float segmentForceFactor = (1 - currentSegmentAlignment);
					armSegment.AddForce(transform.right * upForce * armForceFactor * segmentForceFactor);
				}
		}
		yield return null;
	}
}
