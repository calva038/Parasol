using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour {

	public int damage = 1;
	public List<string> ignoreTags;

	void OnTriggerEnter2D (Collider2D col) {
		if (!ignoreTags.Contains (col.tag)) {
			Damageable damageable = col.GetComponent<Damageable> ();
			if (damageable != null) {
				damageable.ReceiveDamage (damage);
			}
		}

	}
}