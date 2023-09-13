using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W3L11 : MonoBehaviour, IGetLevelDataInterface {
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
  string[] rank = new string[6] { "Nano", "Micro", "Kilo", "Mega", "Giga", "Ultimate" };

  IEnumerator spawn(string name, int num) {
    int summon = 0;
    while (summon < num) {
      summon++;
      spawner.spawnEnemy(name, spawner.ranXPos(), 10f, LevelSpawner.addToList.Specific, true);
      yield return new WaitForSeconds(Random.Range(1f, 2f));
    }
  }
  bool doneSpawn = false;
  IEnumerator spawnbase() {
    while (!doneSpawn || spawner.setEnemies.Count > 0) {
      spawner.spawnEnemy(rank[Random.Range(0, 6)] + "Shield", spawner.ranXPos(), 10f);
      yield return new WaitForSeconds(Random.Range(0f, 3f));
    }
  }
  IEnumerator wave1() {
    StartCoroutine(spawn("Maintainer", 5));
    StartCoroutine(spawn("Protector", 5));
    StartCoroutine(spawnbase());
    yield return new WaitForSeconds(20f);
    yield return StartCoroutine(spawn("Maintainer", 3));
    yield return StartCoroutine(spawn("Protector", 3));
    doneSpawn = true;
    spawner.LastWaveEnemiesCleared();
  }
}
