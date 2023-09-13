using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W2L3 : MonoBehaviour, IGetLevelDataInterface {
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
    for (int i = 5; i > -5; i--) {
      spawner.spawnEnemy("MegaShield", (float)i, 10f);
    }
    yield return null;
    spawner.AllEnemiesCleared();
  }
  IEnumerator wave2() {
    for (int i = 5; i > -5; i--) {
      spawner.spawnEnemy("KiloBasic", (float)i, 10f);
      spawner.spawnEnemy("KiloBasic", (float)i, 10f);
      spawner.spawnEnemy("KiloBasic", (float)i, 10f);
      spawner.spawnEnemy("KiloBasic", (float)i, 10f);
      spawner.spawnEnemy("KiloBasic", (float)i, 10f);
      yield return new WaitForSeconds(1f);
    }
    yield return null;
    spawner.AllEnemiesCleared();
  }
  IEnumerator wave3() {
    for (int i = -5; i < 5; i++) {
      spawner.spawnEnemy("Enigma", (float)i, 10f);
      spawner.spawnEnemy("Enigma", (float)i, 10f);
      spawner.spawnEnemy("Enigma", (float)i, 10f);
      spawner.spawnEnemy("Enigma", (float)i, 10f);
      spawner.spawnEnemy("Enigma", (float)i, 10f);
      yield return new WaitForSeconds(2f);
    }
    yield return null;
    spawner.AllEnemiesCleared();
  }
  IEnumerator wave4() {
    for (int i = -5; i < 5; i++) {
      spawner.spawnEnemyInMap("MesoTeleporter", (float)i, 7f, false);
      spawner.spawnEnemyInMap("MesoTeleporter", (float)i, 7f, false);
      spawner.spawnEnemyInMap("MesoTeleporter", (float)i, 7f, false);
      spawner.spawnEnemyInMap("MesoTeleporter", (float)i, 7f, false);
      spawner.spawnEnemyInMap("MesoTeleporter", (float)i, 7f, false);
      yield return new WaitForSeconds(2f);
      spawner.spawnEnemy("Enigma", (float)i, 10f);
      spawner.spawnEnemy("Enigma", (float)i, 10f);
      spawner.spawnEnemy("Enigma", (float)i, 10f);
      spawner.spawnEnemy("Enigma", (float)i, 10f);
      spawner.spawnEnemy("Enigma", (float)i, 10f);
    }
    yield return null;
    spawner.LastWaveEnemiesCleared();
  }




}
