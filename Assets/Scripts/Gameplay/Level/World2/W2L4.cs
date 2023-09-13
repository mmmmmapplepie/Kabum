using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W2L4 : MonoBehaviour, IGetLevelDataInterface {
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
    audio.ChangeBGM("World2");
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

  bool finalWaveDone = false;
  string[] nano = new string[3] { "NanoBasic", "NanoArmored", "NanoShield" };
  string[] micro = new string[3] { "MicroBasic", "MicroArmored", "MicroShield" };
  string[] kilo = new string[3] { "KiloBasic", "KiloArmored", "KiloShield" };

  IEnumerator ContinuousSpawn(string[] names) {
    while (!finalWaveDone) {
      spawner.spawnEnemy(names[Random.Range(0, 3)], Random.Range(-5f, 5f), 10f);
      yield return new WaitForSeconds(1f);
    }
  }
  IEnumerator wave1() {
    StartCoroutine(ContinuousSpawn(nano));
    yield return new WaitForSeconds(10f);
    spawner.waveCleared();
  }
  IEnumerator wave2() {
    StartCoroutine(ContinuousSpawn(micro));
    yield return new WaitForSeconds(20f);
    spawner.waveCleared();
  }
  IEnumerator wave3() {
    StartCoroutine(ContinuousSpawn(kilo));
    yield return new WaitForSeconds(30f);
    spawner.waveCleared();
  }
  IEnumerator wave4() {
    for (int i = 0; i < 50; i++) {
      spawner.spawnEnemyInMap(micro[Random.Range(0, 3)], Random.Range(-5f, 5f), 9f, false);
      yield return new WaitForSeconds(0.01f);
    }
    finalWaveDone = true;
    yield return new WaitForSeconds(5f);
    spawner.LastWaveEnemiesCleared();
  }
}
