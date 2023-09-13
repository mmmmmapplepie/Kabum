using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W2L19 : MonoBehaviour, IGetLevelDataInterface {
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
    audio.ChangeBGM("World2");
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

  IEnumerator wave1() {
    for (int i = 0; i < 20; i++) {
      spawner.spawnEnemy("MesoEnigma", 3f, 10f);
      yield return new WaitForSeconds(2f);
    }
    for (int i = 0; i < 10; i++) {
      spawner.spawnEnemy("MacroEnigma", -3f, 10f);
      yield return new WaitForSeconds(2f);
    }
    spawner.AllTriggerEnemiesCleared();
  }

  IEnumerator wave2() {
    for (int i = 0; i < 20; i++) {
      spawner.spawnEnemy("Ticker", 0f, 10f);
      yield return new WaitForSeconds(1f);
    }
    yield return new WaitForSeconds(10f);
    for (int i = 0; i < 15; i++) {
      spawner.spawnEnemy("MesoTicker", 0f, 10f);
      spawner.spawnEnemy("MesoEnigma", 3f, 10f);
      spawner.spawnEnemy("MacroEnigma", -3f, 10f);
      yield return new WaitForSeconds(3f);
    }
    spawner.LastWaveEnemiesCleared();
  }
}
