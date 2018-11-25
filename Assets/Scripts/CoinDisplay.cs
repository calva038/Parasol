using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinDisplay : MonoBehaviour {

	public Inventory inventory;
	public TextMeshProUGUI text;

	void Update() {
		text.text = inventory.coins + "";
	}

}
