using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceAI : MonoBehaviour {

	//Floats
	public float patrolMinX;
	public float patrolMaxX;
	public float distance;
	public float wakeRange;
	public float hitInterval;
	public float hitTriggerDuration;
	public float patrolSpeed;
	public float chaseSpeed;
	public float startAttackingDistance;
	public float stopChaseDistance;
	private float hitTriggerTimer;
	private float hitTimer;
	
	public float gravity = -20;

	//Booleans
	public bool awake = false;

	//References
	public Transform target;
	public Animator anim;
	public Collider2D attackTrigger;

	private CharacterPhysics2D controller;
	private bool facingRight = true;
	private bool movingRight = true;
	private Vector2 velocity;

	public enum AIState {
		Patrolling, // Just walking
		Chasing, // Chasing the player
		Attacking, // Attacking the player
	}

	public enum AttackState {
		Attack,
		Waiting
	}

	// Decision FSM state register
	public AIState currentAIState = AIState.Patrolling;

	// Attacking FSM state register
	public AttackState currentAttackState = AttackState.Waiting;

	void Awake () {
		anim = gameObject.GetComponent<Animator> ();
		controller = gameObject.GetComponent<CharacterPhysics2D> ();
	}

	void Update () {
		// Movement
		velocity.y += gravity * Time.deltaTime;

		DecisionFSM ();
		controller.Velocity = velocity;

	}

	void DecisionFSM () {

		distance = Vector3.Distance (transform.position, target.transform.position);
		float xDist = Mathf.Abs(target.position.x - transform.position.x);

		switch (currentAIState) {
			case AIState.Patrolling:
				// Movement
				velocity.x = movingRight ? patrolSpeed : -patrolSpeed;
				anim.SetBool("Walking", true);

				// Flip movement direction at walls
				CharacterPhysics2D.CollisionInfo col = controller.Collisions;

				if(col.below) {
					velocity.y = 0;
				}

				if ((col.right || transform.position.x > patrolMaxX) && movingRight) {
					movingRight = false;
				} else if ((col.left || transform.position.x < patrolMinX) && !movingRight) {
					movingRight = true;
				}
				FaceMovementDir ();

				// Next state
				if (distance < wakeRange) {
					currentAIState = AIState.Chasing;
				}
				break;

			case AIState.Chasing:
				
				// Chase player as long as player is in range
				if (ExclusiveBetween (xDist, 0.1f, wakeRange)) {
					velocity.x = (transform.position.x < target.position.x) ? chaseSpeed : -chaseSpeed;
					anim.SetBool("Walking", true);
				}
				else {
					velocity.x = 0;
					anim.SetBool("Walking", false);
				}
				FaceMovementDir ();

				// Next state
				if (xDist >= wakeRange) {
					currentAIState = AIState.Patrolling;
				} else if (xDist < startAttackingDistance) {
					currentAIState = AIState.Attacking;
				}
				break;

			case AIState.Attacking:
				velocity.x = 0;
				anim.SetBool("Walking", false);

				// Attack the player
				AttackFSM ();
				FaceTarget ();

				// Next state
				if (distance >= stopChaseDistance) {
					currentAIState = AIState.Chasing;
				}
				break;

			default:
				currentAIState = AIState.Patrolling;
				break;
		}
	}

	void RangeCheck () {
		distance = Vector3.Distance (transform.position, target.transform.position);
		awake = (distance < wakeRange);
	}

	void Flip () {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void FaceMovementDir () {
		if ((velocity.x > 0 && !facingRight) ||
			(velocity.x < 0 && facingRight)) {
			Flip ();
		}
	}

	void FaceTarget () {
		if ((target.position.x > transform.position.x && !facingRight) ||
			(target.position.x < transform.position.x && facingRight)) {
			Flip ();
		}
	}

	bool ExclusiveBetween (float x, float min, float max) {
		return x > min && x < max;
	}

	public void AttackFSM () {

		switch (currentAttackState) {
			case AttackState.Attack:
				
				attackTrigger.enabled = true;
				hitTriggerTimer += Time.deltaTime;

				// Next state
				if(hitTriggerTimer > hitTriggerDuration) {
					hitTimer = 0;
					currentAttackState = AttackState.Waiting;
				}
				break;

			case AttackState.Waiting:
				attackTrigger.enabled = false;
				hitTimer += Time.deltaTime;

				// Next state
				if(hitTimer > hitInterval) {
					hitTriggerTimer = 0;
					anim.SetTrigger("Attack");
					currentAttackState = AttackState.Attack;
				}
				break;

			default:
				currentAttackState = AttackState.Waiting;
				break;
		}
	}
}