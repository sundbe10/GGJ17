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
	public GameObject baseObject = null;
	public bool isInflating;

	private Vector3[] currentVelocities;
	private Vector3[] previousVelocities;
	private Vector3[] deltaVelocities;
	private Vector3 summedDeltas;
	public Vector3 laggyDelta;
	public float attackTime = .5f;
	public float releaseTime = 2f;


	// Use this for initialization
	void Awake () {
		isInflating = false;
		bodySegments = new GameObject[transform.childCount];
		for (int i = 0; i < transform.childCount; ++i){
			bodySegments[i] = transform.GetChild(i).gameObject;
		}
		baseObject = bodySegments[0];
		currentVelocities = new Vector3[bodySegments.Length];
		previousVelocities = new Vector3[bodySegments.Length];
		deltaVelocities = new Vector3[bodySegments.Length];

		laggyDelta = Vector3.zero;
	}

	void Start(){
		if(isAi) AIMove();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        for (int i = 0; i < 20; i++)
        {
            if (Input.GetKeyDown("joystick 1 button " + i))
            {
                print("joystick 1 button " + i);
            }
        }
        if (!isAi){
			if(Input.GetButton("Jump_"+playerNum)){
                Debug.Log("test");
				isInflating = true;
			}
			else{
				isInflating = false;
			}
		}

		UpdateCharacter();

		summedDeltas = Vector3.zero;
		foreach (Vector3 v in deltaVelocities)
		{
			summedDeltas += v;
		}

		laggyDelta += summedDeltas*Time.deltaTime/attackTime;
		laggyDelta -= laggyDelta*Time.deltaTime/releaseTime;

	}

	void UpdateCharacter() {
		int segmentCount = bodySegments.Length;
		Vector3 previousSegmentUp = Vector3.up;

		Vector3 userHForce = isAi ? Vector3.zero : Vector3.right * Input.GetAxis("Left_Stick_H_"+playerNum);
		Vector3 userVForce = isAi ? Vector3.zero : Vector3.forward * Input.GetAxis("Left_Stick_V_"+playerNum);

		for (int i = 0; i < segmentCount; ++i)
		{
			GameObject bodysegment = bodySegments[i];
			Rigidbody rb = bodysegment.GetComponent<Rigidbody>();


			if (rb != null)
			{
				previousVelocities[i] = currentVelocities[i];
				currentVelocities[i] = rb.velocity;
				deltaVelocities[i] = currentVelocities[i] - previousVelocities[i];

				// Apply Inflation Physics
				if (isInflating)
				{
					float currentSegmentAlignment = Vector3.Dot(-bodysegment.transform.right.normalized, previousSegmentUp);
					float segmentForceFactor = (1 - currentSegmentAlignment);
					Vector3 totalForce = Vector3.up * upForce * segmentForceFactor +
						userHForce * horzForce +
						userVForce * horzForce;
					rb.AddForce(totalForce);

					previousSegmentUp = bodysegment.transform.up.normalized;
				}
			}
			else
			{
				Vector3 armVelocity = Vector3.zero;
				Rigidbody[] armSegments = bodysegment.GetComponentsInChildren<Rigidbody>();
				for (int j = 0; j < armSegments.Length; ++j)
				{
					Rigidbody armSegment = armSegments[j];
					armVelocity += armSegment.velocity;

					// Apply Inflation Physics
					if (isInflating)
						armSegment.AddForce(transform.up * upForce * armForceFactor);
				}
				previousVelocities[i] = currentVelocities[i];
				currentVelocities[i] = armVelocity;
				deltaVelocities[i] = currentVelocities[i] - previousVelocities[i];
			}
		}
	}

	public void SetAI(){
		isAi = true;
		AIMove();
	}

	void AIMove(){
		isInflating = true;
		Invoke("AIRest", Random.Range(0.1f,2));
	}

	void AIRest(){
		isInflating = false;
		Invoke("AIMove", Random.Range(0.1f,1));
	}

//	void OnDrawGizmos() {
//		if (deltaVelocities != null)
//		{
//			Gizmos.color = Color.blue;
//			for (int i = 0; i < deltaVelocities.Length; ++i)
//			{
//				Gizmos.DrawLine(transform.position, transform.position+deltaVelocities[i]);
//			}
//
//			Gizmos.color = Color.green;
//			Gizmos.DrawLine(transform.position, transform.position+laggyDelta);
//		}
//	}
}
