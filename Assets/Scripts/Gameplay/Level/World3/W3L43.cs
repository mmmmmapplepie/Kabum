using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W3L43 : MonoBehaviour, IGetLevelDataInterface {
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

  string[] type = new string[4] { "Teleporter", "Enigma", "Shifter", "Vessel" };
  string[] btype = new string[3] { "Protector", "Maintainer", "Armory" };
  bool done = false;
  IEnumerator tick() {
    while (!done || spawner.setEnemies.Count > 0) {
      spawner.spawnEnemy("Hyper" + type[Random.Range(0, 4)], spawner.ranXPos(), 10f);
      yield return new WaitForSeconds(Random.Range(0f, 3f));
    }
  }
  IEnumerator buff() {
    while (!done || spawner.setEnemies.Count > 0) {
      spawner.spawnEnemy("Hyper" + btype[Random.Range(0, 3)], spawner.ranXPos(), 10f);
      yield return new WaitForSeconds(Random.Range(0f, 8f));
    }
  }
  IEnumerator wave1() {
    int i = 0;
    while (i < 10) {
      i++;
      spawner.spawnEnemyInMap("HyperArmory", spawner.ranXPos(), 8f, true);
    }
    yield return new WaitForSeconds(5f);
    StartCoroutine(tick());
    StartCoroutine(buff());
    yield return new WaitForSeconds(10f);
    spawner.waveCleared();
  }

  IEnumerator wave2() {
    StartCoroutine(tick());
    yield return new WaitForSeconds(20f);
    spawner.spawnEnemyInMap("Colossus", 5f, 10f, true, LevelSpawner.addToList.Specific, true);
    spawner.spawnEnemyInMap("Colossus", -5f, 10f, true, LevelSpawner.addToList.Specific, true);
    spawner.spawnEnemyInMap("MesoCore", 5f, 10f, true, LevelSpawner.addToList.Specific, true);
    spawner.spawnEnemyInMap("MesoCore", -5f, 10f, true, LevelSpawner.addToList.Specific, true);
    yield return new WaitForSeconds(30f);
    done = true;
    spawner.LastWaveEnemiesCleared();


  }





}
