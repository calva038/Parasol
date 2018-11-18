using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour {

	public float Timer;
	public float Ready;


void Update()
{
	Timer += Time.deltaTime;

	//if (col.isTrigger != true )
	//	{
			
	//		if (Timer >= 2){
				//if (col.isTrigger != true )
				//{
					//PlayerStats You = col.gameObject.GetComponent<PlayerStats>();
				//	You.curHealth -= 10;
				//	Timer = 0;
				//}
				//else
				//{
				//Timer = 0;
				//}
		//	}
	//	}
}
void OnTriggerEnter2D(Collider2D col)
	{


	}
}