using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W2L28 : MonoBehaviour, IGetLevelDataInterface {
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

  string[] pre = new string[3] { "Nano", "Micro", "Kilo" };
  string[] type = new string[3] { "Basic", "Armored", "Shield" };
  IEnumerator wave1() {
    for (int i = 0; i < 10; i++) {
      string n = pre[Random.Range(0, 3)] + type[Random.Range(0, 3)];
      spawner.spawnEnemy(n, spawner.ranXPos(), 10f);
      yield return new WaitForSeconds(Random.Range(0f, 5f));
    }
    spawner.AllTriggerEnemiesCleared();
  }
  IEnumerator wave2() {
    spawner.spawnEnemy("Disruptor", spawner.ranXPos(), 10f);
    spawner.spawnEnemy("Jammer", spawner.ranXPos(), 10f);
    yield return new WaitForSeconds(10f);
    spawner.waveCleared();
  }
  IEnumerator wave3() {
    for (int i = 0; i < 20; i++) {
      string n = pre[Random.Range(0, 3)] + type[Random.Range(0, 3)];
      spawner.spawnEnemy(n, spawner.ranXPos(), 10f);
      yield return new WaitForSeconds(Random.Range(0f, 2f));
    }
    spawner.AllTriggerEnemiesCleared();
  }
  IEnumerator wave4() {
    spawner.spawnEnemy("Disruptor", spawner.ranXPos(), 10f);
    spawner.spawnEnemy("Jammer", spawner.ranXPos(), 10f);
    spawner.spawnEnemy("Disruptor", spawner.ranXPos(), 10f);
    spawner.spawnEnemy("Jammer", spawner.ranXPos(), 10f);
    for (int i = 0; i < 100; i++) {
      string n = pre[Random.Range(0, 3)] + type[Random.Range(0, 3)];
      spawner.spawnEnemy(n, spawner.ranXPos(), 10f);
      yield return new WaitForSeconds(Random.Range(0f, 1f));
    }
    spawner.LastWaveEnemiesCleared();
  }



}
