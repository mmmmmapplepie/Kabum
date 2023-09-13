using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W3L14 : MonoBehaviour, IGetLevelDataInterface {
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
  string[] type = new string[3] { "Basic", "Armored", "Shield" };
  IEnumerator wave1() {
    int i = 0;
    while (i < 15) {
      string name;
      float time;
      if (i % 3 == 0) {
        name = "Ultimate" + type[Random.Range(0, 3)];
        time = 8f;
      } else if (i % 3 == 1) {
        name = "HyperTicker";
        time = 6f;
      } else {
        name = "HyperTeleporter";
        time = 4f;
      }
      spawner.spawnEnemy(name, spawner.ranXPos(), 10f);
      yield return new WaitForSeconds(time);
      i++;
    }
    spawner.LastWaveEnemiesCleared();
  }
}
