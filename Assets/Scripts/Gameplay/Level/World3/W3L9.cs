using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W3L9 : MonoBehaviour, IGetLevelDataInterface {
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

  IEnumerator wave1() {
    float startT = Time.time;
    while (Time.time < startT + 30f) {
      string ene = level.Enemies[Random.Range(0, level.Enemies.Count)].enemyPrefab.name;
      while (ene == "Core" || ene == "Armory") {
        ene = level.Enemies[Random.Range(0, level.Enemies.Count)].enemyPrefab.name;
      }
      spawner.spawnEnemy(ene, spawner.ranXPos(), 10f);
      yield return new WaitForSeconds(0.5f);
    }
    spawner.AllTriggerEnemiesCleared();
  }

  IEnumerator wave2() {
    for (int i = -5; i <= 5; i += 2) {
      spawner.spawnEnemyInMap("Core", (float)i, Random.Range(7f, 10f), true, LevelSpawner.addToList.Specific, true);
    }
    yield return new WaitForSeconds(5f);
    spawner.waveCleared();
  }

  bool stopsummon = false;
  IEnumerator constantBasic() {
    while (!stopsummon || spawner.setEnemies.Count > 0) {
      string ene = level.Enemies[Random.Range(0, level.Enemies.Count)].enemyPrefab.name;
      while (ene == "Core" || ene == "Armory") {
        ene = level.Enemies[Random.Range(0, level.Enemies.Count)].enemyPrefab.name;
      }
      spawner.spawnEnemy(ene, spawner.ranXPos(), 10f);
      yield return new WaitForSeconds(0.5f);
    }
  }
  IEnumerator wave3() {
    StartCoroutine(constantBasic());
    yield return new WaitForSeconds(10f);
    for (int i = -5; i <= 5; i += 2) {
      spawner.spawnEnemyInMap("Armory", (float)i, 10f, true, LevelSpawner.addToList.Specific, true);
    }
    yield return new WaitForSeconds(5f);
    stopsummon = true;
    spawner.LastWaveEnemiesCleared();
  }
}
