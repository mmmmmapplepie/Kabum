using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W3L4 : MonoBehaviour, IGetLevelDataInterface {
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
    spawner.spawnEnemy("MacroOutlier", 0f, 10f);
    yield return new WaitForSeconds(5f);
    int summon = 0;
    while (summon < 4) {
      spawner.spawnEnemy("MacroOutlier", spawner.ranXPos(), 10f);
      yield return new WaitForSeconds(4f);
      summon++;
    }
    spawner.AllTriggerEnemiesCleared();
  }

  IEnumerator wave2() {
    for (int i = -5; i <= 5; i++) {
      spawner.spawnEnemyInMap("Carrier", (float)i, 8f, true, LevelSpawner.addToList.Specific, true);
      yield return new WaitForSeconds(0.5f);
    }
    yield return new WaitForSeconds(5f);
    spawner.waveCleared();
  }

  IEnumerator wave3() {
    for (int i = -5; i <= 5; i++) {
      spawner.spawnEnemyInMap(rank[Random.Range(0, 3)] + "Outlier", (float)i, 10f, false, LevelSpawner.addToList.Specific, true);
      yield return new WaitForSeconds(1f);
    }
    spawner.waveCleared();
  }
  IEnumerator wave4() {
    while (spawner.setEnemies.Count > 0) {
      spawner.spawnEnemy(rank[Random.Range(0, 3)] + "Zipper", spawner.ranXPos(), 10f);
      yield return new WaitForSeconds(0.5f);
    }
    spawner.LastWaveEnemiesCleared();
  }
}
