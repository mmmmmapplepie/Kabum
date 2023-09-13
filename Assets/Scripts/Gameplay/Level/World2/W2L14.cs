using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W2L14 : MonoBehaviour, IGetLevelDataInterface {
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

  IEnumerator wave1() {
    spawner.spawnEnemyInMap("Jammer", 0f, 10f, true);
    yield return new WaitForSeconds(10f);
    spawner.spawnEnemyInMap("Jammer", 5f, 10f, true);
    spawner.spawnEnemyInMap("Jammer", -5f, 10f, true);
    yield return new WaitForSeconds(5f);
    spawner.waveCleared();
  }

  string[] type = new string[3] { "Armored", "Basic", "Shield" };
  IEnumerator wave2() {
    for (int i = 0; i < 5; i++) {
      for (int j = 0; j < 20; j++) {
        string eneName = "Kilo" + type[Random.Range(0, 3)];
        spawner.spawnEnemy(eneName, spawner.ranXPos(), 10f);
      }
      yield return new WaitForSeconds(7f);
    }
    for (int i = 0; i < 2; i++) {
      float x = spawner.ranXPos();
      for (int j = 0; j < 30; j++) {
        string eneName = "Kilo" + type[Random.Range(0, 3)];
        spawner.spawnEnemy(eneName, x, 10f);
      }
      yield return new WaitForSeconds(7f);
    }
    spawner.waveCleared();
  }

  IEnumerator wave3() {
    for (int i = 120; i > 0; i--) {
      spawner.spawnEnemy("Enigma", spawner.ranXPos(), 10f);
      yield return new WaitForSeconds((float)i / 120f);
    }
    spawner.LastWaveEnemiesCleared();
  }
}
