using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W2L12 : MonoBehaviour, IGetLevelDataInterface {
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
  string[] type = new string[3] { "Armored", "Basic", "Shield" };
  bool lastWaveDone = false;
  IEnumerator basicSpawner() {
    while (!lastWaveDone) {
      string name = "Nano" + type[Random.Range(0, 3)];
      spawner.spawnEnemy(name, spawner.ranXPos(), 10f);
      yield return new WaitForSeconds(1f);
    }
  }
  IEnumerator wave1() {
    float x = spawner.ranXPos();
    for (int i = 0; i < 30; i++) {
      spawner.spawnEnemyInMap("Ticker", x, Random.Range(0f, 10f), true);
      yield return new WaitForSeconds(0.2f);
    }
    StartCoroutine(basicSpawner());
    yield return new WaitForSeconds(10f);
    spawner.waveCleared();
  }

  void spawnBurst() {
    float x = spawner.ranXPos();
    for (int i = 0; i < 30; i++) {
      string name = "Kilo" + type[Random.Range(0, 3)];
      spawner.spawnEnemyInMap(name, x, Random.Range(0f, 10f), false);
    }
  }
  IEnumerator wave2() {
    spawnBurst();
    yield return new WaitForSeconds(20f);
    spawnBurst();
    yield return new WaitForSeconds(15f);
    spawnBurst();
    yield return new WaitForSeconds(15f);
    spawner.waveCleared();
  }

  IEnumerator wave3() {
    yield return new WaitForSeconds(10f);
    spawnBurst();
    yield return new WaitForSeconds(15f);
    spawnBurst();
    yield return new WaitForSeconds(15f);
    float x = spawner.ranXPos();
    for (int i = 0; i < 30; i++) {
      spawner.spawnEnemyInMap("Ticker", x, Random.Range(0f, 10f), true);
      yield return new WaitForSeconds(0.1f);
    }
    lastWaveDone = true;
    spawner.LastWaveEnemiesCleared();
  }
}
