using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

	private static Inventory _ins;

	public static Inventory Instance {
		get{
			return _ins;
		}
	}

	void Awake() {
		if(_ins == null) {
			_ins = this;
		}
		else {
			Destroy(this);
		}
	}

	public int coins;
}
