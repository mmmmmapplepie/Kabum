using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W3L7 : MonoBehaviour, IGetLevelDataInterface {
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

  string[] rank = new string[3] { "", "Meso", "Macro" };
  IEnumerator wave1() {
    int summon = 0;
    while (summon < 50) {
      summon++;
      spawner.spawnEnemyInMap("Disruptor", spawner.ranXPos(), Random.Range(0f, 10f), true);
      yield return new WaitForSeconds(0.1f);
    }
    yield return new WaitForSeconds(10f);
    spawner.waveCleared();
  }

  IEnumerator wave2() {
    int summon = 0;
    while (summon < 120) {
      spawner.spawnEnemy(rank[Random.Range(0, 3)] + "Zipper", 4f, 10f);
      summon++;
      yield return new WaitForSeconds(0.2f);
    }
    spawner.AllTriggerEnemiesCleared();
  }
  IEnumerator wave3() {
    spawner.spawnEnemyInMap("Disruptor", spawner.ranXPos(), 8f, true);
    spawner.spawnEnemyInMap("Disruptor", spawner.ranXPos(), 8f, true);
    spawner.spawnEnemyInMap("Disruptor", spawner.ranXPos(), 8f, true);
    int summon = 0;
    while (summon < 100) {
      spawner.spawnEnemy(rank[Random.Range(0, 3)] + "Zipper", 4f, 10f);
      spawner.spawnEnemy(rank[Random.Range(0, 3)] + "Shifter", 4f, 10f);
      summon++;
      yield return new WaitForSeconds(0.4f);
    }
    spawner.LastWaveEnemiesCleared();
  }


}
