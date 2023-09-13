using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W2L22 : MonoBehaviour, IGetLevelDataInterface {
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
    spawner.spawnEnemy("GigaBasic", 0f, 10f);
    yield return null;
    spawner.AllTriggerEnemiesCleared();
  }
  IEnumerator wave2() {
    for (int i = 0; i < 6; i++) {
      spawner.spawnEnemy("KiloArmored", 5f, 10f);
      spawner.spawnEnemy("KiloBasic", 0f, 10f);
      spawner.spawnEnemy("KiloShield", -5f, 10f);
    }
    yield return new WaitForSeconds(10f);
    for (int i = 0; i < 4; i++) {
      spawner.spawnEnemy("MegaArmored", 5f, 10f);
      spawner.spawnEnemy("MegaBasic", 0f, 10f);
      spawner.spawnEnemy("MegaShield", -5f, 10f);
    }
    yield return new WaitForSeconds(10f);
    for (int i = 0; i < 2; i++) {
      spawner.spawnEnemy("GigaArmored", 5f, 10f);
      spawner.spawnEnemy("GigaBasic", 0f, 10f);
      spawner.spawnEnemy("GigaShield", -5f, 10f);
    }
    spawner.AllTriggerEnemiesCleared();
  }
  IEnumerator wave3() {
    spawner.spawnEnemyInMap("Disruptor", 1f, 10f, true);
    spawner.spawnEnemyInMap("Disruptor", -1f, 10f, true);
    for (int i = 0; i < 6; i++) {
      spawner.spawnEnemy("KiloArmored", 5f, 10f);
      spawner.spawnEnemy("KiloBasic", 0f, 10f);
      spawner.spawnEnemy("KiloShield", -5f, 10f);
    }
    yield return new WaitForSeconds(10f);
    for (int i = 0; i < 4; i++) {
      spawner.spawnEnemy("MegaArmored", 5f, 10f);
      spawner.spawnEnemy("MegaBasic", 0f, 10f);
      spawner.spawnEnemy("MegaShield", -5f, 10f);
    }
    yield return new WaitForSeconds(10f);
    for (int i = 0; i < 2; i++) {
      spawner.spawnEnemy("GigaArmored", 5f, 10f);
      spawner.spawnEnemy("GigaBasic", 0f, 10f);
      spawner.spawnEnemy("GigaShield", -5f, 10f);
    }
    spawner.LastWaveEnemiesCleared();
  }
}
