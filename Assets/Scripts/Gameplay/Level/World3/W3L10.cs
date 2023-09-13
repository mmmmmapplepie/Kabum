using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W3L10 : MonoBehaviour, IGetLevelDataInterface {
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
  string[] highrank = new string[2] { "Macro", "Hyper" };

  IEnumerator wave1() {
    spawner.spawnEnemyInMap("MaxCoupladSeeker", -3f, 0f, true, LevelSpawner.addToList.Specific, true);
    spawner.spawnEnemyInMap("MaxCoupladFollower", -3f, 0f, true, LevelSpawner.addToList.Specific, true);
    yield return new WaitForSeconds(10f);
    spawner.waveCleared();
  }
  IEnumerator wave2() {
    int summon = 0;
    while (summon < 150) {
      summon++;
      spawner.spawnEnemy(rank[Random.Range(0, 3)] + "Shifter", spawner.ranXPos(), 10f);
      yield return new WaitForSeconds(0.2f);
    }
    spawner.AllTriggerEnemiesCleared();
  }

  IEnumerator eliteSpawn(string name, float periodmin, bool bigsize) {
    float t = Time.time;
    while (spawner.setEnemies.Count > 0 || Time.time < t + 30f) {
      spawner.spawnEnemyInMap(highrank[Random.Range(0, 2)] + name, spawner.ranXPos(), Random.Range(-2f, 10f), bigsize);
      yield return new WaitForSeconds(Random.Range(periodmin, periodmin + 3f));
    }
  }
  IEnumerator wave3() {
    StartCoroutine(eliteSpawn("Ticker", 2f, true));
    StartCoroutine(eliteSpawn("Outlier", 4f, false));
    yield return new WaitForSeconds(35f);
    spawner.waveCleared();
  }
  IEnumerator wave4() {
    while (spawner.setEnemies.Count > 0) {
      spawner.spawnEnemy(rank[Random.Range(0, 3)] + "Shifter", spawner.ranXPos(), 10f);
      yield return new WaitForSeconds(Random.Range(1f, 3f));
    }
    spawner.LastWaveEnemiesCleared();
  }
}
