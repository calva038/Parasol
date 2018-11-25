using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character {

	[Header ("Movement")]
	[SerializeField] private float gravityScale = 1;
	[SerializeField] private float moveSpeed = 10;
	[SerializeField] private float jumpVelocity = 10;
	[SerializeField] private float xVelocitySmoothTime = 0.5f;

	[Header ("Attacking")]
	[SerializeField] private float attackDuration;

	[Header("Stats")]
	public int curHP;
	public int maxHP;

	private AttackState attackState = AttackState.NOT_ATTACKING;
	private float attackTimer = 0;

	private Vector2 velocity;
	private float targetVelX = 0;
	private float velXSmoothing = 0;

	public bool isGrounded {
		get {
			return physics.Collisions.below;
		}
	}

	public float GravityScale {
		get {
			return gravityScale;
		}

		set {
			gravityScale = value;
		}
	}

	public float MoveSpeed {
		get {
			return moveSpeed;
		}

		set {
			moveSpeed = Mathf.Abs (value);
		}
	}

	public float JumpVelocity {
		get {
			return jumpVelocity;
		}

		set {
			jumpVelocity = Mathf.Abs (value);
		}
	}

	public float XVelocitySmoothTime {
		get {
			return xVelocitySmoothTime;
		}

		set {
			xVelocitySmoothTime = Mathf.Clamp (value, 0, float.MaxValue);
		}
	}

	public float Duration {
		get {
			return attackDuration;
		}

		set {
			attackDuration = value;
		}
	}

	private enum AttackState {
		ATTACKING,
		NOT_ATTACKING
	}

	void OnValidate () {
		moveSpeed = Mathf.Abs (moveSpeed);
		jumpVelocity = Mathf.Abs (jumpVelocity);
		xVelocitySmoothTime = Mathf.Clamp (xVelocitySmoothTime, 0, float.MaxValue);
	}

	void Update () {

		// Player input
		float xInput = Input.GetAxisRaw ("Horizontal");
		float yInput = Input.GetAxisRaw ("Vertical");
		bool jumpPressed = Input.GetKeyDown (KeyCode.Space);
		Vector2 input = new Vector2 (xInput, yInput);

		// Collision info
		CharacterPhysics2D.CollisionInfo col = physics.Collisions;

		// 0 vertical velocity when character is squeezed between a floor and ceiling
		if ((col.above) || (col.below)) {
			velocity.y = 0;
		}

		// Jumping
		if (jumpPressed && col.below) {
			velocity.y = JumpVelocity;
			anim.SetTrigger ("Jump");
		}

		// Horizontal velocity smoothing (if no player input is detected)
		targetVelX = input.x * MoveSpeed;
		if (input.x == 0) {
			velocity.x = Mathf.SmoothDamp (velocity.x, targetVelX, ref velXSmoothing, XVelocitySmoothTime);
		} else {
			velocity.x = targetVelX;
		}

		// Gravity
		velocity += gravityScale * Physics2D.gravity * Time.deltaTime;

		// Move the character
		physics.Velocity = velocity;

		// Attack logic
		switch (attackState) {
			case (AttackState.ATTACKING):
				if (attackTimer > 0) {
					attackTimer -= Time.deltaTime;
				} else {
					attackTrigger.enabled = false;
					attackState = AttackState.NOT_ATTACKING;
				}
				break;

			case (AttackState.NOT_ATTACKING):
				if (Input.GetKeyDown (KeyCode.F)) {
					attackTimer = attackDuration;
					attackTrigger.enabled = true;
					attackState = AttackState.ATTACKING;
				}
				break;

			default:
				break;
		}

		// Attack animation
		anim.SetBool ("Attacking", attackState == AttackState.ATTACKING);
	}

}