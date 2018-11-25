using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthTing : MonoBehaviour {

	public Animator animator;
	public AudioSource audio;
	private bool collected = false;

	void OnTriggerEnter2D(Collider2D other) {
		if(other.CompareTag("Player") && !collected) {
			Health playerHealth = other.gameObject.GetComponent<Health>();
			collected = true;
			playerHealth.curHealth += 1;
			animator.SetTrigger("Collected");
			audio.Play();
			Destroy(this.gameObject, 2);
		}
	}
}
