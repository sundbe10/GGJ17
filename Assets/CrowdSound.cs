using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdSound : MonoBehaviour {

	public AudioSource applauseLight;
	public AudioSource applauseHeavy;
	public AudioSource cheer;
	public ScoreManager player1;
	public ScoreManager player2;
	float totalExcitement;

	float lastFrameCount1;
	float lastFrameCount2;

	// Audio Clips
	public AudioClip[] cheers;

	// Use this for initialization
	void Start () {
		applauseLight.volume = 0.5f;
		applauseHeavy.volume = 0f;
		totalExcitement = 0;
		lastFrameCount1 = 0;
		lastFrameCount2 = 0;
	}
	
	// Update is called once per frame
	void Update () {
		float player1Count = (float)player1.audienceMembers.Count;
		float player2Count = (float)player2.audienceMembers.Count;

		float delta1 = player1Count - lastFrameCount1;
		float delta2 = player2Count - lastFrameCount2;

		if (delta1 > 7 || delta2 > 7)
		{
			if (!cheer.isPlaying)
			{
				cheer.clip = cheers[Random.Range(0,cheers.Length-1)];
				cheer.volume = Random.Range(.5f, .7f);
				cheer.Play();
			}
		}
			

		float rawExcitement = Mathf.Clamp((player1Count + player2Count)/30f, 0, 1);

		if (rawExcitement > totalExcitement)
			totalExcitement = Mathf.Lerp(totalExcitement, rawExcitement, Time.deltaTime/.5f);
		
		totalExcitement = Mathf.Lerp(totalExcitement, 0, Time.deltaTime/10f);


		applauseLight.volume = totalExcitement/2 + 0.3f;
		applauseHeavy.volume = 0.7f * Mathf.Clamp(2*(totalExcitement-0.3f), 0f, 1f);
	}
}
