using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W3L35 : MonoBehaviour, IGetLevelDataInterface {
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

  IEnumerator wave1() {
    spawner.spawnEnemyInMap("Carrier", -4f, 8f, true);
    spawner.spawnEnemyInMap("Carrier", -1f, 8f, true);
    spawner.spawnEnemyInMap("Carrier", 1f, 8f, true);
    spawner.spawnEnemyInMap("Carrier", 4f, 8f, true);
    yield return new WaitForSeconds(15f);
    spawner.waveCleared();
  }

  IEnumerator wave2() {
    spawner.spawnEnemyInMap("Core", -4f, 8f, true);
    spawner.spawnEnemyInMap("Core", -1f, 8f, true);
    spawner.spawnEnemyInMap("Core", 1f, 8f, true);
    spawner.spawnEnemyInMap("Core", 4f, 8f, true);
    yield return new WaitForSeconds(20f);
    spawner.waveCleared();
  }
  bool done = false;
  string[] highrank = new string[4] { "", "Meso", "Macro", "Hyper" };
  string[] carrierrank = new string[4] { "Carrier", "Colossus", "Macro", "Hyper" };
  IEnumerator spawncore() {
    while (!done || spawner.setEnemies.Count > 0) {
      spawner.spawnEnemyInMap(highrank[Random.Range(0, 2)] + "Core", spawner.ranXPos(), Random.Range(8f, 10f), true);
      yield return new WaitForSeconds(Random.Range(10f, 30f));
    }
  }
  IEnumerator spawncarrier() {
    while (!done || spawner.setEnemies.Count > 0) {
      spawner.spawnEnemyInMap(carrierrank[Random.Range(0, 2)], spawner.ranXPos(), Random.Range(8f, 10f), true);
      yield return new WaitForSeconds(Random.Range(10f, 30f));
    }
  }
  IEnumerator wave3() {
    StartCoroutine(spawncore());
    StartCoroutine(spawncarrier());
    yield return new WaitForSeconds(20f);
    spawner.waveCleared();
  }

  IEnumerator wave4() {
    spawner.spawnEnemyInMap("HyperCore", 5f, 10f, true, LevelSpawner.addToList.Specific, true);
    spawner.spawnEnemyInMap("Behemoth", -5f, 10f, true, LevelSpawner.addToList.Specific, true);
    yield return new WaitForSeconds(20f);
    spawner.waveCleared();
  }
  IEnumerator wave5() {
    spawner.spawnEnemyInMap("Colossus", 1f, 8f, true);
    spawner.spawnEnemyInMap("Colossus", 4f, 8f, true);
    spawner.spawnEnemyInMap("MesoCore", -4f, 8f, true);
    spawner.spawnEnemyInMap("MesoCore", -1f, 8f, true);
    yield return new WaitForSeconds(5f);
    done = true;
    spawner.LastWaveEnemiesCleared();
  }



}
