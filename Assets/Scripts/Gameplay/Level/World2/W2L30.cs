using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W2L30 : MonoBehaviour, IGetLevelDataInterface {
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
    spawner.spawnEnemyInMap("Minima", 0f, 0f, true, LevelSpawner.addToList.Specific, true);
    yield return new WaitForSeconds(10f);
    spawner.waveCleared();
  }

  string[] baseTypes = new string[3] { "Basic", "Armored", "Shield" };
  IEnumerator wave2() {
    for (int i = 0; i < 10; i++) {
      string n = "Giga" + baseTypes[Random.Range(0, 3)];
      spawner.spawnEnemyInMap(n, spawner.ranXPos(), Random.Range(0f, 10f), true);
      yield return new WaitForSeconds(1f);
    }
    yield return new WaitForSeconds(10f);
    spawner.waveCleared();
  }
  IEnumerator wave3() {
    spawner.spawnEnemyInMap("Protector", 4f, 8f, true);
    spawner.spawnEnemyInMap("Protector", -4f, 8f, true);
    yield return new WaitForSeconds(20f);
    spawner.waveCleared();
  }
  IEnumerator wave4() {
    for (int i = 0; i < 10; i++) {
      string n = "Ultimate" + baseTypes[Random.Range(0, 3)];
      spawner.spawnEnemyInMap(n, spawner.ranXPos(), Random.Range(0f, 10f), true);
      yield return new WaitForSeconds(0.5f);
    }
    yield return new WaitForSeconds(30f);
    spawner.waveCleared();
  }

  string[] rank = new string[3] { "", "Meso", "Macro" };
  IEnumerator wave5() {
    spawner.spawnEnemyInMap("Maintainer", 4f, 7f, true);
    spawner.spawnEnemyInMap("Maintainer", -4f, 7f, true);
    while (spawner.setEnemies.Count > 0) {
      string n = rank[Random.Range(0, 3)] + "Ticker";
      spawner.spawnEnemyInMap(n, spawner.ranXPos(), Random.Range(2f, 10f), true);
      yield return new WaitForSeconds(Random.Range(3f, 5f));
    }
    spawner.LastWaveEnemiesCleared();
  }












}
