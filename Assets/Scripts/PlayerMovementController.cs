using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour {

	[SerializeField] private CharacterMovement movement;
	[SerializeField] private string horizontalInputAxis = "Horizontal";
	[SerializeField] private string verticalInputAxis = "Vertical";
	[SerializeField] private string jumpButton = "Jump";
    public GameObject grenadePrefab;
    public float throwForce = 40f;
    public bool isHub;
    public bool isPaused;

    // Update is called once per frame
    void Update () {
        if (!isPaused)
        {
            float xInput = Input.GetAxisRaw(horizontalInputAxis);
            float yInput = Input.GetAxisRaw(verticalInputAxis);
            Vector2 input = new Vector2(xInput, yInput);

            movement.DirectionalInput = input;

            if (Input.GetButtonDown(jumpButton))
            {
                movement.Jump();
            }

            bool grenadePressed = Input.GetKeyDown(KeyCode.R);
            //Grenade Throwing
            if (grenadePressed && !isHub)
            {
                ThrowGrenade();
            }
        }
        else
        {
            movement.DirectionalInput = new Vector2(0,0);
            Vector2 input = new Vector2(0, 0);
        }
    }

    void ThrowGrenade()
    {
        GameObject grenade = Instantiate(grenadePrefab, transform.position, transform.rotation);
        Rigidbody2D rb = grenade.GetComponent<Rigidbody2D>();
        CharacterMovement move = this.GetComponent<CharacterMovement>();
        Vector3 speedx = new Vector3(move.velocity.x,0,0);
        Vector3 speedy = new Vector3(0, Mathf.Abs(move.velocity.y), 0);
        rb.AddForce((transform.up * throwForce) + speedy*2, ForceMode2D.Force);
        if (gameObject.transform.localScale.x >= 0)
        {
            rb.AddForce((transform.right * throwForce) + (speedx*8), ForceMode2D.Force);
        }
        else
        {
            rb.AddForce((transform.right * -throwForce) + (speedx*8), ForceMode2D.Force);
        }
    }
}