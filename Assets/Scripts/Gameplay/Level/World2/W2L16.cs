using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W2L16 : MonoBehaviour, IGetLevelDataInterface {
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
  bool end = false;
  string[] type = new string[3] { "Armored", "Basic", "Shield" };
  IEnumerator spawnRoutine(string name, float interval) {
    while (!end) {
      spawner.spawnEnemy(name, spawner.ranXPos(), 10f);
      yield return new WaitForSeconds(interval);
    }
  }
  IEnumerator basicSpawnRoutine(string name, float interval) {
    while (!end) {
      string enename = name + type[Random.Range(0, 3)];
      spawner.spawnEnemy(enename, spawner.ranXPos(), 10f);
      yield return new WaitForSeconds(interval);
    }
  }
  IEnumerator wave1() {
    StartCoroutine(basicSpawnRoutine("Nano", 1f));
    yield return new WaitForSeconds(5f);
    StartCoroutine(basicSpawnRoutine("Micro", 1.5f));
    yield return new WaitForSeconds(5f);
    StartCoroutine(basicSpawnRoutine("Kilo", 2f));
    yield return new WaitForSeconds(10f);
    StartCoroutine(basicSpawnRoutine("Mega", 10f));
    yield return new WaitForSeconds(10f);
    StartCoroutine(basicSpawnRoutine("Giga", 20f));
    yield return new WaitForSeconds(10f);
    StartCoroutine(basicSpawnRoutine("Ultimate", 30f));
    yield return new WaitForSeconds(10f);
    StartCoroutine(spawnRoutine("Vessel", 15f));
    yield return new WaitForSeconds(10f);
    StartCoroutine(spawnRoutine("MesoVessel", 15f));
    yield return new WaitForSeconds(60f);
    end = true;
    spawner.LastWaveEnemiesCleared();
  }
}
