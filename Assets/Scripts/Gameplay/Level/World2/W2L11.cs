using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W2L11 : MonoBehaviour, IGetLevelDataInterface {
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
    for (int i = 0; i < 10; i++) {
      if (i % 2 == 0) {
        spawner.spawnEnemy("MesoZipper", spawner.ranXPos(), 10f);
        yield return new WaitForSeconds(2f);
      } else {
        spawner.spawnEnemy("Zipper", spawner.ranXPos(), 10f);
        yield return new WaitForSeconds(1f);
      }
    }
    spawner.AllTriggerEnemiesCleared();
  }

  string[] type = new string[3] { "Armored", "Basic", "Shield" };
  string[] prefix = new string[2] { "Ultimate", "Giga" };
  IEnumerator wave2() {
    float xpos = spawner.ranXPos();
    for (int i = 0; i < 2; i++) {
      string ranName = prefix[Random.Range(0, 2)] + type[Random.Range(0, 3)];
      spawner.spawnEnemy(ranName, xpos, 10f);
    }
    yield return new WaitForSeconds(10f);
    for (int i = 0; i < 5; i++) {
      string ranName = prefix[Random.Range(0, 2)] + type[Random.Range(0, 3)];
      spawner.spawnEnemy(ranName, xpos, 10f);
    }
    spawner.LastWaveEnemiesCleared();
  }
}
