using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPIndicator : MonoBehaviour {

	public List<FloatToSprite> HPSprites;
	public int HPPerFlower = 5;
	public Health health;

	void Update() {
		int HP = health.curHealth;
		foreach(FloatToSprite s in HPSprites) {
			s.val = Mathf.Clamp01((float)HP / HPPerFlower);
			HP = Mathf.Clamp(HP - HPPerFlower, 0, int.MaxValue);
		}
	}
}
