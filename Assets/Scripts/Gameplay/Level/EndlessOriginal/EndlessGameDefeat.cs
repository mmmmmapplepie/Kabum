using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
public class EndlessGameDefeat : MonoBehaviour, DefeatScripts {
  public GameObject AdButton { get; set; }
  public GameObject selfObject { get; set; }
  [SerializeField] List<string> tipsList;
  [SerializeField] Text tipsText, loadPercent, RewardsAndPoints;
  [SerializeField] GameObject loadPanel;
  new AudioManagerUI audio;

  [HideInInspector]
  public float StartTime;
  float Reward;
  string EndlessType;
  void Awake() {
    selfObject = gameObject;
    audio = GameObject.FindObjectOfType<AudioManagerUI>();
  }
  void OnEnable() {
    StartCoroutine(loseAudio());
    Time.timeScale = 0f;
    showRewards();
    EndlessType = SceneManager.GetActiveScene().name;
    BowManager.GunsReady = false;
  }
  void showRewards() {
    float timeElapsed = Time.time - StartTime;
    float multiplier = getMultiplier(timeElapsed);
    Reward = timeElapsed * multiplier;
    string rewardString = "Your Current reward:" + $"\n" + "Bombs: " + Mathf.Round(Reward).ToString()
    + $"\n" + "Score: " + Mathf.Round(Reward * 1.5f).ToString();
    RewardsAndPoints.text = rewardString;
  }
  float getMultiplier(float time) {
    float multiplier;
    if (time < 600f) {
      multiplier = (time * 5f) / 600f;
    } else {
      multiplier = 10f;
    }
    return multiplier;
  }
  IEnumerator loseAudio() {
    yield return new WaitForSecondsRealtime(0.2f);
    audio.PlayAudio("Defeat");
  }
  public void Restart() {
    audio.PlayAudio("Click");
    Time.timeScale = 1f;
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
  }
  public void GameModes() {
    audio.PlayAudio("Click");
    SceneManager.LoadScene("GameMode");
  }
  public void ContinueAfterAd() {
    audio.PlayAudio("Click");
    AdButton.GetComponent<Button>().gameObject.SetActive(false);
    LifeManager.CurrentLife = BowManager.MaxLife;
    gameObject.SetActive(false);
  }
  void OnDisable() {
    BowManager.GunsReady = true;
    Time.timeScale = 1f;
    if (EndlessType == "EndlessOriginal") {
      if (Mathf.Round(Reward * 1.5f) > SettingsManager.endlessOriginalHS) SettingsManager.endlessOriginalHS = Mathf.Round(Reward * 1.5f);
    } else {
      if (Mathf.Round(Reward * 1.5f) > SettingsManager.endlessUpgradedHS) SettingsManager.endlessUpgradedHS = Mathf.Round(Reward * 1.5f);
    }
    SaveSystem.saveSettings();
  }
  void OnDestroy() {
    MoneyManager.addMoney((int)Mathf.Round(Reward));
  }
}
