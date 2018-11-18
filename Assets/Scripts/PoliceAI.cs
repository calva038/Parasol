using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceAI : MonoBehaviour {

	//Integers
	public int curHealth;	
	public int maxHealth;

	//Floats
	public float distance;
	public float wakeRange;
	public float hitInterval;
	public float hitTimer;

	//Booleans
	public bool awake = false;
	public bool lookingRight = true;


	//References
	public Transform target;
	public Animator anim;
	public Transform hitPointLeft;
	public Transform hitPointRight;

	private Vector2 velocity;
	public float gravity = -20;
	private Controller2D controller;
	bool facingRight = true;


	void Awake()
	{
		anim = gameObject.GetComponent<Animator>();
		controller = gameObject.GetComponent<Controller2D>();
	}

	void Start()
	{
		curHealth = maxHealth;
	}

	void Update()
	{
		velocity.y += gravity * Time.deltaTime;
		//anim.SetBool("Awake", awake);
		//anim.SetBool("LookingRight", lookingRight);

		RangeCheck();

		if (target.transform.position.x > transform.position.x)
		{
			lookingRight = true;
		}

		if (target.transform.position.x < transform.position.x)
		{
			lookingRight = false;
		}

		if (lookingRight == true)
		{
			velocity.x = 2 ;

		}


		else
		{
			velocity.x = -2;
		}
		controller.Move(velocity * Time.deltaTime);

	}

	void RangeCheck()
	{
		distance = Vector3.Distance(transform.position, target.transform.position);

		if (distance < wakeRange)
		{

			awake = true;

		}

		if (distance > wakeRange)
		{

			awake = false;

		}
		if (curHealth <= 0 )
		{
			Destroy(gameObject);
		}
	}
	void Flip(){
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= - 1;
		transform.localScale = theScale;
	}
	void FixedUpdate()
	{
		if(velocity.x > 0 &&!facingRight){
			Flip();
		}
		else if(velocity.x < 0 && facingRight){
			Flip();
		}
	}

	public void Attack(bool attackingRight)
	{

		hitTimer += Time.deltaTime;

		if (hitTimer >= hitInterval)
		{

			Vector2 direction = target.transform.position - transform.position;
			direction.Normalize();

			if(!attackingRight)
			{

				hitTimer = 0;


			}

			if (attackingRight)
			{

				hitTimer = 0;
			}

		}
	}



	public void Damage(int damage)
	{

	curHealth -= damage;
	//	gameObject.GetComponent<Animation>().Play("Player_RedFlash");
	}
}
