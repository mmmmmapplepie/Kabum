using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W3L39 : MonoBehaviour, IGetLevelDataInterface {
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
  void burst(int num, float y) {
    int i = 0;
    while (i < num) {
      i++;
      spawner.spawnEnemyInMap(rank[Random.Range(1, 3)] + basetype[Random.Range(0, 3)], spawner.ranXPos(), y, false);
    }
  }
  IEnumerator wave1() {
    burst(100, 10f);
    yield return new WaitForSeconds(10f);
    burst(100, 9f);
    yield return new WaitForSeconds(10f);
    burst(100, 8f);
    yield return new WaitForSeconds(10f);
    burst(100, 7f);
    yield return new WaitForSeconds(10f);
    burst(100, 6f);
    burst(100, 5f);
    spawner.AllTriggerEnemiesCleared();
  }
  IEnumerator wave2() {
    burst(50, 5f);
    yield return new WaitForSeconds(5f);
    spawner.spawnEnemyInMap("HyperBooster", 5f, 10f, true);
    spawner.spawnEnemyInMap("HyperHavoc", 0f, 10f, true);
    spawner.spawnEnemyInMap("HyperBooster", -5f, 10f, true);
    burst(100, 4f);
    yield return new WaitForSeconds(5f);
    burst(100, 3f);
    yield return new WaitForSeconds(5f);
    burst(100, 2f);
    yield return new WaitForSeconds(5f);
    burst(100, 1f);
    yield return new WaitForSeconds(5f);
    spawner.waveCleared();
  }
  IEnumerator wave3() {
    spawner.spawnEnemyInMap("HyperJammer", -5f, 10f, true);
    spawner.spawnEnemyInMap("HyperJammer", 5f, 10f, true);
    burst(100, 1f);
    yield return new WaitForSeconds(5f);
    burst(100, 0f);
    yield return new WaitForSeconds(5f);
    burst(100, -1f);
    yield return new WaitForSeconds(5f);
    burst(100, -2f);
    yield return new WaitForSeconds(5f);
    spawner.AllTriggerEnemiesCleared();
  }

  IEnumerator bursts() {
    while (!done || spawner.setEnemies.Count > 0) {
      burst(Random.Range(50, 101), Random.Range(-2f, 1f));
      yield return new WaitForSeconds(Random.Range(4f, 7f));
    }
  }
  IEnumerator wave4() {
    spawner.spawnEnemyInMap("HyperBooster", 5f, 10f, true, LevelSpawner.addToList.Specific, true);
    spawner.spawnEnemyInMap("HyperBooster", -5f, 10f, true, LevelSpawner.addToList.Specific, true);
    spawner.spawnEnemyInMap("HyperHavoc", 3f, 8f, true, LevelSpawner.addToList.Specific, true);
    spawner.spawnEnemyInMap("HyperHavoc", -3f, 8f, true, LevelSpawner.addToList.Specific, true);
    spawner.spawnEnemyInMap("HyperJammer", -1f, 6f, true, LevelSpawner.addToList.Specific, true);
    spawner.spawnEnemyInMap("HyperJammer", 1f, 6f, true, LevelSpawner.addToList.Specific, true);
    yield return new WaitForSeconds(5f);
    done = true;
    spawner.LastWaveEnemiesCleared();
  }




}
