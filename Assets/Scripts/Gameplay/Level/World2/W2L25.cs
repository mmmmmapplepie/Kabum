using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W2L25 : MonoBehaviour, IGetLevelDataInterface {
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
    spawner.spawnEnemyInMap("MesoCore", 0f, 10f, true, LevelSpawner.addToList.Specific, true);
    yield return new WaitForSeconds(3f);
    spawner.waveCleared();
  }
  IEnumerator wave2() {
    spawner.spawnEnemyInMap("Colossus", 0f, 10f, true, LevelSpawner.addToList.Specific, true);
    yield return new WaitForSeconds(3f);
    spawner.waveCleared();
  }
  IEnumerator wave3() {
    for (int i = -5; i <= 5; i++) {
      string pre = Random.Range(0, 2) == 0 ? "" : "Meso";
      spawner.spawnEnemyInMap(pre + "Reflector", (float)i, 5f, false);
      yield return new WaitForSeconds(1f);
    }
    yield return new WaitForSeconds(5f);
    for (int i = -5; i <= 5; i = i + 2) {
      string pre = Random.Range(0, 2) == 0 ? "" : "Meso";
      spawner.spawnEnemyInMap(pre + "Outlier", (float)i, 3f, false);
      yield return new WaitForSeconds(1f);
    }
    yield return new WaitForSeconds(20f);
    spawner.waveCleared();
  }

  string[] pre = new string[6] { "Nano", "Micro", "Kilo", "Mega", "Giga", "Ultimate" };
  string[] type = new string[2] { "Shield", "Armored" };
  IEnumerator wave4() {
    for (int i = 0; i < 10; i++) {
      string ran = pre[Random.Range(0, 6)] + type[Random.Range(0, 2)];
      spawner.spawnEnemy(ran, spawner.ranXPos(), 10f);
      yield return new WaitForSeconds(2f);
    }
    for (int i = 0; i < 10; i++) {
      string ran = pre[Random.Range(0, 6)] + type[Random.Range(0, 2)];
      spawner.spawnEnemy(ran, spawner.ranXPos(), 10f);
      yield return new WaitForSeconds(2f);
    }
    spawner.AllTriggerEnemiesCleared();
  }

  IEnumerator outliers() {
    while (spawner.setEnemies.Count > 0) {
      for (int i = -5; i < 5; i++) {
        string pre = Random.Range(0, 2) == 0 ? "" : "Meso";
        spawner.spawnEnemyInMap(pre + "Outlier", (float)i, Random.Range(0f, 10f), false);
      }
      yield return new WaitForSeconds(20f);
    }
  }
  IEnumerator wave5() {
    StartCoroutine(outliers());
    while (spawner.setEnemies.Count > 0) {
      string ran = pre[Random.Range(0, 6)] + type[Random.Range(0, 2)];
      spawner.spawnEnemy(ran, spawner.ranXPos(), 10f);
      yield return new WaitForSeconds(5f);
    }
    spawner.LastWaveEnemiesCleared();
  }
}