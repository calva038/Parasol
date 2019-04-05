using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {

	[SerializeField] private CharacterPhysics2D physics;
	[SerializeField] private float gravityScale = 1;
	[SerializeField] private float moveSpeed = 10;
	[SerializeField] private float jumpVelocity = 10;
	[SerializeField] private float xVelocitySmoothTime = 0.5f;

	[Header ("Audio")]
	[SerializeField] private new AudioSource audio;
	[SerializeField] private List<AudioClip> jumpSounds;
	[SerializeField] private AudioSource walkingAudio;

	public Vector2 DirectionalInput { get; set; }

	public CharacterPhysics2D Physics {
		get { return physics; }
		set { physics = value; }
	}

	public Vector2 velocity;
    public Vector2 velocityHolder;
	private float targetVelX = 0;
	private float velXSmoothing = 0;

	private bool jumpActivated;
	private float xInput;
	private float yInput;

    public float knockback;
    public float knockbackLength;
    public static double isDamaged;
    public static double knockbackCount;
    public static bool knockFromRight;

    public GameObject hubWorld;
    public bool isHub;

    private void Update () {
        

        // Collision info
        CharacterPhysics2D.CollisionInfo col = Physics.Collisions;

		// 0 vertical velocity when character is squeezed between a floor and ceiling
		if ((col.above) || (col.below)) {
			velocity.y = 0;
		}

		// Jumping
		if (jumpActivated && col.below) {
			velocity.y = jumpVelocity;

			// Jumping sound
			if (jumpSounds.Count > 0) {
				audio.PlayOneShot (jumpSounds[Random.Range (0, jumpSounds.Count)]);
			}
		}

		Vector2 input = DirectionalInput;
        if (isDamaged > 0)
        {
            isDamaged -= Time.deltaTime;
        }
        if (knockbackCount <= 0)
        {
            // Walking sound
            if (input.x != 0 && col.below) {
		    	if (!walkingAudio.isPlaying) {
			    	walkingAudio.Play ();
		    	}
	    	}
            else {
			    walkingAudio.Stop ();
	    	}

            // Horizontal velocity smoothing (if no player input is detected)
            targetVelX = input.x * moveSpeed;
            if (input.x == 0)
            {
                velocity.x = Mathf.SmoothDamp(velocity.x, targetVelX, ref velXSmoothing, xVelocitySmoothTime);
            }
            else
            {
                velocity.x = targetVelX;
            }
        }
        else
        {
            if (knockFromRight)
            {
                velocity.x = -knockback;
                velocity.y = knockback/2;
            }
            else
            {
                velocity.x = knockback;
                velocity.y = knockback/2;
            }
            knockbackCount -= Time.deltaTime;
        }
		// Gravity
		velocity += gravityScale * Physics2D.gravity * Time.deltaTime;

		// Move the character
        if (isHub)
        {
            hubWorld.transform.Rotate(0, -velocity.x/100, 0);
            velocityHolder.x = 0;
            velocityHolder.y = velocity.y/2;
            Physics.Velocity = velocityHolder;
        }
        else
        {
            Physics.Velocity = velocity;
        }

		// Clear flags
		jumpActivated = false;
	}

	public void Jump () {
		jumpActivated = true;
	}
}