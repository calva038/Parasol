using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackTrigger : MonoBehaviour {

	public int dmg = 1;



	void OnTriggerStay2D(Collider2D col)
	{
		Debug.Log("lol");

		if (col.isTrigger != true )
		{
			

			PoliceAI enemy = col.gameObject.GetComponent<PoliceAI>();
			enemy.curHealth -= 1;


		}
	}
}
