using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W2L24 : MonoBehaviour, IGetLevelDataInterface {
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
    for (int i = 0; i < 50; i++) {
      spawner.spawnEnemy(level.Enemies[Random.Range(0, level.Enemies.Count)].enemyPrefab.name, spawner.ranXPos(), 10f);
      yield return new WaitForSeconds(2f);
    }
    spawner.LastWaveEnemiesCleared();
  }
}
