using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSkinChanger : MonoBehaviour {
	[SerializeField]
	List<Skin> listOfBulletSkins = new List<Skin>();
	Skin currSkin;
	GameObject effect = null;
	void Awake() {
		currSkin = FindBulletSkin();
		if (currSkin.particleEffect != null) {
			effect = currSkin.particleEffect;
		}
	}
	Skin FindBulletSkin() {
		return listOfBulletSkins.Find(x => x.name == SettingsManager.currBulletSkin);
	}
	public void changeBulletSprite(GameObject ob, bool helper = false) {
		ob.GetComponent<SpriteRenderer>().sprite = currSkin.mainBody;
		if (helper) return;
		if (effect != null) {
			Transform tra = ob.GetComponent<Transform>();
			GameObject GO = Instantiate(effect, tra);
		}
	}

	public void changeScale(Transform parent, float scale) {
		parent.transform.localScale = scale * parent.transform.localScale;
		foreach (Transform child in parent) {
			changeScale(child, scale);
		}
	}
}
