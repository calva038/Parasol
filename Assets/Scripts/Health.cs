using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour {

    public int curHealth;
    public int maxHealth;
    public Damageable damageable;
    private bool dying = false;
    private GameObject player;

    private void Start() {
        player = GameObject.Find("Player"); 
        damageable.OnReceiveDamage += ReceiveDamage;
    }

    private void OnDestroy() {
        damageable.OnReceiveDamage -= ReceiveDamage;
    }

    public void ReceiveDamage(int dmg) {
        if (this.gameObject == player && CharacterMovement.isDamaged > 0)
        {
            ;
        }
        else
        {
            int finalHealth = curHealth - dmg;
            curHealth = Mathf.Clamp(finalHealth, 0, maxHealth);
            if (this.gameObject == player)
            {
                CharacterMovement.knockbackCount += .2;
                CharacterMovement.isDamaged += 1.5;
            }
            if (curHealth <= 0 && !dying)
            {
                Death();
                dying = true;
            }
        }
    }
    
    private void Death() {
        if (this.gameObject == player)
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
            CharacterMovement.isDamaged = 0;
            CharacterMovement.knockbackCount = 0;
}
        Destroy(this.gameObject);
    }
}
