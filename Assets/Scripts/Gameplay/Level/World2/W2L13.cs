using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W2L13 : MonoBehaviour, IGetLevelDataInterface {
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
    spawner.spawnEnemy("MesoReflector", spawner.ranXPos(), 10f);
    yield return new WaitForSeconds(5f);
    spawner.spawnEnemy("MesoReflector", spawner.ranXPos(), 10f);
    yield return new WaitForSeconds(5f);
    spawner.waveCleared();
  }

  IEnumerator wave2() {
    spawner.spawnEnemyInMap("MesoReflector", spawner.ranXPos(), 6f, false);
    spawner.spawnEnemyInMap("MesoReflector", spawner.ranXPos(), 6f, false);
    for (int i = 0; i < 20; i++) {
      spawner.spawnEnemyInMap("MesoEnigma", spawner.ranXPos(), Random.Range(0f, 10f), false);
      yield return new WaitForSeconds(0.5f);
    }
    spawner.LastWaveEnemiesCleared();
  }
}
