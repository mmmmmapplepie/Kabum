using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W2L7 : MonoBehaviour, IGetLevelDataInterface {
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

  bool lastWaveDone = false;
  IEnumerator vesselSpawner() {
    while (!lastWaveDone) {
      spawner.spawnEnemyInMap("Vessel", Random.Range(-5f, 5f), Random.Range(7f, 10f), true);
      yield return new WaitForSeconds(3f);
    }
  }
  IEnumerator wave1() {
    StartCoroutine(vesselSpawner());
    yield return new WaitForSeconds(20f);
    for (int i = 0; i < 10; i++) {
      spawner.spawnEnemy("Vessel", Random.Range(-5f, 5f), 10f);
    }
    yield return new WaitForSeconds(30f);
    for (int i = 0; i < 12; i++) {
      spawner.spawnEnemy("Vessel", Random.Range(-5f, 5f), 10f);
    }
    spawner.waveCleared();
  }
  IEnumerator wave2() {
    yield return new WaitForSeconds(15f);
    for (int i = 0; i < 10; i++) {
      spawner.spawnEnemy("MesoVessel", Random.Range(-5f, 5f), 10f);
    }
    yield return new WaitForSeconds(30f);
    lastWaveDone = true;
    for (int i = 0; i < 12; i++) {
      spawner.spawnEnemy("MesoVessel", Random.Range(-5f, 5f), 10f);
    }
    spawner.LastWaveEnemiesCleared();
  }
}
