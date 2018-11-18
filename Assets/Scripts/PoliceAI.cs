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


	void Awake()
	{
		anim = gameObject.GetComponent<Animator>();
	}

	void Start()
	{
		curHealth = maxHealth;
	}

	void Update()
	{
		anim.SetBool("Awake", awake);
		anim.SetBool("LookingRight", lookingRight);

		RangeCheck();

		if (target.transform.position.x > transform.position.x)
		{
			lookingRight = true;
		}

		if (target.transform.position.x < transform.position.x)
		{
			lookingRight = false;
		}


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



	//public void Damage(int damage)
	//{

	//	curHealth -= damage;
	//	gameObject.GetComponent<Animation>().Play("Player_RedFlash");
	//}
}
