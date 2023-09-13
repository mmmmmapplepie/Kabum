using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W3L38 : MonoBehaviour, IGetLevelDataInterface {
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


  string[] highrank = new string[4] { "", "Meso", "Macro", "Hyper" };
  bool done = false;
  IEnumerator zip() {
    while (!done || spawner.setEnemies.Count > 0) {
      spawner.spawnEnemy(highrank[Random.Range(2, 4)] + "Zipper", 5f, 10f);
      yield return new WaitForSeconds(1f);
    }
  }
  IEnumerator tick() {
    while (!done || spawner.setEnemies.Count > 0) {
      spawner.spawnEnemy(highrank[Random.Range(2, 4)] + "Ticker", -5f, 10f);
      yield return new WaitForSeconds(8f);
    }
  }
  IEnumerator wave1() {
    StartCoroutine(zip());
    yield return new WaitForSeconds(20f);
    spawner.waveCleared();
  }

  IEnumerator wave2() {
    StartCoroutine(tick());
    yield return new WaitForSeconds(20f);
    spawner.waveCleared();
  }

  IEnumerator wave3() {
    int i = 0;
    while (i < 20) {
      i++;
      spawner.spawnEnemyInMap(highrank[Random.Range(2, 4)] + "Reflector", spawner.ranXPos(), Random.Range(0f, 10f), false, LevelSpawner.addToList.Specific, true);
    }
    yield return new WaitForSeconds(5f);
    done = true;
    spawner.LastWaveEnemiesCleared();
  }
}
