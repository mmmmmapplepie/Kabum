using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W3L46 : MonoBehaviour, IGetLevelDataInterface {
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
  bool done = false;
  string[] highrank = new string[4] { "", "Meso", "Macro", "Hyper" };
  string[] type = new string[4] { "Shifter", "Teleporter", "Ticker", "Enigma" };
  IEnumerator hspawner() {
    while (spawner.setEnemies.Count > 0 || !done) {
      spawner.spawnEnemy("Hyper" + type[Random.Range(0, 4)], spawner.ranXPos(), 10f);
      yield return new WaitForSeconds(Random.Range(0f, 3f));
    }
  }
  IEnumerator zipper() {
    while (spawner.setEnemies.Count > 0 || !done) {
      spawner.spawnEnemy("HyperZipper", spawner.ranXPos(), 10f);
      yield return new WaitForSeconds(Random.Range(0f, 2f));
    }
  }
  IEnumerator booster() {
    while (spawner.setEnemies.Count > 0 || !done) {
      spawner.spawnEnemy("HyperBooster", spawner.ranXPos(), 10f);
      yield return new WaitForSeconds(Random.Range(2f, 10f));
    }
  }
  void burst(string name, float y, int num) {
    int i = 0;
    while (i < num) {
      i++;
      spawner.spawnEnemyInMap(name, spawner.ranXPos(), y, true);
    }
  }
  IEnumerator wave1() {
    spawner.spawnEnemyInMap("HyperMaintainer", 0f, 8f, true);
    yield return new WaitForSeconds(10f);
    spawner.waveCleared();
  }
  IEnumerator wave2() {
    spawner.spawnEnemyInMap("HyperProtector", 3f, 8f, true);
    spawner.spawnEnemyInMap("HyperProtector", -3f, 8f, true);
    yield return new WaitForSeconds(5f);
    spawner.waveCleared();
  }

  IEnumerator wave3() {
    StartCoroutine(hspawner());
    yield return new WaitForSeconds(20f);
    spawner.waveCleared();
  }

  IEnumerator wave4() {
    StartCoroutine(hspawner());
    StartCoroutine(booster());
    yield return new WaitForSeconds(10f);
    burst("HyperMaintainer", 8f, 3);
    yield return new WaitForSeconds(10f);
    spawner.waveCleared();
  }

  IEnumerator wave5() {
    StartCoroutine(hspawner());
    spawner.spawnEnemyInMap("Ernesto", 0f, 10f, true, LevelSpawner.addToList.Specific, true);
    yield return new WaitForSeconds(20f);
    StartCoroutine(zipper());
    spawner.waveCleared();
  }
  IEnumerator wave6() {
    burst("HyperMaintainer", 5f, 3);
    yield return new WaitForSeconds(10f);
    burst("HyperProtector", 5f, 3);
    yield return new WaitForSeconds(15f);
    burst("HyperMaintainer", 5f, 4);
    yield return new WaitForSeconds(10f);
    burst("HyperProtector", 5f, 4);
    yield return new WaitForSeconds(15f);
    burst("HyperMaintainer", 5f, 5);
    yield return new WaitForSeconds(10f);
    burst("HyperProtector", 5f, 5);
    yield return new WaitForSeconds(5f);
    done = true;
    spawner.LastWaveEnemiesCleared();
  }

}
