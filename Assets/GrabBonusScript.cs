using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabBonusScript : MonoBehaviour {

	//[SerializeField]
	private bool isGrabbed;

	//[SerializeField]
	private float effectiveness;

	public float additionalRadius = 3f;
	public float decayTime = 4f;
	public float rechargeTime = 6f;

	[HideInInspector]
	public float effectiveBonusRadius;

	// Use this for initialization
	void Start () {
		effectiveness = 1.0f;
	}

	void Update() {
		if (isGrabbed)
		{
			if (effectiveness > 0f)
				effectiveness -= Time.deltaTime/decayTime;
			else
				effectiveness = 0f;
		}
		else
		{
			if (effectiveness < 1f)
				effectiveness += Time.deltaTime/rechargeTime;
			else
				effectiveness = 1f;
		}

		effectiveBonusRadius = effectiveness * additionalRadius;
	}

	public void SetGrabbed(bool grabbed)
	{
		isGrabbed = grabbed;
	}
}
