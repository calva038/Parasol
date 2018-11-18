using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackTrigger : MonoBehaviour {

	public int dmg = 1;



	void OnTriggerEnter2D(Collider2D col)
	{

		if (col.isTrigger != true )
		{
			

			PoliceAI enemy = col.gameObject.GetComponent<PoliceAI>();
			enemy.curHealth -= 1;


		}
	}
}
