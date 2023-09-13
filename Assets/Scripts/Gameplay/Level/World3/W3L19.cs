using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W3L19 : MonoBehaviour, IGetLevelDataInterface {
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

  string[] type = new string[3] { "Teleporter", "Shifter", "Zipper" };
  void ranburst(int num, string name = null) {
    for (int i = 0; i < num; i++) {
      string n = name;
      if (name == null) {
        n = "Hyper" + type[Random.Range(0, 3)];
      }
      spawner.spawnEnemyInMap(n, spawner.ranXPos(), Random.Range(-2f, 9f), false);
    }
  }
  IEnumerator wave1() {
    for (int i = 0; i < 5; i++) {
      ranburst(i + 10);
      yield return new WaitForSeconds((float)i + 5f);
    }
    spawner.AllTriggerEnemiesCleared();
  }
  IEnumerator wave2() {
    for (int i = 0; i < 6; i++) {
      string n = null;
      if (i % 3 == 0) {
        n = "Hyper" + type[0];
      }
      if (i % 3 == 1) {
        n = "Hyper" + type[1];
      }
      if (i % 3 == 2) {
        n = "Hyper" + type[2];
      }
      ranburst(i + 15, n);
      yield return new WaitForSeconds((float)i + 5f);
    }
    spawner.AllTriggerEnemiesCleared();
  }

  IEnumerator spawn() {
    while (spawner.setEnemies.Count > 0) {
      ranburst(Random.Range(15, 20));
      yield return new WaitForSeconds(8f);
    }
  }
  IEnumerator wave3() {
    spawner.spawnEnemyInMap("HyperHavoc", -3f, 8f, true, LevelSpawner.addToList.Specific, true);
    spawner.spawnEnemyInMap("HyperHavoc", -3f, 8f, true, LevelSpawner.addToList.Specific, true);
    spawner.spawnEnemyInMap("HyperHavoc", -1f, 8f, true, LevelSpawner.addToList.Specific, true);
    spawner.spawnEnemyInMap("HyperHavoc", 1f, 8f, true, LevelSpawner.addToList.Specific, true);
    spawner.spawnEnemyInMap("HyperHavoc", 3f, 8f, true, LevelSpawner.addToList.Specific, true);
    spawner.spawnEnemyInMap("HyperHavoc", 5f, 8f, true, LevelSpawner.addToList.Specific, true);
    yield return new WaitForSeconds(5f);
    yield return StartCoroutine(spawn());
    spawner.LastWaveEnemiesCleared();
  }
}
