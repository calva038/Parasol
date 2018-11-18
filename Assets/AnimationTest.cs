using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTest : MonoBehaviour {

	public Animator animTest;
	// Use this for initialization
	void Start () {
		animTest = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)){
			animTest.SetTrigger("Testtrigger");

		}
		if(Input.GetKeyDown(KeyCode.Space)){
			animTest.SetFloat("TestFloat", 10);
		}
	}
}
