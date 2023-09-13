using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W2L21 : MonoBehaviour, IGetLevelDataInterface {
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
    spawner.spawnEnemy("Ticker", 0f, 0f);
    yield return null;
    spawner.AllTriggerEnemiesCleared();
  }

  void bundle(int num, string name) {
    float x = spawner.ranXPos();
    for (int i = 0; i < num; i++) {
      spawner.spawnEnemy(name, x, 10f);
    }
  }
  IEnumerator wave2() {
    bundle(10, "NanoBasic");
    yield return new WaitForSeconds(1f);
    bundle(12, "NanoArmored");
    yield return new WaitForSeconds(1f);
    bundle(14, "NanoShield");
    yield return new WaitForSeconds(1f);
    bundle(10, "MicroBasic");
    yield return new WaitForSeconds(1f);
    bundle(12, "MicroArmored");
    yield return new WaitForSeconds(1f);
    bundle(14, "MicroShield");
    spawner.AllTriggerEnemiesCleared();
  }

  IEnumerator wave3() {
    spawner.spawnEnemy("MesoTicker", spawner.ranXPos(), 10f);
    bundle(20, "NanoShield");
    yield return new WaitForSeconds(4f);
    bundle(20, "MicroArmored");
    yield return new WaitForSeconds(10f);
    spawner.spawnEnemy("MacroTicker", spawner.ranXPos(), 10f);
    bundle(20, "MicroShield");
    yield return new WaitForSeconds(4f);
    bundle(20, "NanoArmored");
    spawner.LastWaveEnemiesCleared();
  }
}
