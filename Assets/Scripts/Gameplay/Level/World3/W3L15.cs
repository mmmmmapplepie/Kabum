using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W3L15 : MonoBehaviour, IGetLevelDataInterface {
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
  string[] type = new string[3] { "HyperZipper", "HyperShifter", "HyperTeleporter" };
  IEnumerator wave1() {
    int i = 0;
    while (i < 40) {
      i++;
      spawner.spawnEnemy(type[Random.Range(0, 2)], spawner.ranXPos(), 10f);
      yield return new WaitForSeconds(1f);
    }
    spawner.waveCleared();
  }

  bool done = false;
  IEnumerator spawn() {
    while (spawner.setEnemies.Count > 0 || !done) {
      spawner.spawnEnemy(type[Random.Range(0, 3)], spawner.ranXPos(), 10f);
      yield return new WaitForSeconds(Random.Range(0f, 2f));
    }
  }
  IEnumerator wave2() {
    spawner.spawnEnemyInMap("Behemoth", 0f, 10f, true, LevelSpawner.addToList.Specific, true);
    StartCoroutine(spawn());
    yield return new WaitForSeconds(10f);
    spawner.waveCleared();
  }
  IEnumerator wave3() {
    spawner.spawnEnemyInMap("Booster", -5f, 8f, true);
    spawner.spawnEnemyInMap("Booster", -1f, 8f, true);
    spawner.spawnEnemyInMap("Booster", 1f, 8f, true);
    spawner.spawnEnemyInMap("Booster", 5f, 8f, true);
    yield return new WaitForSeconds(20f);
    done = true;
    yield return StartCoroutine(spawn());
    spawner.LastWaveEnemiesCleared();
  }
}
