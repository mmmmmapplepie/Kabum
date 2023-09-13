using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W3L45 : MonoBehaviour, IGetLevelDataInterface {
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
  string[] rank = new string[6] { "Nano", "Micro", "Kilo", "Mega", "Giga", "Ultimate" };
  string[] basetype = new string[3] { "Basic", "Armored", "Shield" };
  string[] highrank = new string[4] { "", "Meso", "Macro", "Hyper" };
  string[] type = new string[2] { "Outlier", "Ticker" };
  IEnumerator wave1() {
    spawner.spawnEnemy("HyperTicker", 5f, 10f);
    spawner.spawnEnemy("HyperOutlier", 5f, 10f);
    yield return null;
    spawner.AllTriggerEnemiesCleared();
  }
  IEnumerator basic() {
    while (!done || spawner.setEnemies.Count > 0) {
      string r = rank[Random.Range(2, 5)];
      bool big = r == "Giga" ? true : false;
      spawner.spawnEnemyInMap(r + basetype[Random.Range(0, 3)], spawner.ranXPos(), Random.Range(-3f, 10f), big);
      yield return new WaitForSeconds(Random.Range(0f, 4f));
    }
  }
  IEnumerator wave2() {
    StartCoroutine(basic());
    yield return new WaitForSeconds(20f);
    spawner.waveCleared();
  }

  IEnumerator wave3() {
    spawner.spawnEnemyInMap("Maxima", 0f, 5f, true, LevelSpawner.addToList.Specific, true);
    yield return new WaitForSeconds(30f);
    spawner.waveCleared();
  }

  IEnumerator tick() {
    while (!done || spawner.setEnemies.Count > 0) {
      float side = Random.Range(-1f, 1f);
      spawner.spawnEnemy("HyperTicker", -5f * side, 10f);
      spawner.spawnEnemy("HyperOutlier", 5f * side, 10f);
      yield return new WaitForSeconds(Random.Range(4f, 12f));
    }
  }
  IEnumerator wave4() {
    StartCoroutine(tick());
    spawner.spawnEnemyInMap("Booster", 0f, 0f, true);
    spawner.spawnEnemyInMap("Booster", 0f, 0f, true);
    spawner.spawnEnemyInMap("Booster", 0f, 0f, true);
    spawner.spawnEnemyInMap("Booster", 0f, 0f, true);
    spawner.spawnEnemyInMap("Booster", 0f, 0f, true);
    yield return new WaitForSeconds(30f);
    spawner.waveCleared();
  }
  IEnumerator wave5() {
    spawner.spawnEnemyInMap("HyperBooster", 0f, 0f, true);
    spawner.spawnEnemyInMap("HyperBooster", 0f, 0f, true);
    spawner.spawnEnemyInMap("HyperBooster", 0f, 0f, true);
    spawner.spawnEnemyInMap("HyperBooster", 0f, 0f, true);
    spawner.spawnEnemyInMap("HyperBooster", 0f, 0f, true);
    yield return new WaitForSeconds(5f);
    done = true;
    spawner.LastWaveEnemiesCleared();
  }




}
