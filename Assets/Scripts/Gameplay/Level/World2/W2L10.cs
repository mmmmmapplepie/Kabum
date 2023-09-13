using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W2L10 : MonoBehaviour, IGetLevelDataInterface {
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
    spawner.spawnEnemyInMap("UltimateBasic", spawner.ranXPos(), 9f, true);
    spawner.spawnEnemyInMap("UltimateBasic", spawner.ranXPos(), 9f, true);
    spawner.spawnEnemyInMap("UltimateBasic", spawner.ranXPos(), 9f, true);
    spawner.spawnEnemyInMap("UltimateBasic", spawner.ranXPos(), 9f, true);
    spawner.spawnEnemyInMap("UltimateBasic", spawner.ranXPos(), 9f, true);
    yield return new WaitForSeconds(10f);
    spawner.spawnEnemyInMap("Havoc", spawner.ranXPos(), 9f, true);
    spawner.AllTriggerEnemiesCleared();
  }

  IEnumerator wave2() {
    spawner.spawnEnemyInMap("Armory", spawner.ranXPos(), 7f, true);
    yield return new WaitForSeconds(5f);
    for (int i = 0; i < 5; i++) {
      spawner.spawnEnemyInMap("UltimateArmored", spawner.ranXPos(), 9f, true);
      spawner.spawnEnemyInMap("UltimateShield", spawner.ranXPos(), 9f, true);
    }
    yield return new WaitForSeconds(15f);
    spawner.spawnEnemyInMap("Booster", spawner.ranXPos(), 9f, true);
    spawner.AllTriggerEnemiesCleared();
  }

  string[] megasList = new string[3] { "MegaBasic", "MegaArmored", "MegaShield" };

  IEnumerator megas() {
    bool startTimePassed = false;
    float startTime = Time.time;
    while (spawner.SpecificWaveTriggerEnemies.Count > 0 || !startTimePassed) {
      if (Time.time - startTime > 20f) startTimePassed = true;
      spawner.spawnEnemy(megasList[Random.Range(0, 3)], spawner.ranXPos(), 10f);
      yield return new WaitForSeconds(5f);
    }
  }
  IEnumerator wave3() {
    StartCoroutine(megas());
    yield return new WaitForSeconds(15f);
    spawner.spawnEnemyInMap("Protector", spawner.ranXPos(), 9f, true, LevelSpawner.addToList.Specific);
    spawner.spawnEnemyInMap("Maintainer", spawner.ranXPos(), 9f, true, LevelSpawner.addToList.Specific);
    spawner.LastWaveEnemiesCleared();
  }
}
