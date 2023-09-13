using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W3L12 : MonoBehaviour, IGetLevelDataInterface {
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

  IEnumerator wave1() {
    spawner.spawnEnemy("MacroVessel", 0f, 10f);
    yield return new WaitForSeconds(5f);
    spawner.spawnEnemy("MacroVessel", 0f, 10f);
    yield return new WaitForSeconds(5f);
    spawner.spawnEnemy("MacroVessel", 0f, 10f);
    spawner.AllTriggerEnemiesCleared();
  }

  void burst(string name, int num, bool bigsize) {
    int i = 0;
    while (i < num) {
      i++;
      spawner.spawnEnemyInMap(name, spawner.ranXPos(), Random.Range(0f, 10f), bigsize);
    }
  }
  IEnumerator wave2() {
    for (int i = 0; i < 8; i++) {
      if (i % 2 == 0) {
        burst("MacroTeleporter", 30, false);
      } else {
        burst("MacroVessel", 6, true);
      }
      yield return new WaitForSeconds(5f);
    }
    spawner.LastWaveEnemiesCleared();
  }
}
