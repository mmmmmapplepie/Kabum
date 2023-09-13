using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W2L20 : MonoBehaviour, IGetLevelDataInterface {
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
    spawner.spawnEnemyInMap("MesoCore", 0f, 10f, true, LevelSpawner.addToList.Specific, true);
    yield return new WaitForSeconds(20f);
    spawner.waveCleared();
  }
  IEnumerator wave2() {
    spawner.spawnEnemy("Armory", 0f, 10f);
    yield return new WaitForSeconds(3f);
    spawner.spawnEnemy("Armory", 5f, 10f);
    yield return new WaitForSeconds(3f);
    spawner.spawnEnemy("Armory", -5f, 10f);
    yield return new WaitForSeconds(10f);
    spawner.waveCleared();
  }


  string[] types = new string[6] { "Nano", "Meso", "Micro", "Mega", "Giga", "Ultimate" };
  IEnumerator ArmoredTypes(string name, float interval) {
    while (spawner.setEnemies.Count > 0) {
      spawner.spawnEnemy(name, spawner.ranXPos(), 10f);
      yield return new WaitForSeconds(interval + Random.Range(-1f, 1f));
    }
  }
  IEnumerator wave3() {
    StartCoroutine(ArmoredTypes("NanoArmored", 1.5f));
    StartCoroutine(ArmoredTypes("MicroArmored", 2f));
    StartCoroutine(ArmoredTypes("KiloArmored", 3f));
    StartCoroutine(ArmoredTypes("MegaArmored", 8f));
    StartCoroutine(ArmoredTypes("GigaArmored", 15f));
    StartCoroutine(ArmoredTypes("UltimateArmored", 20f));
    while (spawner.setEnemies.Count > 0) {
      yield return new WaitForSeconds(20f);
      spawner.spawnEnemy("Armory", 0f, 10f);
    }
    spawner.LastWaveEnemiesCleared();
  }
}
