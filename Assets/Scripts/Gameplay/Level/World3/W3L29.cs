using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W3L29 : MonoBehaviour, IGetLevelDataInterface {
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
  IEnumerator nspawner() {
    while (spawner.setEnemies.Count > 0 || !done) {
      spawner.spawnEnemyInMap(rank[Random.Range(2, 4)] + "Basic", 0f, 7f, false);
      yield return new WaitForSeconds(Random.Range(0f, 2f));
    }
  }
  IEnumerator wave1() {
    int i = 0;
    while (i < 30) {
      i++;
      spawner.spawnEnemy(rank[Random.Range(2, 4)] + "Basic", 0f, 10f);
      yield return new WaitForSeconds(0.01f);
    }
    yield return new WaitForSeconds(5f);
    i = 0;
    while (i < 40) {
      i++;
      spawner.spawnEnemy(rank[Random.Range(2, 4)] + "Basic", 0f, 10f);
      yield return new WaitForSeconds(0.01f);
    }
    yield return new WaitForSeconds(10f);
    i = 0;
    while (i < 50) {
      i++;
      spawner.spawnEnemy(rank[Random.Range(2, 4)] + "Basic", 0f, 10f);
      yield return new WaitForSeconds(0.01f);
    }
    yield return new WaitForSeconds(15f);
    i = 0;
    while (i < 60) {
      i++;
      spawner.spawnEnemy(rank[Random.Range(2, 4)] + "Basic", 0f, 10f);
      yield return new WaitForSeconds(0.01f);
    }
    spawner.AllTriggerEnemiesCleared();
  }
  IEnumerator wave2() {
    StartCoroutine(nspawner());
    yield return new WaitForSeconds(20f);
    spawner.waveCleared();
  }
  IEnumerator wave3() {
    spawner.spawnEnemyInMap("HyperJammer", 3f, 6f, true, LevelSpawner.addToList.Specific, true);
    spawner.spawnEnemyInMap("HyperJammer", -3f, 6f, true, LevelSpawner.addToList.Specific, true);
    yield return new WaitForSeconds(5f);
    done = true;
    spawner.LastWaveEnemiesCleared();
  }



}
