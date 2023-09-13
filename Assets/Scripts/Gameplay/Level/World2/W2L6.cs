using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W2L6 : MonoBehaviour, IGetLevelDataInterface {
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
    spawner.spawnEnemyInMap("HyperTicker", 0f, 4f, true);
    yield return new WaitForSeconds(3f);
    spawner.AllTriggerEnemiesCleared();
  }

  bool ultimateSpawnDone = false;
  IEnumerator UltimateSpawner() {
    for (int i = 0; i < 4; i++) {
      spawner.spawnEnemy("UltimateBasic", Random.Range(-5f, 5f), 10f);
      yield return new WaitForSeconds(15f);
    }
    ultimateSpawnDone = true;
  }
  IEnumerator wave2() {
    StartCoroutine(UltimateSpawner());
    int rotation = 0;
    while (!ultimateSpawnDone) {
      rotation++;
      if (rotation % 2 == 0) {
        for (int i = 5; i > -5; i--) {
          spawner.spawnEnemy("MesoTicker", (float)i, 10f);
          yield return new WaitForSeconds(2f);
        }
      } else {
        for (int i = 5; i > -5; i--) {
          spawner.spawnEnemy("MacroTicker", (float)i, 10f);
          yield return new WaitForSeconds(3f);
        }
      }
    }
    spawner.LastWaveEnemiesCleared();
  }
}
