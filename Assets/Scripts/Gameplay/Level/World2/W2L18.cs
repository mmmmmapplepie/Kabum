using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W2L18 : MonoBehaviour, IGetLevelDataInterface {
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
    spawner.spawnEnemy("MacroTicker", 3f, 10f);
    yield return new WaitForSeconds(5f);
    spawner.spawnEnemy("MacroTicker", -3f, 10f);
    yield return new WaitForSeconds(5f);
    spawner.spawnEnemy("MacroTicker", 3f, 10f);
    yield return new WaitForSeconds(5f);
    spawner.spawnEnemy("MacroTicker", -3f, 10f);
    yield return new WaitForSeconds(5f);
    spawner.spawnEnemy("MacroTicker", 4f, 10f);
    yield return new WaitForSeconds(4f);
    spawner.spawnEnemy("MacroTicker", -4f, 10f);
    yield return new WaitForSeconds(4f);
    spawner.spawnEnemy("MacroTicker", 4f, 10f);
    yield return new WaitForSeconds(4f);
    spawner.spawnEnemy("MacroTicker", -4f, 10f);
    yield return new WaitForSeconds(4f);
    spawner.spawnEnemy("MacroTicker", 5f, 10f);
    yield return new WaitForSeconds(3f);
    spawner.spawnEnemy("MacroTicker", -5f, 10f);
    yield return new WaitForSeconds(3f);
    spawner.spawnEnemy("MacroTicker", 5f, 10f);
    yield return new WaitForSeconds(3f);
    spawner.spawnEnemy("MacroTicker", -5f, 10f);
    spawner.AllTriggerEnemiesCleared();
  }

  string[] pre = new string[3] { "", "Meso", "Macro" };

  IEnumerator teleporters() {
    while (spawner.setEnemies.Count > 0) {
      for (int i = 0; i < 5; i++) {
        string ene = pre[Random.Range(0, 3)] + "Teleporter";
        spawner.spawnEnemyInMap(ene, spawner.ranXPos(), Random.Range(0f, 10f), false);
      }
      yield return new WaitForSeconds(8f);
    }
  }
  IEnumerator outliers() {
    while (spawner.setEnemies.Count > 0) {
      for (int i = 0; i < 2; i++) {
        spawner.spawnEnemy("MesoOutlier", spawner.ranXPos(), 10f);
      }
      yield return new WaitForSeconds(Random.Range(15f, 25f));
    }
  }
  IEnumerator tickers() {
    while (spawner.setEnemies.Count > 0) {
      for (int i = 0; i < 2; i++) {
        spawner.spawnEnemy("MacroTicker", spawner.ranXPos(), 10f);
      }
      yield return new WaitForSeconds(Random.Range(10f, 12f));
    }
  }
  IEnumerator wave2() {
    spawner.spawnEnemyInMap("CoupladSeeker", 5f, 0f, true, LevelSpawner.addToList.Specific, true);
    spawner.spawnEnemyInMap("CoupladFollower", -5f, 0f, true, LevelSpawner.addToList.Specific, true);
    yield return new WaitForSeconds(8f);
    StartCoroutine(teleporters());
    StartCoroutine(outliers());
    yield return new WaitForSeconds(4f);
    StartCoroutine(tickers());
    spawner.LastWaveEnemiesCleared();
  }
}
