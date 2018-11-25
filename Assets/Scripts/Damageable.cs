using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour {

	public Action<int> OnReceiveDamage;

	public void ReceiveDamage(int dmg) {
		OnReceiveDamage(dmg);
	}
}

	// [SerializeField] private int curHealth;
	// [SerializeField] private int maxHealth;

	// public int CurHealth {
	// 	get {
	// 		return curHealth;
	// 	}

	// 	set {
	// 		if (value != curHealth) {
	// 			int change = value - curHealth;
	// 			curHealth = Mathf.Clamp(value, 0, int.MaxValue);
	// 			onCurHealthChange (curHealth, change);
	// 		}
	// 	}
	// }

	// public int MaxHealth {
	// 	get {
	// 		return maxHealth;
	// 	}

	// 	set {
	// 		if (value != maxHealth) {
	// 			int change = value - maxHealth;
	// 			maxHealth = Mathf.Clamp(value, 0, int.MaxValue);
	// 			onMaxHealthChange (maxHealth, change);
	// 		}
	// 	}
	// }

	// void OnValidate() {
	// 	curHealth = Mathf.Clamp(curHealth, 0, int.MaxValue);
	// 	maxHealth = Mathf.Clamp(maxHealth, 0, int.MaxValue);
	// }
	
	// /// <summary>
	// /// Called when current health changes. (First param is current health; Second param is amount changed)
	// /// </summary>
	// public Action<int,int> onCurHealthChange;

	// /// <summary>
	// /// Called when maximum health changes. (First param is maximum health; Second param is amount changed)
	// /// </summary>
	// public Action<int,int> onMaxHealthChange;