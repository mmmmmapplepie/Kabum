using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W2L5 : MonoBehaviour, IGetLevelDataInterface {
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
    spawner.spawnEnemyInMap("GigaArmored", 0f, 9f, true);
    yield return new WaitForSeconds(3f);
    spawner.AllTriggerEnemiesCleared();
  }
  IEnumerator wave2() {
    for (int i = 0; i < 2; i++) {
      spawner.spawnEnemy("Vessel", Random.Range(-5f, 5f), 10f);
      yield return new WaitForSeconds(5f);
    }
    spawner.spawnEnemyInMap("Outlier", 5f, 10f, false);
    spawner.spawnEnemyInMap("Outlier", -5f, 10f, false);
    for (int i = 0; i < 3; i++) {
      spawner.spawnEnemy("Vessel", Random.Range(-5f, 5f), 10f);
      yield return new WaitForSeconds(5f);
    }
    spawner.AllTriggerEnemiesCleared();
  }
  IEnumerator wave3() {
    for (int i = 0; i < 20; i++) {
      spawner.spawnEnemy("GigaArmored", Random.Range(-5f, 5f), 10f);
    }
    yield return new WaitForSeconds(5f);
    spawner.spawnEnemy("MesoOutlier", Random.Range(-5f, 5f), 10f, LevelSpawner.addToList.Specific, true);
    while (spawner.setEnemies.Count > 0) {
      spawner.spawnEnemy("Vessel", Random.Range(-5f, 5f), 10f);
      yield return new WaitForSeconds(5f);
    }
    spawner.LastWaveEnemiesCleared();
  }
}
