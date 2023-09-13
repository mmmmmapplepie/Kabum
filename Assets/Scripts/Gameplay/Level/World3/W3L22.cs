using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W3L22 : MonoBehaviour, IGetLevelDataInterface {
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
    spawner.spawnEnemy("HyperProtector", 5f, 10f);
    yield return new WaitForSeconds(5f);
    spawner.spawnEnemy("HyperProtector", -5f, 10f);
    yield return new WaitForSeconds(5f);
    spawner.spawnEnemy("HyperProtector", 0f, 10f);
    yield return new WaitForSeconds(10f);
    spawner.waveCleared();
  }

  string[] rank = new string[6] { "Nano", "Micro", "Kilo", "Mega", "Giga", "Ultimate" };
  string[] highrank = new string[4] { "", "Meso", "Macro", "Hyper" };
  IEnumerator wave2() {
    int i = 0;
    while (i < 80) {
      i++;
      spawner.spawnEnemy(rank[Random.Range(0, 6)] + "Shield", spawner.ranXPos(), 10f);
      yield return new WaitForSeconds(0.25f);
    }
    spawner.spawnEnemyInMap("HyperProtector", 0f, 5f, true);
    i = 0;
    while (i < 60) {
      i++;
      spawner.spawnEnemy(highrank[Random.Range(0, 4)] + "Teleporter", spawner.ranXPos(), 10f);
      yield return new WaitForSeconds(0.25f);
    }
    yield return new WaitForSeconds(10f);
    spawner.waveCleared();
  }

  IEnumerator wave3() {
    int i = 0;
    while (i < 30) {
      i++;
      spawner.spawnEnemy("UltimateShield", spawner.ranXPos(), 10f);
    }
    yield return new WaitForSeconds(10f);
    spawner.spawnEnemy("HyperProtector", 0f, 10f);
    spawner.spawnEnemy("HyperProtector", 0f, 10f);
    spawner.LastWaveEnemiesCleared();
  }
}
