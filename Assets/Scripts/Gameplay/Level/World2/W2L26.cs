using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W2L26 : MonoBehaviour, IGetLevelDataInterface {
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
      spawner.spawnEnemy(level.Enemies[Random.Range(0, level.Enemies.Count)].enemyPrefab.name, spawner.ranXPos(), 10f);
      yield return new WaitForSeconds(2f);
    }
    spawner.AllTriggerEnemiesCleared();
  }
  IEnumerator burst(string Name) {
    spawner.spawnEnemyInMap(Name, -5f, 0f, false);
    spawner.spawnEnemyInMap(Name, -2.5f, 0f, false);
    spawner.spawnEnemyInMap(Name, 0f, 0f, false);
    spawner.spawnEnemyInMap(Name, 2.5f, 0f, false);
    spawner.spawnEnemyInMap(Name, 5f, 0f, false);
    yield return new WaitForSeconds(2f);
    spawner.spawnEnemyInMap(Name, -5f, 4f, false);
    spawner.spawnEnemyInMap(Name, -2.5f, 4f, false);
    spawner.spawnEnemyInMap(Name, 0f, 4f, false);
    spawner.spawnEnemyInMap(Name, 2.5f, 4f, false);
    spawner.spawnEnemyInMap(Name, 5f, 4f, false);
    yield return new WaitForSeconds(2f);
    spawner.spawnEnemyInMap(Name, -5f, 8f, false);
    spawner.spawnEnemyInMap(Name, -2.5f, 8f, false);
    spawner.spawnEnemyInMap(Name, 0f, 8f, false);
    spawner.spawnEnemyInMap(Name, 2.5f, 8f, false);
    spawner.spawnEnemyInMap(Name, 5f, 8f, false);
  }
  IEnumerator wave2() {
    StartCoroutine(burst("MacroTeleporter"));
    yield return new WaitForSeconds(15f);
    StartCoroutine(burst("MesoVessel"));
    yield return new WaitForSeconds(15f);
    spawner.LastWaveEnemiesCleared();
  }
}
