using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Laser : MonoBehaviour {
  [SerializeField]
  GameObject LaserEffect;
  [SerializeField]
  GameObject LaserButton;
  [SerializeField]
  Image cooldownCover;
  [SerializeField]
  GameObject clickPanel;
  [SerializeField]
  GameObject LaserAimer;
  AudioManagerCannon audioManager;
  bool shot = false;
  float LaserHalfWidth = 1.6875f;
  float LaserDamage = 100f;
  float BaseLaserCooldown = 100f;
  float remainingTime = 0f;
  float cooldownTimerChangeReceptor = 1f;
  //Initial setup including upgrades
  void Awake() {
    audioManager = GameObject.Find("AudioManagerCannon").GetComponent<AudioManagerCannon>();
    SetBaseSettings();
  }
  public void checkUpgradesForLaserEquipped() {
    if (UpgradesEquipped.EquippedUpgrades.Contains("Laser")) {
      LaserButton.SetActive(true);
    }
  }
  void SetBaseSettings() {
    int lvl = UpgradesManager.returnDictionaryValue("Laser")[0];
    // int lvl = 1; //testing
    BaseLaserCooldown = 100f - 5f * (float)lvl;//min 50sec
    LaserDamage = 100f + 90f * (float)lvl; // max at 1000 dmg
  }
  void Update() {
    if (BowManager.CoolDownRate != cooldownTimerChangeReceptor) {
      float oldBaseTime = BaseLaserCooldown * cooldownTimerChangeReceptor;
      float newBaseTime = BaseLaserCooldown * BowManager.CoolDownRate;
      float ratioRemaining = remainingTime / oldBaseTime;
      float newRemaining = ratioRemaining * newBaseTime;
      remainingTime = newRemaining;
      cooldownTimerChangeReceptor = BowManager.CoolDownRate;
    }
    if (remainingTime > 0f) {
      countDownTimer();
    }
    if (remainingTime <= 0f) {
      LaserButton.GetComponent<Button>().interactable = true;
      cooldownCover.fillAmount = 0;
      remainingTime = 0f;
    }
  }
  void countDownTimer() {
    remainingTime -= Time.deltaTime;
    RenderCooldownImage();
  }
  void RenderCooldownImage() {
    float BaseTime = BaseLaserCooldown * cooldownTimerChangeReceptor;
    float ratioRemaining = remainingTime / BaseTime;
    cooldownCover.fillAmount = ratioRemaining;
  }
  public void UseLaser() {
    if (BowManager.UsingCooldown || remainingTime != 0f) {
      return;
    }
    LaserButton.GetComponent<Button>().interactable = false;
    remainingTime = BaseLaserCooldown * cooldownTimerChangeReceptor;

    clickPanel.SetActive(true);
    LaserAimer.SetActive(true);
    BowManager.UsingCooldown = true;
    BowManager.GunsReady = false;
    StartCoroutine("RemoveButtonTouch");
    StartCoroutine("clickTimer");
  }
  IEnumerator RemoveButtonTouch() {
    while (Input.touchCount != 0 && clickPanel.activeSelf && shot == false) {
      yield return null;
    }
    if (clickPanel.activeSelf && shot == false) {
      StartCoroutine("LaserPlacement");
    }
  }
  IEnumerator clickTimer() {
    float timer = 0f;
    float timescale = 0.2f;
    float opacity = 0.4f;
    while (timer < 2f && shot == false) {
      timer += Time.deltaTime;
      Time.timeScale = timescale;
      timescale = 0.2f + 0.8f * timer / 2f;
      opacity = 0.4f - 0.4f * timer / 2f;
      clickPanel.GetComponent<Image>().color = new Color(0f, 0f, 0f, opacity);
      yield return null;
    }
    clickPanel.GetComponent<Image>().color = new Color(0f, 0f, 0f, 0.4f);
    if (shot == false) {
      Vector3 firePos = LaserAimer.GetComponent<Transform>().position;
      StartCoroutine("FireLaser", firePos);
    }
    ResetValues();
  }
  IEnumerator FireLaser(Vector3 position) {
    Instantiate(LaserEffect, new Vector3(position.x, -11, 0f), Quaternion.identity);
    audioManager.PlayAudio("Laser");
    yield return new WaitForSeconds(0.5f);
    damageWithLaser(position.x);
  }
  void ResetValues() {
    LaserAimer.SetActive(false);
    clickPanel.SetActive(false);
    LaserAimer.GetComponent<Transform>().position = new Vector3(0f, 0f, 0f);
    shot = false;
    BowManager.UsingCooldown = false;
    Invoke("readyguns", 0.5f);
    Time.timeScale = 1f;
  }
  void readyguns() {
    BowManager.GunsReady = true;
  }
  IEnumerator LaserPlacement() {
    while (clickPanel.activeSelf && shot == false) {
      if (Input.touchCount > 0 && remainingTime > 0f) {
        Touch touch = Input.GetTouch(0);
        Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
        touchPos.z = 0f;
        touchPos.y = 0f;
        if (touch.phase == TouchPhase.Moved) {
          LaserAimer.GetComponent<Transform>().position = touchPos;
          yield return null;
        }
        if (touch.phase == TouchPhase.Ended) {
          shot = true;
          Vector3 firePos = LaserAimer.GetComponent<Transform>().position;
          StartCoroutine("FireLaser", firePos);
          break;
        }
      }
      yield return null;
    }
  }
  void damageWithLaser(float x) {
    Collider2D[] Objects = Physics2D.OverlapAreaAll(new Vector2(x - LaserHalfWidth, -20f), new Vector2(x + LaserHalfWidth, 20f));
    foreach (Collider2D ene in Objects) {
      if ((ene.gameObject.tag == "Enemy" || ene.gameObject.tag == "TauntEnemy")) {
        ene.transform.root.gameObject.GetComponent<IDamageable>().takeTrueDamage(LaserDamage);
      }
    }
  }











}

