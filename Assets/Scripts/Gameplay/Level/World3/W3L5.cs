using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W3L5 : MonoBehaviour, IGetLevelDataInterface {
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
  string[] rank = new string[3] { "", "Meso", "Macro" };
  string[] baserank = new string[6] { "Nano", "Micro", "Kilo", "Mega", "Giga", "Ultimate" };
  IEnumerator wave1() {
    int s = 0;
    while (s < 20) {
      s++;
      spawner.spawnEnemy(rank[Random.Range(0, 3)] + "Zipper", 4f, 10f);
      yield return new WaitForSeconds(0.8f);
    }
    spawner.AllTriggerEnemiesCleared();
  }

  IEnumerator wave2() {
    int s = 0;
    while (s < 30) {
      s++;
      spawner.spawnEnemy(rank[Random.Range(0, 3)] + "Enigma", -5f, 10f);
      yield return new WaitForSeconds(0.5f);
    }
    spawner.AllTriggerEnemiesCleared();
  }
  IEnumerator wave3() {
    int s = 0;
    while (s < 20) {
      s++;
      spawner.spawnEnemy(rank[Random.Range(0, 3)] + "Zipper", 4f, 10f);
      spawner.spawnEnemy(rank[Random.Range(0, 3)] + "Enigma", -5f, 10f);
      spawner.spawnEnemy(baserank[Random.Range(0, 6)] + "Basic", 0f, 10f);
      yield return new WaitForSeconds(0.5f);
    }

    for (int i = 0; i < 40; i++) {
      spawner.spawnEnemy(baserank[Random.Range(0, 6)] + "Basic", 0f, 10f, LevelSpawner.addToList.Specific, true);
      yield return new WaitForSeconds(0.1f);
    }

    while (spawner.setEnemies.Count > 0) {
      spawner.spawnEnemy(rank[Random.Range(0, 3)] + "Zipper", 4f, 10f);
      spawner.spawnEnemy(rank[Random.Range(0, 3)] + "Enigma", -5f, 10f);
      yield return new WaitForSeconds(1f);
    }
    spawner.LastWaveEnemiesCleared();
  }
}
