using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W2L8 : MonoBehaviour, IGetLevelDataInterface {
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
    for (int i = 0; i < 10; i++) {
      spawner.spawnEnemy("MesoEnigma", Random.Range(-5f, 5f), 10f);
      yield return new WaitForSeconds(2f);
    }
    spawner.AllTriggerEnemiesCleared();
  }

  IEnumerator wave2() {
    for (int i = 0; i < 5; i++) {
      float xPos = spawner.ranXPos();
      for (int j = 0; j < 8; j++) {
        spawner.spawnEnemy("MacroZipper", xPos, 10f);
      }
      yield return new WaitForSeconds(3f);
    }
    spawner.AllTriggerEnemiesCleared();
  }

  IEnumerator wave3() {
    for (int i = 0; i < 10; i++) {
      float xPos = spawner.ranXPos();
      if (i % 2 == 0) {
        for (int j = 0; j < 10; j++) {
          spawner.spawnEnemy("MacroZipper", xPos, 10f);
        }
        yield return new WaitForSeconds(2f);
      } else {
        for (int j = 0; j < 15; j++) {
          spawner.spawnEnemy("MesoEnigma", xPos, 10f);
        }
        yield return new WaitForSeconds(4f);
      }
    }
    spawner.LastWaveEnemiesCleared();
  }
}
