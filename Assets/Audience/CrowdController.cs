using UnityEngine;
using System.Collections;

public class CrowdController : MonoBehaviour {

	public int numberOfPeople;
	public GameObject[] audiencePrefabs;

	GameObject[] audienceMembers;

	// Use this for initialization
	void Start () {
		MakeAudience();
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