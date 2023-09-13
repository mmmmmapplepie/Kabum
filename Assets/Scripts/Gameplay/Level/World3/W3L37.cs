using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W3L37 : MonoBehaviour, IGetLevelDataInterface {
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
  IEnumerator nspawner() {
    while (spawner.setEnemies.Count > 0 || !done) {
      spawner.spawnEnemy(rank[Random.Range(3, 6)] + basetype[Random.Range(0, 3)], spawner.ranXPos(), 10f);
      yield return new WaitForSeconds(Random.Range(1f, 4f));
    }
  }
  IEnumerator wave1() {
    StartCoroutine(nspawner());
    yield return new WaitForSeconds(30f);
    spawner.waveCleared();
  }
  IEnumerator wave2() {
    yield return new WaitForSeconds(5f);
    spawner.spawnEnemyInMap("HyperMaintainer", 5f, 10f, true, LevelSpawner.addToList.Specific, true);
    spawner.spawnEnemyInMap("HyperMaintainer", -5f, 10f, true, LevelSpawner.addToList.Specific, true);
    yield return new WaitForSeconds(5f);
    spawner.spawnEnemyInMap("HyperProtector", 5f, 10f, true, LevelSpawner.addToList.Specific, true);
    spawner.spawnEnemyInMap("HyperProtector", -5f, 10f, true, LevelSpawner.addToList.Specific, true);
    yield return new WaitForSeconds(5f);
    spawner.spawnEnemyInMap("HyperArmory", 5f, 10f, true, LevelSpawner.addToList.Specific, true);
    spawner.spawnEnemyInMap("HyperArmory", -5f, 10f, true, LevelSpawner.addToList.Specific, true);
    yield return new WaitForSeconds(5f);
    spawner.spawnEnemyInMap("HyperDisruptor", 5f, 10f, true, LevelSpawner.addToList.Specific, true);
    spawner.spawnEnemyInMap("HyperDisruptor", -5f, 10f, true, LevelSpawner.addToList.Specific, true);
    yield return new WaitForSeconds(5f);
    done = true;
    spawner.LastWaveEnemiesCleared();
  }


}
