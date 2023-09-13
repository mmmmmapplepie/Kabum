using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W3L41 : MonoBehaviour, IGetLevelDataInterface {
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
  string[] basetype = new string[3] { "Basic", "Armored", "Shield" };
  string[] highrank = new string[4] { "", "Meso", "Macro", "Hyper" };
  string[] type = new string[2] { "Vessel", "Ticker" };
  string[] btype = new string[7] { "Booster", "Havoc", "Protector", "Maintainer", "Armory", "Disruptor", "Jammer" };

  IEnumerator wave1() {
    spawner.spawnEnemyInMap("HyperBooster", 5f, 10f, true);
    spawner.spawnEnemyInMap("HyperBooster", -5f, 10f, true);
    yield return new WaitForSeconds(1f);
    spawner.spawnEnemyInMap("HyperHavoc", -4f, 10f, true);
    spawner.spawnEnemyInMap("HyperHavoc", 4f, 10f, true);
    yield return new WaitForSeconds(1f);
    spawner.spawnEnemyInMap("HyperProtector", -3f, 10f, true);
    spawner.spawnEnemyInMap("HyperProtector", 3f, 10f, true);
    yield return new WaitForSeconds(1f);
    spawner.spawnEnemyInMap("HyperMaintainer", -2f, 10f, true);
    spawner.spawnEnemyInMap("HyperMaintainer", 2f, 10f, true);
    yield return new WaitForSeconds(1f);
    spawner.spawnEnemyInMap("HyperArmory", -1f, 10f, true);
    spawner.spawnEnemyInMap("HyperArmory", 1f, 10f, true);
    yield return new WaitForSeconds(1f);
    spawner.spawnEnemyInMap("HyperJammer", 0f, 10f, true);
    spawner.spawnEnemyInMap("HyperJammer", 0f, 10f, true);
    yield return new WaitForSeconds(1f);
    spawner.spawnEnemyInMap("HyperDisruptor", 0f, 10f, true);
    spawner.spawnEnemyInMap("HyperDisruptor", 0f, 10f, true);
    yield return new WaitForSeconds(5f);
    int i = 0;
    while (i < 20) {
      i++;
      spawner.spawnEnemyInMap("HyperVessel", spawner.ranXPos(), Random.Range(5f, 8f), true);
    }
    spawner.AllEnemiesCleared();
  }

  IEnumerator ultimates() {
    while (!done) {
      spawner.spawnEnemy("Ultimate" + basetype[Random.Range(0, 3)], spawner.ranXPos(), 10f);
      yield return new WaitForSeconds(Random.Range(0f, 10f));
    }
  }
  IEnumerator tick() {
    while (!done) {
      spawner.spawnEnemy("Hyper" + type[Random.Range(0, 2)], spawner.ranXPos(), 10f);
      yield return new WaitForSeconds(Random.Range(0f, 10f));
    }
  }
  IEnumerator buff() {
    while (!done) {
      spawner.spawnEnemy("Hyper" + btype[Random.Range(0, 7)], spawner.ranXPos(), 10f);
      yield return new WaitForSeconds(Random.Range(0f, 20f));
    }
  }
  IEnumerator wave2() {
    StartCoroutine(ultimates());
    StartCoroutine(tick());
    StartCoroutine(buff());
    yield return new WaitForSeconds(10f);
    spawner.spawnEnemyInMap("HyperBooster", spawner.ranXPos(), 8f, true);
    yield return new WaitForSeconds(0.5f);
    spawner.spawnEnemyInMap("HyperHavoc", spawner.ranXPos(), 8f, true);
    yield return new WaitForSeconds(0.5f);
    spawner.spawnEnemyInMap("HyperProtector", spawner.ranXPos(), 8f, true);
    yield return new WaitForSeconds(0.5f);
    spawner.spawnEnemyInMap("HyperMaintainer", spawner.ranXPos(), 8f, true);
    yield return new WaitForSeconds(0.5f);
    spawner.spawnEnemyInMap("HyperArmory", spawner.ranXPos(), 8f, true);
    yield return new WaitForSeconds(0.5f);
    spawner.spawnEnemyInMap("HyperJammer", spawner.ranXPos(), 8f, true);
    yield return new WaitForSeconds(0.5f);
    spawner.spawnEnemyInMap("HyperDisruptor", spawner.ranXPos(), 8f, true);
    yield return new WaitForSeconds(60f);
    done = true;
    spawner.LastWaveEnemiesCleared();
  }
}
