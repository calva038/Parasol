using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class SmoothCam : MonoBehaviour {

	public Transform target;
	public Vector3 offset;
	[Range(0.01f, 20f)]
	public float sharpness = 1f;

	void Start() {
		Assert.IsNotNull(target, "Target must be assigned!");
	}

	void Update() {
		float zpos = transform.position.z;
		float t = Mathf.Clamp01(sharpness * Time.deltaTime);
		Vector3 nextPos = Vector2.Lerp(transform.position, target.position, t);
		nextPos.z = zpos;
		transform.position = nextPos;
	}
}
