using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W3L20 : MonoBehaviour, IGetLevelDataInterface {
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
  string[] rank = new string[4] { "", "Meso", "Macro", "Hyper" };
  IEnumerator wave1() {
    spawner.spawnEnemyInMap("Ticker", 2f, 10f, true);
    spawner.spawnEnemyInMap("Ticker", 2f, 9f, true);
    spawner.spawnEnemyInMap("Ticker", 2f, 8f, true);
    spawner.spawnEnemyInMap("Ticker", 2f, 7f, true);
    spawner.spawnEnemyInMap("Ticker", 2f, 6f, true);
    spawner.spawnEnemyInMap("Ticker", 2f, 5f, true);
    spawner.spawnEnemyInMap("Ticker", 2f, 4f, true);
    spawner.spawnEnemyInMap("Ticker", 2f, 3f, true);
    spawner.spawnEnemyInMap("Ticker", 2f, 2f, true);
    spawner.spawnEnemyInMap("Ticker", 2f, 1f, true);
    spawner.spawnEnemyInMap("Ticker", 2f, 0f, true);
    spawner.spawnEnemyInMap("Ticker", 2f, -1f, true);
    spawner.spawnEnemyInMap("Ticker", 2f, -2f, true);
    spawner.spawnEnemyInMap("Ticker", 2f, -3f, true);
    yield return new WaitForSeconds(10f);
    spawner.spawnEnemyInMap("HyperCore", 0f, 10f, true, LevelSpawner.addToList.Specific, true);
    yield return new WaitForSeconds(5f);
    spawner.AllTriggerEnemiesCleared();
  }

  IEnumerator outlierspawn() {
    while (spawner.setEnemies.Count > 0) {
      spawner.spawnEnemy(rank[Random.Range(0, 3)] + "Outlier", -5f, 10f);
      spawner.spawnEnemy(rank[Random.Range(0, 3)] + "Outlier", 0f, 10f);
      spawner.spawnEnemy(rank[Random.Range(0, 3)] + "Outlier", 5f, 10f);
      yield return new WaitForSeconds(Random.Range(10f, 15f));
    }
  }
  IEnumerator vesselspawn() {
    while (spawner.setEnemies.Count > 0) {
      spawner.spawnEnemy(rank[Random.Range(1, 3)] + "Vessel", spawner.ranXPos(), 10f);
      spawner.spawnEnemy(rank[Random.Range(1, 3)] + "Vessel", spawner.ranXPos(), 10f);
      yield return new WaitForSeconds(Random.Range(5f, 10f));
    }
  }
  IEnumerator tickerspawn() {
    while (spawner.setEnemies.Count > 0) {
      spawner.spawnEnemy(rank[Random.Range(1, 3)] + "Ticker", spawner.ranXPos(), 10f);
      yield return new WaitForSeconds(Random.Range(3f, 7f));
    }
  }
  IEnumerator wave2() {
    StartCoroutine(outlierspawn());
    StartCoroutine(vesselspawn());
    yield return StartCoroutine(tickerspawn());
    spawner.LastWaveEnemiesCleared();
  }
}
