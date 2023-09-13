using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W2L15 : MonoBehaviour, IGetLevelDataInterface {
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
  void delayedSummon() {
    spawner.spawnEnemyInMap("Booster", 0f, 8f, true);
    spawner.spawnEnemyInMap("Booster", 0f, 8f, true);
  }
  IEnumerator wave1() {
    Invoke("delayedSummon", 10f);
    for (int i = 0; i < 30; i++) {
      spawner.spawnEnemy("MacroShifter", spawner.ranXPos(), 10f);
      yield return new WaitForSeconds(1f);
    }
    spawner.AllTriggerEnemiesCleared();
  }

  IEnumerator wave2() {
    spawner.spawnEnemyInMap("Armory", 0f, 8f, true);
    spawner.spawnEnemyInMap("Armory", 0f, 8f, true);
    for (int i = 0; i < 2; i++) {
      yield return new WaitForSeconds(8f);
      spawner.spawnEnemy("MesoVessel", spawner.ranXPos(), 10f);
    }
    yield return new WaitForSeconds(10f);
    spawner.waveCleared();
  }
  IEnumerator wave3() {
    spawner.spawnEnemyInMap("Colossus", 0f, 8f, true, LevelSpawner.addToList.Specific, true);
    yield return new WaitForSeconds(5f);
    spawner.spawnEnemyInMap("Havoc", spawner.ranXPos(), 10f, true);
    yield return new WaitForSeconds(5f);
    spawner.waveCleared();
  }

  IEnumerator wave4() {
    spawner.spawnEnemyInMap("Protector", 5f, 10f, true);
    spawner.spawnEnemyInMap("Protector", -5f, 10f, true);
    for (int i = 0; i < 2; i++) {
      yield return new WaitForSeconds(10f);
      spawner.spawnEnemy("Vessel", spawner.ranXPos(), 10f);
    }
    spawner.waveCleared();
  }

  IEnumerator wave5() {
    spawner.spawnEnemyInMap("Maintainer", 0f, 10f, true);
    while (spawner.setEnemies.Count > 0) {
      string name = Random.Range(0, 2) == 0 ? "Vessel" : "MesoVessel";
      spawner.spawnEnemy(name, spawner.ranXPos(), 10f);
      yield return new WaitForSeconds(3f);
    }
    spawner.LastWaveEnemiesCleared();
  }
}
