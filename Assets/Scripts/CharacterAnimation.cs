using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour {

	public float maxSpeed = 5f;
	bool facingRight = true;
	bool skippableAnimation = true;

	Animator anim;
	PlayerMovement player;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		player = GetComponent<PlayerMovement>();
	}
	
	void FixedUpdate(){

		anim.SetBool("Ground", player.isGrounded());


		float move = Input.GetAxisRaw("Horizontal");

		anim.SetFloat("Speed", Mathf.Abs(move));  //abs is used incase we move in the opposite direction so -1 = 1
		anim.SetBool("MoveButtons", move != 0);

		if(move > 0 &&!facingRight){
			Flip();
		}
		else if(move < 0 && facingRight){
			Flip();
		}
	}

	void Flip(){
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= - 1;
		transform.localScale = theScale;
	}
}

