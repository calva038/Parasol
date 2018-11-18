using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller2D))]
public class PlayerMovement : MonoBehaviour {

	public float gravity = -20;
	public float moveSpeed = 10;
	public float jumpVelocity = 10;
	public float xVelocitySmoothTime = 0.5f;
	private Vector2 velocity;
	private float targetVelX = 0; 
	private float velXSmoothing = 0;

	private Controller2D controller;

	void Start() {
		controller = GetComponent<Controller2D>();
	}

	void Update() {

		// Get input
		float xInput = Input.GetAxisRaw("Horizontal");
		float yInput = Input.GetAxisRaw("Vertical");
		bool jumpPressed = Input.GetKeyDown(KeyCode.Space);
		Vector2 input = new Vector2(xInput, yInput);


		// Calculate movement physics
		Controller2D.CollisionInfo col = controller.collisions;
		if((col.above) || (col.below)) {
			velocity.y = 0;
		}
		if(jumpPressed && col.below) {
			velocity.y = jumpVelocity;
		}
		targetVelX = input.x * moveSpeed;
		if(input.x == 0) {
			velocity.x = Mathf.SmoothDamp(velocity.x, targetVelX, ref velXSmoothing, xVelocitySmoothTime);
		}
		else
		{
			velocity.x = targetVelX;
		}
		velocity.y += gravity * Time.deltaTime;
		controller.Move(velocity * Time.deltaTime);
	}
	
	public bool isGrounded() {
		return controller.collisions.below;
	}
}
