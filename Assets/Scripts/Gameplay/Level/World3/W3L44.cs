using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W3L44 : MonoBehaviour, IGetLevelDataInterface {
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
  string[] btype = new string[7] { "Booster", "Havoc", "Protector", "Maintainer", "Armory", "Disruptor", "Jammer" };
  IEnumerator nspawner() {
    while (spawner.setEnemies.Count > 0 || !done) {
      spawner.spawnEnemy(rank[Random.Range(2, 6)] + basetype[Random.Range(0, 3)], spawner.ranXPos(), 10f);
      yield return new WaitForSeconds(Random.Range(0f, 2f));
    }
  }
  IEnumerator buff() {
    while (spawner.setEnemies.Count > 0 || !done) {
      spawner.spawnEnemyInMap("Hyper" + btype[Random.Range(0, 7)], spawner.ranXPos(), Random.Range(0f, 10f), true);
      yield return new WaitForSeconds(Random.Range(0f, 10f));
    }
  }
  IEnumerator wave1() {
    StartCoroutine(nspawner());
    yield return new WaitForSeconds(20f);
    spawner.spawnEnemyInMap("HyperJammer", spawner.ranXPos(), 7f, true);
    spawner.spawnEnemyInMap("HyperJammer", spawner.ranXPos(), 7f, true);
    spawner.spawnEnemyInMap("HyperDisruptor", spawner.ranXPos(), 7f, true);
    spawner.spawnEnemyInMap("HyperDisruptor", spawner.ranXPos(), 7f, true);
    yield return new WaitForSeconds(10f);
    StartCoroutine(buff());
    yield return new WaitForSeconds(10f);
    spawner.waveCleared();
  }

  IEnumerator wave2() {
    spawner.spawnEnemyInMap("MacroCore", 5f, 0f, true, LevelSpawner.addToList.Specific, true);
    spawner.spawnEnemyInMap("Leviathan", -5f, 0f, true, LevelSpawner.addToList.Specific, true);
    yield return new WaitForSeconds(30f);
    spawner.waveCleared();
  }

  IEnumerator wave3() {
    spawner.spawnEnemyInMap("HyperCore", 5f, 2f, true, LevelSpawner.addToList.Specific, true);
    spawner.spawnEnemyInMap("Behemoth", -5f, 2f, true, LevelSpawner.addToList.Specific, true);
    yield return new WaitForSeconds(5f);
    done = true;
    spawner.LastWaveEnemiesCleared();
  }
}
