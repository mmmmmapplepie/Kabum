using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W2L27 : MonoBehaviour, IGetLevelDataInterface {
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

  IEnumerator burst(string Name) {
    spawner.spawnEnemyInMap(Name, 0f, -2f, false);
    yield return new WaitForSeconds(0.2f);
    spawner.spawnEnemyInMap(Name, 0f, -1f, false);
    yield return new WaitForSeconds(0.2f);
    spawner.spawnEnemyInMap(Name, 0f, 0f, false);
    yield return new WaitForSeconds(0.2f);
    spawner.spawnEnemyInMap(Name, 0f, 1f, false);
    yield return new WaitForSeconds(0.2f);
    spawner.spawnEnemyInMap(Name, 0f, 2f, false);
    yield return new WaitForSeconds(0.2f);
    spawner.spawnEnemyInMap(Name, 0f, 3f, false);
    yield return new WaitForSeconds(0.2f);
    spawner.spawnEnemyInMap(Name, 0f, 4f, false);
    yield return new WaitForSeconds(0.2f);
    spawner.spawnEnemyInMap(Name, 0f, 5f, false);
    yield return new WaitForSeconds(0.2f);
    spawner.spawnEnemyInMap(Name, 0f, 6f, false);
    yield return new WaitForSeconds(0.2f);
    spawner.spawnEnemyInMap(Name, 0f, 7f, false);
    yield return new WaitForSeconds(0.2f);
    spawner.spawnEnemyInMap(Name, 0f, 8f, false);
    yield return new WaitForSeconds(0.2f);
    spawner.spawnEnemyInMap(Name, 0f, 9f, false);
  }
  IEnumerator wave1() {
    yield return burst("UltimateBasic");
    spawner.AllTriggerEnemiesCleared();
  }
  IEnumerator wave2() {
    for (int i = 0; i < 40; i++) {
      spawner.spawnEnemy(level.Enemies[Random.Range(0, level.Enemies.Count)].enemyPrefab.name, spawner.ranXPos(), 10f);
      yield return new WaitForSeconds(1f);
    }
    spawner.LastWaveEnemiesCleared();
  }
}
