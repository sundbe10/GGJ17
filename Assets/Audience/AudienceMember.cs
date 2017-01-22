using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudienceMember : MonoBehaviour {

	public GameObject particlePrefab;

	private string[] names = {"idle","applause","applause2","celebration","celebration2","celebration3"};
	Animation animation;
	GameObject lookObject;
	ParticleSystem heartEmitter;

	// Use this for initialization
	void Start () {
		animation = GetComponent<Animation>();
		var particleSim = Instantiate(particlePrefab, this.transform.position+ Vector3.up * 1.7f, Quaternion.LookRotation(Vector3.up)) as GameObject;
		particleSim.transform.parent = this.transform;
		heartEmitter = particleSim.GetComponent<ParticleSystem>();
		animation.wrapMode = WrapMode.Loop;
		lookObject = GameObject.FindGameObjectWithTag("Pole");
		Relax();
	}
	
	// Update is called once per frame
	void Update () {
		LookAtObject(lookObject);
	}

	void OnTriggerEnter(Collider collider){
		if(collider.gameObject.CompareTag("TubeLove")){
			Celebrate();
			lookObject = collider.gameObject;
		}
	}

	void OnTriggerExit(Collider collider){
		if(collider.gameObject.CompareTag("TubeLove")){
			Relax();
		}
	}

	public void Relax(){
		heartEmitter.Stop();
		string newAnimation = names[Random.Range(0,3)];
		ChangeAnimation(newAnimation);
	}

	public void Celebrate(){
		heartEmitter.Play();
		string newAnimation = names[Random.Range(3,5)];
		ChangeAnimation(newAnimation);
	}

	void ChangeAnimation(string _animation){
		animation.CrossFade(_animation);
		animation[_animation].time = Random.Range(0f,3f);
	}

	void LookAtObject(GameObject lookObject){
		Transform target = lookObject.transform;
		Vector3 targetPostition = target.position - transform.position;
		targetPostition.y = 0;
		this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(targetPostition, transform.up), Time.deltaTime);
	}
		
}
