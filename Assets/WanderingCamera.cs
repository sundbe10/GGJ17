using UnityEngine;
using System.Collections;

public class WanderingCamera : MonoBehaviour {

	public GameObject[] CameraMovePoints;
	public GameObject CameraObject;
	public float CameraMoveSpeed;
	public GameObject CamtargetObject;

	private int lastCamMPIndex = -1;
	private int currentCamMPIndex = -1;

	void Start () {
		if (CameraMovePoints.Length == 0) { Debug.Log("Not enough Camera Move Points to make cam-wandering");  }
		if (CameraObject == null) Destroy(this);
	}

	void Update()
	{
		if (currentCamMPIndex < 0 || currentCamMPIndex == CameraMovePoints.Length) { currentCamMPIndex = 0; }
		else
		{
			
			if (CameraObject.transform.position != CameraMovePoints[currentCamMPIndex].transform.position)
			{
				Vector3 dist = CameraMovePoints[currentCamMPIndex].transform.position - CameraObject.transform.position;
				Vector3 maxDist = new Vector3(.1f,.1f,.1f);
				if (dist.magnitude > maxDist.magnitude)
				{
					Debug.Log("test");
					CameraObject.transform.position = Vector3.Lerp(CameraObject.transform.position, 
						CameraMovePoints[currentCamMPIndex].transform.position, 
						Time.deltaTime * CameraMoveSpeed / dist.magnitude);

					CameraObject.transform.LookAt(CamtargetObject.transform);
				}
				else { NextCamIndex(); }
			}
			else { NextCamIndex(); }

		}
	}

	void NextCamIndex()
	{
		
		currentCamMPIndex++;
		Debug.Log(currentCamMPIndex);
	}
}