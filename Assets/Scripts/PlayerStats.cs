using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

	//stats 
	public int curHealth; 
	public int maxHealth;


	void Update()
	{
		if (curHealth <= 0)
			gameObject.SetActive(false);
	}
}
