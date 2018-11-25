using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatToSprite : MonoBehaviour {

	[Range(0, 1), SerializeField]
	private float _val;
	
	public float val{
		get {
			return _val;
		}

		set {
			_val = Mathf.Clamp01(value);
			UpdateSprite();
		}
	}
	public Image image;
	public List<Sprite> sprites;

	void OnValidate() {
		UpdateSprite();
	}

	void UpdateSprite() {
		if(sprites.Count > 1) {
			int index = Mathf.CeilToInt(_val * (sprites.Count - 1));
			image.sprite = sprites[index];
		}
		
	}

}
