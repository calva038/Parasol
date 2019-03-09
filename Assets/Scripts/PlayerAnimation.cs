using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {

	//private bool skippableAnimation = true;
	private bool repeat = false;
	private float dirMultiplier = 1;	// Player initially faces right

	[SerializeField] private Animator anim;
	[SerializeField] protected CharacterPhysics2D characterPhysics;

	private void Update () {
		float xInput = Input.GetAxisRaw ("Horizontal");
		anim.SetBool ("Ground", characterPhysics.IsGrounded);
		anim.SetFloat ("Speed", Mathf.Abs (xInput)); //abs is used incase we move in the opposite direction so -1 = 1
		anim.SetBool ("MoveButtons", xInput != 0);
		if (Input.GetKeyDown(KeyCode.Space)) {
			anim.SetTrigger ("Jump");
		}

		// Determine which way the player should face (left/right?)
		Vector3 s = transform.localScale;
		if(xInput > 0) {
			dirMultiplier = 1;
		}
		else if(xInput < 0) {
			dirMultiplier = -1;
		}
		transform.localScale = new Vector3( dirMultiplier * Mathf.Abs(s.x), s.y, s.z);
	}

	private void PokeRepeatCheck () {
		if (Input.GetKeyDown (KeyCode.F)) {
			repeat = true;
		}
	}

	private void PokeRepeat () {
		if (Input.GetKeyDown (KeyCode.F)) {
			repeat = true;
		}
		if (repeat) {
			anim.Play ("poke_repeat");
			repeat = false;
		}
	}
}