using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

	public Animator animator;
	public new AudioSource audio;
	private bool collected = false;

	void OnTriggerEnter2D(Collider2D other) {
		if(other.CompareTag("Player") && !collected) {
			collected = true;
			Inventory.Instance.coins += 1;
			animator.SetTrigger("Collected");
			audio.Play();
			Destroy(this.gameObject, 2);
		}
	}
}
