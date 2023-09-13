using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W2L2 : MonoBehaviour, IGetLevelDataInterface {
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
    int spawnNum = 20;
    while (spawnNum > 0) {
      spawner.spawnEnemy("MesoShifter", spawner.randomWithRange(-5, 5f), 10f);
      spawner.spawnEnemy("MesoShifter", spawner.randomWithRange(-5, 5f), 10f);
      spawnNum--;
      yield return new WaitForSeconds(0.5f);
    }
    spawner.AllTriggerEnemiesCleared();
  }
  IEnumerator wave2() {
    for (int i = 5; i > -5; i--) {
      spawner.spawnEnemyInMap("MegaBasic", (float)i, 8f, false);
      yield return new WaitForSeconds(0.1f);
    }
    yield return new WaitForSeconds(3f);
    for (int i = 0; i < 30; i++) {
      spawner.spawnEnemy("MesoShifter", spawner.randomWithRange(-5, 5f), 10f);
    }
    spawner.AllTriggerEnemiesCleared();
  }
  IEnumerator wave3() {
    for (int bursts = 0; bursts < 15; bursts++) {
      for (int i = 0; i < 5; i++) {
        spawner.spawnEnemy("MacroShifter", spawner.randomWithRange(-5, 5f), 10f);
      }
      yield return new WaitForSeconds(2f);
    }
    spawner.LastWaveEnemiesCleared();
  }
}
