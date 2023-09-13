using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W2L9 : MonoBehaviour, IGetLevelDataInterface {
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

  string[] smallShield = new string[3] { "NanoShield", "MicroShield", "KiloShield" };
  string[] bigShield = new string[3] { "MegaShield", "GigaShield", "UltimateShield" };
  IEnumerator wave1() {
    int waves = 4;
    for (int i = 0; i < waves; i++) {
      for (int ene = 0; ene < 5; ene++) {
        spawner.spawnEnemy(smallShield[Random.Range(0, 3)], 0f, 10f);
      }
      yield return new WaitForSeconds(5f);
      for (int ene = 0; ene < 10; ene++) {
        spawner.spawnEnemy(smallShield[Random.Range(0, 3)], 0f, 10f);
      }
      yield return new WaitForSeconds(5f);
      for (int ene = 0; ene < 20; ene++) {
        spawner.spawnEnemy(smallShield[Random.Range(0, 3)], 0f, 10f);
      }
      yield return new WaitForSeconds(5f);
      for (int ene = 0; ene < 5; ene++) {
        spawner.spawnEnemy(bigShield[Random.Range(0, 3)], 0f, 10f);
      }
    }
    yield return new WaitForSeconds(15f);
    spawner.waveCleared();
  }

  IEnumerator wave2() {
    yield return new WaitForSeconds(10f);
    for (int ene = 0; ene < 10; ene++) {
      spawner.spawnEnemy(bigShield[Random.Range(0, 3)], spawner.ranXPos(), 10f);
    }
    yield return new WaitForSeconds(20f);
    for (int ene = 0; ene < 15; ene++) {
      spawner.spawnEnemy(bigShield[Random.Range(0, 3)], -spawner.ranXPos(), 10f);
    }
    spawner.LastWaveEnemiesCleared();
  }
}
