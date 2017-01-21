using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudienceScript : MonoBehaviour {

	Animator animator;

	// Use this for initialization
	void Awake (){
		animator = GetComponent<Animator>();
		if(Random.Range(0f, 1f) > 0.5){
			animator.SetBool("cheer", true);
			animator.CrossFade("cheer",0);
		}
	}

	void Start () {
		AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
		animator.Play(state.fullPathHash, -1, Random.Range(0f, 1f));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
