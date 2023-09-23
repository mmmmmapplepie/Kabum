using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowImageFlicker : MonoBehaviour {
	[SerializeField] SpriteRenderer spriteRenderer;

	[SerializeField] float waitMin, waitMax, alphaMin, alphaMax;

	void Start() {
		StartCoroutine(Flicker());
	}
	public void startRoutines() {
		StartCoroutine(Flicker());
	}
	public IEnumerator Flicker() {
		while (true) {
			// bool longwait = Random.Range(0, 2) == 0 ? true : false;
			// float wait = Random.Range(0.01f, 0.2f);
			// if (longwait) wait = Random.Range(0.5f, 1.2f);
			// float alpha = Random.Range(0.4f, 1f);
			float wait = Random.Range(waitMin, waitMax);
			float alpha = Random.Range(alphaMin, alphaMax);
			spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, alpha);
			yield return new WaitForSeconds(wait);
		}
	}
	public void stopRoutines() {
		StopAllCoroutines();
	}
}
