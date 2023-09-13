using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W3L18 : MonoBehaviour, IGetLevelDataInterface {
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
  #endregion
  string[] rank = new string[2] { "Macro", "Hyper" };
  string[] type = new string[3] { "Enigma", "Vessel", "Ticker" };
  IEnumerator wave1() {
    int i = 0;
    while (i < 40) {
      i++;
      spawner.spawnEnemy("Macro" + type[Random.Range(0, 3)], 5f, 10f);
      yield return new WaitForSeconds(1f);
    }
    spawner.AllTriggerEnemiesCleared();
  }

  IEnumerator wave1Equiv() {
    int i = 0;
    while (i < 20) {
      i++;
      spawner.spawnEnemy("Macro" + type[Random.Range(0, 3)], -5f, 10f);
      yield return new WaitForSeconds(2f);
    }
  }
  IEnumerator wave2() {
    StartCoroutine(wave1Equiv());
    int i = 0;
    while (i < 40) {
      i++;
      spawner.spawnEnemy("Hyper" + type[Random.Range(0, 3)], 5f, 10f);
      yield return new WaitForSeconds(1f);
    }
    spawner.LastWaveEnemiesCleared();
  }
}
