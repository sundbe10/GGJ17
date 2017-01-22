using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

	public float affectRadius;
	private List<GameObject> audienceMembers;
	private SphereCollider affectTrigger;
	private int audienceLayer = 10;
	private PlayerScript playerScript = null;
	private Transform baseTransform = null;
	private List<GrabBonusScript> bonuses;

	// Use this for initialization
	void Start () {
		affectRadius = 2;
		audienceMembers = new List<GameObject>();
		affectTrigger = GetComponentInChildren<SphereCollider>();
		playerScript = GetComponent<PlayerScript>();
		baseTransform = playerScript.baseObject.transform;
		bonuses = new List<GrabBonusScript>();
	}
	
	// Update is called once per frame
	void Update () {
		affectRadius -= Time.deltaTime;
		affectRadius = Mathf.Max(affectRadius, playerScript.laggyDelta.magnitude);

		float bonusRadius = 0f;
		// Bonus Objects
		foreach (GrabBonusScript bonus in bonuses)
		{
			bonusRadius += bonus.effectiveBonusRadius;
		}

		affectTrigger.radius = Mathf.Lerp(affectTrigger.radius, bonusRadius + affectRadius, Time.deltaTime);
	}

	void OnDestory() {
		audienceMembers.Clear();
	}

	void OnDrawGizmos() {
		if (baseTransform != null)
			Gizmos.DrawWireSphere(baseTransform.position, affectTrigger.radius);
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

	public void AddBonusObject(GrabBonusScript bonus)
	{
		bonuses.Add(bonus);
	}

	public void RemoveBonusObject(GrabBonusScript bonus)
	{
		bonuses.Remove(bonus);
	}
}

