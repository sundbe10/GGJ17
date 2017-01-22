using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

	public LayerMask layerMask;
	public float affectRadius;
	private List<GameObject> audienceMembers;
	private SphereCollider affectTrigger;
	private int audienceLayer = 10;
	private PlayerScript playerScript = null;
	private HandScript[] handscripts = null;

	// Use this for initialization
	void Awake () {
		affectRadius = 2;
		audienceMembers = new List<GameObject>();
		affectTrigger = GetComponent<SphereCollider>();
		playerScript = GetComponent<PlayerScript>();
		handscripts = GetComponentsInChildren<HandScript>();
	}
	
	// Update is called once per frame
	void Update () {
		if (playerScript != null)
			affectRadius = playerScript.laggyDelta.magnitude;
		affectTrigger.radius = affectRadius;
	}

	void OnDestory() {
		audienceMembers.Clear();
	}

	void OnDrawGizmos() {
		Gizmos.DrawWireSphere(transform.position, affectRadius);
		if (audienceMembers != null)
			foreach (GameObject obj in audienceMembers)
			{
				Gizmos.color = Color.red;
				Gizmos.DrawSphere(obj.transform.position + Vector3.up * 1.5f, .2f);
			}
	}

	void OnTriggerEnter(Collider col) {
		GameObject obj = col.gameObject;
		if (obj.layer == audienceLayer && !audienceMembers.Contains(obj))
			audienceMembers.Add(obj);
	}

	void OnTriggerExit(Collider col) {
		GameObject obj = col.gameObject;
		if (obj.layer == audienceLayer && audienceMembers.Contains(obj))
			audienceMembers.Remove(obj);
	}
}

