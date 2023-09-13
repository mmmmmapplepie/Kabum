using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W2L29 : MonoBehaviour, IGetLevelDataInterface {
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
    spawner.spawnEnemyInMap("Havoc", spawner.ranXPos(), 10f, true, LevelSpawner.addToList.Specific, true);
    spawner.spawnEnemyInMap("Havoc", spawner.ranXPos(), 10f, true, LevelSpawner.addToList.Specific, true);
    spawner.spawnEnemyInMap("Havoc", spawner.ranXPos(), 10f, true, LevelSpawner.addToList.Specific, true);
    spawner.spawnEnemyInMap("Havoc", spawner.ranXPos(), 10f, true, LevelSpawner.addToList.Specific, true);
    spawner.spawnEnemyInMap("Havoc", spawner.ranXPos(), 10f, true, LevelSpawner.addToList.Specific, true);
    spawner.spawnEnemyInMap("Havoc", spawner.ranXPos(), 10f, true, LevelSpawner.addToList.Specific, true);
    spawner.spawnEnemyInMap("Havoc", spawner.ranXPos(), 10f, true, LevelSpawner.addToList.Specific, true);
    spawner.spawnEnemyInMap("Havoc", spawner.ranXPos(), 10f, true, LevelSpawner.addToList.Specific, true);
    spawner.spawnEnemyInMap("Havoc", spawner.ranXPos(), 10f, true, LevelSpawner.addToList.Specific, true);
    spawner.spawnEnemyInMap("Havoc", spawner.ranXPos(), 10f, true, LevelSpawner.addToList.Specific, true);
    yield return new WaitForSeconds(4f);
    spawner.waveCleared();
  }
  string[] pre = new string[3] { "", "Meso", "Macro" };
  string[] type = new string[2] { "Zipper", "Shifter" };
  IEnumerator wave2() {
    while (spawner.setEnemies.Count > 0) {
      spawner.spawnEnemy(pre[Random.Range(0, 3)] + type[Random.Range(0, 2)], spawner.ranXPos(), 10f);
      yield return new WaitForSeconds(Random.Range(0f, 1f));
    }
    spawner.LastWaveEnemiesCleared();
  }

}
