using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W3L2 : MonoBehaviour, IGetLevelDataInterface {
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
    float xpos = spawner.ranXPos();
    for (int i = 0; i < 15; i++) {
      spawner.spawnEnemyInMap("Jammer", xpos, i / 15f, true);
      yield return new WaitForSeconds(0.2f);
    }

    yield return new WaitForSeconds(10f);
    int summon = 0;
    while (summon < 10) {
      spawner.spawnEnemyInMap("MacroVessel", spawner.ranXPos(), Random.Range(5f, 10f), true);
      yield return new WaitForSeconds(0.5f);
      summon++;
    }
    spawner.AllTriggerEnemiesCleared();
  }

  IEnumerator wave2() {
    int summon = 0;
    while (summon < 50) {
      string ene = level.Enemies[Random.Range(0, level.Enemies.Count)].enemyPrefab.name;
      while (ene == "Jammer") {
        ene = level.Enemies[Random.Range(0, level.Enemies.Count)].enemyPrefab.name;
      }
      spawner.spawnEnemy(ene, spawner.ranXPos(), 10f);
      yield return new WaitForSeconds(0.5f);
      summon++;
    }
    spawner.LastWaveEnemiesCleared();
  }
}
