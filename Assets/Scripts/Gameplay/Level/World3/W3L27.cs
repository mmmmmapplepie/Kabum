using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W3L27 : MonoBehaviour, IGetLevelDataInterface {
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
  IEnumerator wave1() {
    spawner.spawnEnemy("Armory", spawner.ranXPos(), 10f);
    yield return new WaitForSeconds(5f);
    spawner.spawnEnemy("Armory", spawner.ranXPos(), 10f);
    yield return new WaitForSeconds(5f);
    spawner.spawnEnemy("Armory", spawner.ranXPos(), 10f);
    yield return new WaitForSeconds(5f);
    spawner.spawnEnemy("Armory", spawner.ranXPos(), 10f);
    yield return new WaitForSeconds(5f);
    spawner.spawnEnemy("Armory", spawner.ranXPos(), 10f);
    yield return new WaitForSeconds(5f);
    spawner.waveCleared();
  }
  bool done = false;
  IEnumerator nspawner() {
    while (spawner.setEnemies.Count > 0 || !done) {
      spawner.spawnEnemy(rank[Random.Range(0, 6)] + "Armored", spawner.ranXPos(), 10f);
      yield return new WaitForSeconds(Random.Range(2f, 3f));
    }
  }
  IEnumerator cspawner() {
    while (spawner.setEnemies.Count > 0 || !done) {
      spawner.spawnEnemy("Core", spawner.ranXPos(), 10f);
      yield return new WaitForSeconds(Random.Range(20f, 25f));
    }
  }
  IEnumerator wave2() {
    StartCoroutine(cspawner());
    StartCoroutine(nspawner());
    yield return new WaitForSeconds(5f);
    spawner.spawnEnemyInMap("HyperArmory", -5f, 5f, true, LevelSpawner.addToList.Specific, true);
    spawner.spawnEnemyInMap("HyperArmory", 5f, 5f, true, LevelSpawner.addToList.Specific, true);
    spawner.spawnEnemyInMap("HyperArmory", 0f, 5f, true, LevelSpawner.addToList.Specific, true);
    done = true;
    spawner.LastWaveEnemiesCleared();
  }
}
