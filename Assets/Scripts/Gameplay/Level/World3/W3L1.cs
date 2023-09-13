using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W3L1 : MonoBehaviour, IGetLevelDataInterface {
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
  string[] rank = new string[6] { "Nano", "Micro", "Kilo", "Mega", "Giga", "Ultimate" };

  void burstSpawn(int num, float y = 10f) {
    for (int i = 0; i < num; i++) {
      string currrank = rank[Random.Range(0, 6)];
      bool size = (currrank == "Ultimate" || currrank == "Giga") ? true : false;
      spawner.spawnEnemyInMap(currrank + type[Random.Range(0, 3)], spawner.ranXPos(), y, size);
    }
  }
  IEnumerator wave1() {
    for (int i = 0; i < 5; i++) {
      burstSpawn(5, 5f);
      yield return new WaitForSeconds(0.5f);
      burstSpawn(6, 6f);
      yield return new WaitForSeconds(0.5f);
      burstSpawn(7, 7f);
      yield return new WaitForSeconds(0.5f);
      burstSpawn(8, 8f);
      yield return new WaitForSeconds(0.5f);
      burstSpawn(9, 9f);
      yield return new WaitForSeconds(0.5f);
      burstSpawn(10, 10f);
      yield return new WaitForSeconds(5f);
    }
    spawner.AllTriggerEnemiesCleared();
  }

  IEnumerator wave2() {
    for (int i = 0; i < 8; i++) {
      burstSpawn(6, 0f);
      yield return new WaitForSeconds(0.5f);
      burstSpawn(7, 2f);
      yield return new WaitForSeconds(0.5f);
      burstSpawn(8, 4f);
      yield return new WaitForSeconds(0.5f);
      burstSpawn(9, 6f);
      yield return new WaitForSeconds(0.5f);
      burstSpawn(10, 7f);
      yield return new WaitForSeconds(0.5f);
      burstSpawn(11, 10f);
      yield return new WaitForSeconds(5f);
    }
    spawner.LastWaveEnemiesCleared();
  }
}
