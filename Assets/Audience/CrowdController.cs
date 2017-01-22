﻿using UnityEngine;
using System.Collections;

public class CrowdController : MonoBehaviour {

	public int numberOfPeople;
	public GameObject[] audiencePrefabs;

	private string[] names = {"idle","applause","applause2","celebration","celebration2","celebration3"};
	private GameObject[] audienceMembers;

	// Use this for initialization
	void Start () {
		MakeAudience();
		Animation[] AudienceMembers = gameObject.GetComponentsInChildren<Animation>();
		foreach(Animation anim in AudienceMembers){
			string thisAnimation = names[Random.Range(0,5)];

			anim.wrapMode = WrapMode.Loop;
			anim.CrossFade(thisAnimation);
			anim[thisAnimation].time = Random.Range(0f,3f);
		}
	}

	void MakeAudience(){
		audienceMembers = new GameObject[numberOfPeople];
		int audienceCounter = 0;

		GameObject groundObject = GameObject.FindGameObjectWithTag("ClubGround");
		Vector3 groundBounds = groundObject.GetComponent<MeshRenderer>().bounds.size;

		while(audienceCounter < numberOfPeople){
			var newPosition = new Vector3(
				groundObject.transform.position.x + Random.Range(-groundBounds.x/2, groundBounds.x/2),
				0,
				groundObject.transform.position.z + Random.Range(-groundBounds.z/2, groundBounds.z/2)
			);
			if(PositionIsValid(newPosition)){
				GameObject newAudienceMember = Instantiate(audiencePrefabs[Random.Range(0, audiencePrefabs.Length)],newPosition,Quaternion.identity) as GameObject;
				newAudienceMember.transform.parent = transform;	
				audienceMembers[audienceCounter] = newAudienceMember;
				audienceCounter++;
			}
		}			
	}

	bool PositionIsValid(Vector3 positionVector){
		RaycastHit hit;
		if (Physics.Raycast(positionVector+Vector3.up*3, -Vector3.up, out hit)){
			if(hit.collider.gameObject.tag == "ClubGround"){
				return true;
			}
			return false;
		}
		return false;
	}
}