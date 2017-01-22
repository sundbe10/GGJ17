using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TubeManSound : MonoBehaviour {

	private AudioSource audioSource = null;
	private Rigidbody headRigidbody = null;
	private float headVelocity;
	private float lastFrameHeadVelocity;
	public GameObject baseObj = null;
	private bool isInflating;

	// Audio Sources
	private AudioSource fanLoop = null;
	private AudioSource fanStartStop = null;
	private AudioSource flap = null;

	// Audio Clips
	public AudioClip fanLoopWave = null;
	public AudioClip fanStartWave = null;
	public AudioClip fanStopWave = null;
	public AudioClip flapLoopWave = null;
	public AudioClip[] flapLightWaves = null;
	public AudioClip[] flapHardWaves = null;

	// Volumes
	public float fanLoopVolume = 1.0f;
	public float fanStartVolume = 1.0f;
	public float fanStopVolume = 1.0f;
	public float flapLoopVolume = 1.0f;


	// Use this for initialization
	void Awake () {
		InitAudioSources(); 
		isInflating = false;
		audioSource = GetComponent<AudioSource>();
		headRigidbody = GetComponent<Rigidbody>();
		headVelocity = 0f;
		lastFrameHeadVelocity = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton("Jump"))
		{
			if (!isInflating)
			{
				StopAllCoroutines();
				StartCoroutine(Inflate());
			}
		}
		else
		{
			if (isInflating)
			{
				StopAllCoroutines();
				StartCoroutine(Deflate());
			}
		}

		headVelocity = headRigidbody.velocity.magnitude;

		// Flapping Loop
		audioSource.volume = flapLoopVolume*((isInflating ? .5f : 0f) + 0.5f *Mathf.Clamp(headVelocity/4f, 0f, 1f));
		audioSource.pitch = (isInflating ? 1.2f : 1f) * Mathf.Clamp(headVelocity/7f, .7f, 1.4f);

		// Flapping OneShots
		float deltaSpeed = headVelocity - lastFrameHeadVelocity;
		Debug.Log(deltaSpeed);

		if (deltaSpeed > 2.5 && !flap.isPlaying)
		{
			int randIndex = Random.Range(0, flapLightWaves.Length-1);
			flap.clip = flapLightWaves[randIndex];
			flap.volume = (deltaSpeed/2);
			flap.pitch = Random.Range(0.85f,1.15f);
			flap.Play();
		}

		lastFrameHeadVelocity = headVelocity;
	}

	void InitAudioSources() {
		fanLoop = baseObj.AddComponent<AudioSource>();
		fanLoop.clip = fanLoopWave;
		fanLoop.playOnAwake = false;
		fanLoop.loop = true;
		fanLoop.volume = 0f;
		fanLoop.pitch = .75f;

		fanStartStop = baseObj.AddComponent<AudioSource>();
		fanStartStop.clip = fanStartWave;
		fanLoop.playOnAwake = false;
		fanStartStop.loop = false;
		fanStartStop.volume = 0f;
		fanStartStop.pitch = .75f;

		flap = baseObj.AddComponent<AudioSource>();
		flap.playOnAwake = false;
		flap.loop = false;
		flap.volume = 0f;
	}

	IEnumerator Inflate() {
		isInflating = true;

		int offset = 0;

		if (fanStartStop.isPlaying)
		{
			offset = Mathf.Max(fanStartWave.samples - fanStartStop.timeSamples, 0)/3;
			fanStartStop.Pause();
			Debug.Log(offset);
		}

		fanStartStop.clip = fanStartWave;
		fanStartStop.timeSamples = offset;
		fanStartStop.volume = fanStartVolume;
		fanStartStop.Play();

		fanLoop.volume = 0f;
		fanLoop.Play();

		float fadeTime = .25f;
		float timer = 0f;

		while (fanStartStop.timeSamples < fanStartWave.samples - fadeTime*AudioSettings.outputSampleRate)
		{
			timer += Time.deltaTime;
			yield return null;
		}
			
		while (fanLoop.volume < fanLoopVolume)
		{
			fanStartStop.volume -= fanStartVolume * Time.deltaTime / fadeTime;
			fanLoop.volume += fanLoopVolume * Time.deltaTime / fadeTime;
			yield return null;
		}
		fanStartStop.Stop();
		yield break;
	}

	IEnumerator Deflate() {
		isInflating = false;

		int offset = 0;

		if (fanStartStop.isPlaying)
		{
			offset = (fanStartWave.samples - fanStartStop.timeSamples)/3;
			fanStartStop.Pause();
			Debug.Log(offset);
		}

		fanStartStop.clip = fanStopWave;
		fanStartStop.timeSamples = offset;
		fanStartStop.volume = fanStopVolume;
		fanStartStop.Play();

		float fadeTime = .2f;

		while (fanLoop.volume > 0)
		{
			if (fanLoop.isPlaying)
				fanLoop.volume -= fanLoopVolume * Time.deltaTime / fadeTime;
			yield return null;
		}
		if (fanLoop.isPlaying)
			fanLoop.Stop();
		yield break;
	}
}
