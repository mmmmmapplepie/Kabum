using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W3L40 : MonoBehaviour, IGetLevelDataInterface {
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


  string[] basetype = new string[3] { "Basic", "Armored", "Shield" };
  IEnumerator wave1() {
    for (int i = -5; i <= 5f; i += 10) {
      for (int j = 10; j > 0; j--) {
        float x = i < 0 ? i + (10 - j) * 4f / 9f : i - (10 - j) * 4f / 9f;
        spawner.spawnEnemyInMap("HyperArmory", x, j - 1f, true);
      }
    }
    yield return new WaitForSeconds(15f);
    spawner.AllTriggerEnemiesCleared();
  }

  bool done = false;
  IEnumerator megas() {
    while (!done || spawner.setEnemies.Count > 0) {
      spawner.spawnEnemy("Mega" + basetype[Random.Range(0, 3)], spawner.ranXPos(), 10f);
      yield return new WaitForSeconds(Random.Range(0, 6f));
    }
  }
  IEnumerator wave2() {
    spawner.spawnEnemyInMap("MaxCoupladSeeker", 2f, 7f, true, LevelSpawner.addToList.Specific, true);
    spawner.spawnEnemyInMap("MaxCoupladFollower", -2f, 7f, true, LevelSpawner.addToList.Specific, true);
    spawner.spawnEnemyInMap("CoupladSeeker", 5f, 6f, true, LevelSpawner.addToList.Specific, true);
    spawner.spawnEnemyInMap("CoupladFollower", -5f, 6f, true, LevelSpawner.addToList.Specific, true);
    StartCoroutine(megas());
    yield return new WaitForSeconds(5f);
    spawner.waveCleared();
  }

  IEnumerator wave3() {
    yield return new WaitForSeconds(5f);
    spawner.spawnEnemyInMap("HyperProtector", 5f, 10f, true);
    spawner.spawnEnemyInMap("HyperProtector", 5f, 10f, true);
    spawner.spawnEnemyInMap("HyperMaintainer", 3f, 10f, true);
    spawner.spawnEnemyInMap("HyperMaintainer", 3f, 10f, true);
    spawner.spawnEnemyInMap("HyperArmory", 1f, 10f, true);
    spawner.spawnEnemyInMap("HyperArmory", 1f, 10f, true);
    yield return new WaitForSeconds(30f);
    done = true;
    spawner.LastWaveEnemiesCleared();
  }



}
