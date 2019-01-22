using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Animator))]
public class PlayerAttack : MonoBehaviour {

	[SerializeField] private float attackDuration = 0.3f;
	[SerializeField] private Collider2D attackTrigger;

	[Header("Audio")]
	[SerializeField] private new AudioSource audio;
	[SerializeField] private List<AudioClip> swingSounds;

	private AttackState attackState = AttackState.NOT_ATTACKING;
	private float attackTimer = 0;
	private Animator anim;

	private void OnValidate () {
		attackDuration = Mathf.Clamp (attackDuration, 0, int.MaxValue);
	}

	private void Awake () {
		anim = gameObject.GetComponent<Animator> ();
		attackTrigger.enabled = false;
	}

	public enum AttackState {
		ATTACKING,
		NOT_ATTACKING
	}

	private void Update () {
		switch (attackState) {
			case (AttackState.ATTACKING):
				if (attackTimer > 0) {
					attackTimer -= Time.deltaTime;
				} else {
					attackTrigger.enabled = false;
					attackState = AttackState.NOT_ATTACKING;
				}
				break;

			case (AttackState.NOT_ATTACKING):
				if (Input.GetKeyDown (KeyCode.F)) {
					attackTimer = attackDuration;
					attackTrigger.enabled = true;
					attackState = AttackState.ATTACKING;

					if(swingSounds.Count > 0) {
						audio.PlayOneShot(swingSounds[UnityEngine.Random.Range(0, swingSounds.Count)]);
					}
					
				}
				break;

			default:
				break;
		}

		anim.SetBool ("Attacking", attackState == AttackState.ATTACKING);
	}
}