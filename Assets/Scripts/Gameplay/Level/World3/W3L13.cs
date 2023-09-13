using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W3L13 : MonoBehaviour, IGetLevelDataInterface {
  #region basicLevelWaveChangingCode
  [SerializeField]
  Level level;
  LevelSpawner spawner;
  new AudioManagerBGM audio;
  public Level GetLevelData() {
    return level;
  }
  void Awake() {
    spawner = gameObject.GetComponent<LevelSpawner>();
    spawner.setLevelData(level);
    audio = GameObject.Find("AudioManagerBGM").GetComponent<AudioManagerBGM>();
  }
  void Start() {
    audio.ChangeBGM("World3");
  }
  void Update() {
    if (spawner.waveRunning == false && WaveController.startWave == true && WaveController.LevelCleared == false) {
      string name = spawner.findCorrectWaveToStart();
      if (name != null) {
        StartCoroutine(name);
      }
    }
  }

  string[] type = new string[3] { "Basic", "Armored", "Shield" };
  #endregion
  IEnumerator wave1() {
    float t = Time.time;
    bool rightwards = true;
    float x = 5f;
    while (Time.time < t + 60f) {
      if (Time.time - t > 20f && Time.time - t < 22f) {
        yield return new WaitForSeconds(3f);
      }
      if (Time.time - t > 40f && Time.time - t < 42f) {
        yield return new WaitForSeconds(3f);
      }
      if (x > 5f) {
        rightwards = false;
        x = 5f;
      }
      if (x < -5f) {
        rightwards = true;
        x = -5f;
      }
      spawner.spawnEnemy("Nano" + type[Random.Range(0, 3)], x, 10f);
      x = rightwards ? x + 0.1f : x - 0.1f;
      yield return new WaitForSeconds(0.01f);
    }
    spawner.LastWaveEnemiesCleared();
  }
}
