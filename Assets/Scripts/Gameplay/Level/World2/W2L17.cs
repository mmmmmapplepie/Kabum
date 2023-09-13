using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W2L17 : MonoBehaviour, IGetLevelDataInterface {
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

  string[] prefix = new string[3] { "", "Macro", "Meso" };
  IEnumerator wave1() {
    for (int i = 0; i < 20; i++) {
      spawner.spawnEnemy("Zipper", spawner.ranXPos(), 10f);
    }
    yield return new WaitForSeconds(5f);
    for (int i = 0; i < 30; i++) {
      spawner.spawnEnemy("Zipper", spawner.ranXPos(), 10f);
    }
    yield return new WaitForSeconds(7f);
    for (int i = 0; i < 20; i++) {
      spawner.spawnEnemy("MesoZipper", spawner.ranXPos(), 10f);
    }
    yield return new WaitForSeconds(5f);
    for (int i = 0; i < 20; i++) {
      spawner.spawnEnemy("MacroZipper", spawner.ranXPos(), 10f);
    }
    yield return new WaitForSeconds(5f);
    for (int i = 0; i < 20; i++) {
      spawner.spawnEnemy("MacroZipper", spawner.ranXPos(), 10f);
    }
    spawner.AllTriggerEnemiesCleared();
  }

  bool done = false;
  IEnumerator ranSpawn() {
    while (!done) {
      spawner.spawnEnemy(prefix[Random.Range(0, 3)] + "Zipper", spawner.ranXPos(), 10f);
      yield return new WaitForSeconds(1f);
    }
  }
  void spawninMap(string name, float y) {
    spawner.spawnEnemyInMap(name, -5f, y, false);
    spawner.spawnEnemyInMap(name, -3f, y, false);
    spawner.spawnEnemyInMap(name, -1f, y, false);
    spawner.spawnEnemyInMap(name, 1f, y, false);
    spawner.spawnEnemyInMap(name, 3f, y, false);
    spawner.spawnEnemyInMap(name, 5f, y, false);
  }
  IEnumerator wave2() {
    StartCoroutine(ranSpawn());
    spawninMap("Reflector", 9f);
    yield return new WaitForSeconds(3f);
    spawninMap("MesoReflector", 9f);
    yield return new WaitForSeconds(3f);
    spawninMap("MacroReflector", 9f);
    yield return new WaitForSeconds(20f);
    done = true;
    spawner.LastWaveEnemiesCleared();
  }
}
