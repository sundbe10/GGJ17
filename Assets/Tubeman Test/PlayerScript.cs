using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

	public float upForce = 100f;
	public float horzForce = 20f;
	public float armForceFactor = .05f;
	public int playerNum = 1;
	public bool isAi = false;

	private GameObject[] bodySegments;
	public bool isInflating;

	// Use this for initialization
	void Awake () {
		isInflating = false;
		bodySegments = new GameObject[transform.childCount];
		for (int i = 0; i < transform.childCount; ++i){
			bodySegments[i] = transform.GetChild(i).gameObject;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(Input.GetButton("Jump_"+playerNum) && !isInflating && !isAi){
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

		Vector3 userHForce = Vector3.right * Input.GetAxis("Left_Stick_H_"+playerNum);
		Vector3 userVForce = Vector3.forward * Input.GetAxis("Left_Stick_V_"+playerNum);

		for (int i = 0; i < segmentCount; ++i)
		{
			GameObject bodysegment = bodySegments[i];
			Rigidbody rb = bodysegment.GetComponent<Rigidbody>();

			if (rb != null)
			{
				float currentSegmentAlignment = Vector3.Dot(-bodysegment.transform.right.normalized, previousSegmentUp);
				float segmentForceFactor = (1 - currentSegmentAlignment);
				Vector3 totalForce = Vector3.up * upForce * segmentForceFactor +
						userHForce * horzForce +
						userVForce * horzForce;
				rb.AddForce(totalForce);

				//Debug.Log(i + " = " + segmentForceFactor + "  " + bodysegment.transform.up.normalized + " / " + Vector3.up);
				previousSegmentUp = bodysegment.transform.up.normalized;
			}
			else
				foreach (Rigidbody armSegment in bodysegment.GetComponentsInChildren<Rigidbody>())
				{
					armSegment.AddForce(transform.up * upForce * armForceFactor);
				}
		}
		yield return null;
	}
}
