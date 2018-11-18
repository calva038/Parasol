using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour {

void OnTriggerEnter2D(Collider2D col)
	{

		if (col.isTrigger != true )
		{
			

			PlayerStats You = col.gameObject.GetComponent<PlayerStats>();
			You.curHealth -= 10;


		}
	}
}