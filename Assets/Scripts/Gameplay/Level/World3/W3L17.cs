using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W3L17 : MonoBehaviour, IGetLevelDataInterface {
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
  string[] rank = new string[4] { "", "Meso", "Macro", "Hyper" };
  string[] basicrank = new string[6] { "Nano", "Micro", "Kilo", "Mega", "Giga", "Ultimate" };
  IEnumerator wave1() {
    int i = 0;
    while (i < 40) {
      i++;
      string r = basicrank[Random.Range(0, 6)];
      bool big = false;
      if (r == "Giga" || r == "Ultimate") {
        big = true;
      }
      spawner.spawnEnemyInMap(r + "Shield", spawner.ranXPos(), 0f, big);
    }
    yield return new WaitForSeconds(10f);
    i = 0;
    while (i < 40) {
      i++;
      string r = basicrank[Random.Range(0, 6)];
      bool big = false;
      if (r == "Giga" || r == "Ultimate") {
        big = true;
      }
      spawner.spawnEnemyInMap(r + "Shield", spawner.ranXPos(), 0f, big);
    }
    spawner.AllTriggerEnemiesCleared();
  }
  IEnumerator wave2() {
    int i = 0;
    while (i < 40) {
      i++;
      spawner.spawnEnemy(rank[Random.Range(0, 4)] + "Reflector", spawner.ranXPos(), 10f);
    }
    yield return new WaitForSeconds(20f);
    spawner.waveCleared();
  }

  IEnumerator spawn() {
    while (spawner.setEnemies.Count > 0) {
      string r = basicrank[Random.Range(0, 6)];
      bool big = false;
      if (r == "Giga" || r == "Ultimate") {
        big = true;
      }
      spawner.spawnEnemyInMap(r + "Shield", spawner.ranXPos(), 0f, big);
      yield return new WaitForSeconds(2f);
    }
  }
  IEnumerator wave3() {
    spawner.spawnEnemyInMap("HyperArmory", 0f, 10f, true, LevelSpawner.addToList.Specific, true);
    spawner.spawnEnemyInMap("HyperArmory", 0f, 10f, true, LevelSpawner.addToList.Specific, true);
    spawner.spawnEnemyInMap("HyperArmory", 0f, 10f, true, LevelSpawner.addToList.Specific, true);
    spawner.spawnEnemyInMap("HyperArmory", 0f, 10f, true, LevelSpawner.addToList.Specific, true);
    spawner.spawnEnemyInMap("HyperArmory", 0f, 10f, true, LevelSpawner.addToList.Specific, true);
    yield return new WaitForSeconds(5f);
    yield return StartCoroutine(spawn());
    spawner.LastWaveEnemiesCleared();
  }
}
