using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GigantodonMechanics : MonoBehaviour {
  [SerializeField] Slider TimerSlider;
  [SerializeField] IDamageable lifescript;
  [SerializeField] float timerInterval = 5f;
  float StartTimer;

  void Start() {
    StartTimer = Time.time;
    lifescript = gameObject.GetComponent<IDamageable>();
    BowManager.AmmoRate /= 1.5f;
    BowManager.CurrentAmmo = 0;
    StartCoroutine(ammoModulate());
  }
  void Update() {
    lifeRecover();
  }
  void lifeRecover() {
    if (lifescript.currentLife / lifescript.maxLife > 0.1f) return;
    lifescript.currentLife += 40f * Time.deltaTime;
  }

  IEnumerator ammoModulate() {
    while (true) {
      if (Time.time > timerInterval + StartTimer) {
        StartTimer = Time.time;
        BowManager.CurrentAmmo = 0;
        TimerSlider.value = 1f;
      } else {
        TimerSlider.value = (timerInterval - (Time.time - StartTimer)) / timerInterval;
      }
      yield return null;
    }
  }

  void OnDestroy() {
    BowManager.AmmoRate *= 1.5f;
    StopAllCoroutines();
  }

  void OnTriggerEnter2D(Collider2D coll) {
    if (!this.enabled) return;
    if (coll.tag == "Bullet") {
      IBullet bullet = coll.GetComponent<IBullet>();
      bullet.pierce = Mathf.FloorToInt(bullet.pierce / 2f);
      if (bullet.pierce == 0) Destroy(coll.gameObject);
    }
  }
}
