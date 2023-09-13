using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W3L24 : MonoBehaviour, IGetLevelDataInterface {
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
    int i = 0;
    spawner.spawnEnemyInMap("Core", 1f, 8f, true);
    spawner.spawnEnemyInMap("Core", -1f, 8f, true);
    while (i < 10) {
      i++;
      int r = Random.Range(0, 2);
      if (r == 0) {
        yield return new WaitForSeconds(10f);
        spawner.spawnEnemyInMap("MesoCore", spawner.ranXPos(), 5f, true);
      } else {
        yield return new WaitForSeconds(5f);
        spawner.spawnEnemyInMap("Core", spawner.ranXPos(), 5f, true);
      }
    }
    spawner.LastWaveEnemiesCleared();
  }
}
