using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W3L26 : MonoBehaviour, IGetLevelDataInterface {
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
  string[] highrank = new string[4] { "", "Meso", "Macro", "Hyper" };
  IEnumerator wave1() {
    spawner.spawnEnemy("UltimateBasic", spawner.ranXPos(), 10f);
    spawner.spawnEnemy("UltimateBasic", spawner.ranXPos(), 10f);
    spawner.spawnEnemy("UltimateBasic", spawner.ranXPos(), 10f);
    spawner.spawnEnemy("UltimateBasic", spawner.ranXPos(), 10f);
    spawner.spawnEnemy("UltimateBasic", spawner.ranXPos(), 10f);
    spawner.spawnEnemy("UltimateBasic", spawner.ranXPos(), 10f);
    spawner.spawnEnemy("UltimateBasic", spawner.ranXPos(), 10f);
    spawner.spawnEnemy("UltimateBasic", spawner.ranXPos(), 10f);
    yield return new WaitForSeconds(5f);
    spawner.AllTriggerEnemiesCleared();
  }

  bool done = false;
  IEnumerator nspawner() {
    while (spawner.setEnemies.Count > 0 || !done) {
      spawner.spawnEnemy("Ultimate" + basetype[Random.Range(0, 3)], spawner.ranXPos(), 10f);
      yield return new WaitForSeconds(Random.Range(4f, 5f));
    }
  }
  IEnumerator hspawner(string name, float period) {
    while (spawner.setEnemies.Count > 0 || !done) {
      spawner.spawnEnemy(name, spawner.ranXPos(), 10f);
      yield return new WaitForSeconds(Random.Range(period, period + period / 4f));
    }
  }
  IEnumerator rspawner(string name, float period) {
    while (spawner.setEnemies.Count > 0 || !done) {
      spawner.spawnEnemy(highrank[Random.Range(0, 4)] + name, spawner.ranXPos(), 10f);
      yield return new WaitForSeconds(Random.Range(period, period + period / 4f));
    }
  }
  IEnumerator wave2() {
    spawner.spawnEnemyInMap("HyperMaintainer", -3f, 10f, true);
    spawner.spawnEnemyInMap("HyperMaintainer", 3f, 10f, true);
    spawner.spawnEnemyInMap("HyperMaintainer", 0f, 10f, true);
    StartCoroutine(nspawner());
    StartCoroutine(hspawner("Maintainer", 10f));
    StartCoroutine(rspawner("Reflector", 10f));
    yield return new WaitForSeconds(10f);
    done = true;
    spawner.AllTriggerEnemiesCleared();
  }











}
