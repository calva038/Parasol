using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : CharacterAnimation {

	private bool skippableAnimation = true;
	private bool repeat = false;

	[SerializeField] private Animator anim;

	protected override void Update () {
		base.Update ();
		float xInput = Input.GetAxisRaw ("Horizontal");
		anim.SetBool ("Ground", characterPhysics.IsGrounded);
		anim.SetFloat ("Speed", Mathf.Abs (xInput)); //abs is used incase we move in the opposite direction so -1 = 1
		anim.SetBool ("MoveButtons", xInput != 0);
		if (Input.GetKeyDown(KeyCode.Space)) {
			anim.SetTrigger ("Jump");
		}

	}

	private void pokeRepeatCheck () {
		if (Input.GetKeyDown (KeyCode.F)) {
			repeat = true;
		}
	}

	private void pokeRepeat () {
		if (Input.GetKeyDown (KeyCode.F)) {
			repeat = true;
		}
		if (repeat) {
			anim.Play ("poke_repeat");
			repeat = false;
		}
	}
}