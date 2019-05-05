using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterPhysics2D))]
public class CharacterAnimation : MonoBehaviour {

	protected bool facingRight = true;

	[SerializeField] protected CharacterPhysics2D characterPhysics;
	
	protected virtual void Update(){
		if(characterPhysics.Velocity.x > 0 != facingRight){
			Flip();
		}
	}

	protected void Flip(){
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= - 1;
		transform.localScale = theScale;
	}
}

