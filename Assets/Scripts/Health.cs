using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    public int curHealth;
    public int maxHealth;
    public Damageable damageable;
    private bool dying = false;

    private void Start() {
        damageable.OnReceiveDamage += ReceiveDamage;
    }

    private void OnDestroy() {
        damageable.OnReceiveDamage -= ReceiveDamage;
    }

	public void ReceiveDamage(int dmg) {
		int finalHealth = curHealth - dmg;
		curHealth = Mathf.Clamp(finalHealth, 0, maxHealth);
        if(curHealth <= 0 && !dying) {
            Death();
            dying = true;
        }
	}

    private void Death() {
        Destroy(this.gameObject);
    }
}
