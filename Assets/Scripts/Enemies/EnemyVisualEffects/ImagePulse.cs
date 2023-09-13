using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImagePulse : MonoBehaviour {

  [SerializeField] SpriteRenderer spriteRenderer;

  [SerializeField] float waitMin, waitMax, upPeriod, downPeriod, maxAlpha, minAlpha;

  void Start() {
    StartCoroutine(Pulse());
  }

  IEnumerator Pulse() {
    while (true) {
      float maxwait = Random.Range(waitMax, waitMax + waitMax / 4f);
      float minwait = Random.Range(waitMin, waitMin + waitMin / 4f);
      float freqtimeUp = Random.Range(upPeriod, upPeriod + upPeriod / 4f);
      float freqtimeDown = Random.Range(downPeriod, downPeriod + downPeriod / 4f);

      spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, maxAlpha);
      yield return new WaitForSeconds(maxwait);
      while (spriteRenderer.color.a > minAlpha) {
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, spriteRenderer.color.a - (maxAlpha - minAlpha) / freqtimeDown * Time.deltaTime);
        yield return null;
      }
      spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, minAlpha);
      yield return new WaitForSeconds(minwait);
      while (spriteRenderer.color.a < maxAlpha) {
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, spriteRenderer.color.a + (maxAlpha - minAlpha) / freqtimeUp * Time.deltaTime);
        yield return null;
      }

    }
  }
}
