using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtSound : MonoBehaviour {

	[SerializeField] private Damageable damageable;
	[SerializeField] private AudioSource audio;
	[SerializeField] private List<AudioClip> hurtSounds;

	void Start () {
		damageable.OnReceiveDamage += this.OnTakeDamage;
	}

	void OnDestroy () {
		damageable.OnReceiveDamage -= this.OnTakeDamage;
	}

	void OnTakeDamage (int dmg) {
		if (hurtSounds.Count > 0) {
			audio.PlayOneShot (hurtSounds[Random.Range (0, hurtSounds.Count)]);
		}
	}
}