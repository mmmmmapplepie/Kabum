using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenBoss_Invisible : MonoBehaviour {
	[SerializeField] float pickRadius;
	[SerializeField] Collider2D effectRange;
	List<GameObject> Enemies = new List<GameObject>();
	void OnEnable() {
		if (!this.enabled) return;
		InitialAddEnemies();
	}
	void InitialAddEnemies() {
		Collider2D[] EnemiesTemp = Physics2D.OverlapCircleAll(transform.root.position, pickRadius);
		foreach (Collider2D coll in EnemiesTemp) {
			if (coll.tag == "Enemy" || coll.tag == "TauntEnemy") {
				Enemies.Add(coll.gameObject);
				ChangeVisibility(coll.gameObject, true);
			}
		}
	}
	void ChangeVisibility(GameObject imageObject, bool Invisible) {
		float opacity = 1f;
		GameObject chained;
		GameObject ernestoMark;
		try {
			chained = imageObject.transform.root.Find("State").Find("Life").Find("Background").GetChild(0).gameObject;
		} catch {
			chained = null;
		}
		try {
			ernestoMark = imageObject.transform.root.Find("State").Find("ErnestoMark").gameObject;
		} catch {
			ernestoMark = null;
		}
		try {
			imageObject.transform.root.gameObject.GetComponent<TurnInvisibleBase>().ChangeVisibility(Invisible);
		} catch {

		}
		if (Invisible) {
			if (chained != null) chained.SetActive(false);
			if (ernestoMark != null) ernestoMark.SetActive(false);
			imageObject.transform.root.Find("State").gameObject.GetComponent<Canvas>().enabled = false;
			opacity = 0f;
		} else {
			imageObject.transform.root.Find("State").gameObject.GetComponent<Canvas>().enabled = true;
			if (chained != null) chained.SetActive(true);
			if (ernestoMark != null) ernestoMark.SetActive(true);
		}
		Transform parent = imageObject.transform;
		if (imageObject.GetComponent<SpriteRenderer>()) {
			Color originalColor = imageObject.GetComponent<SpriteRenderer>().color;
			imageObject.GetComponent<SpriteRenderer>().color = new Color(originalColor.r, originalColor.g, originalColor.b, opacity);
		}
		if (parent.childCount != 0) {
			foreach (Transform child in parent) {
				ChangeVisibility(child.gameObject, Invisible);
			}
		}
	}
	void OnTriggerEnter2D(Collider2D coll) {
		if (!this.enabled) return;
		if (coll.tag != "Enemy" && coll.tag != "TauntEnemy") {
			return;
		}
		Enemies.Add(coll.gameObject);
		ChangeVisibility(coll.gameObject, true);
	}
	void OnTriggerExit2D(Collider2D coll) {
		if (!this.enabled) return;
		if (coll.tag != "Enemy" && coll.tag != "TauntEnemy") {
			return;
		}
		Enemies.Remove(coll.gameObject);
		ChangeVisibility(coll.gameObject, false);
	}
	void OnDisable() {
		foreach (GameObject enemies in Enemies) {
			if (enemies != null) {
				ChangeVisibility(enemies, false);
			}
		}
		Enemies.Clear();
	}
}