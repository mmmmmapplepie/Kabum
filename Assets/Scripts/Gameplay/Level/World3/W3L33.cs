using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W3L33 : MonoBehaviour, IGetLevelDataInterface {
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
  string[] rank = new string[6] { "Nano", "Micro", "Kilo", "Mega", "Giga", "Ultimate" };
  string[] basetype = new string[3] { "Basic", "Armored", "Shield" };
  IEnumerator wave1() {
    spawner.spawnEnemyInMap("UltimateBasic", 0f, 0f, true);
    yield return new WaitForSeconds(5f);
    spawner.spawnEnemyInMap("UltimateBasic", 4f, 0f, true);
    yield return new WaitForSeconds(5f);
    spawner.spawnEnemyInMap("UltimateBasic", -4f, 0f, true);
    yield return new WaitForSeconds(5f);
    spawner.spawnEnemyInMap("UltimateBasic", 4f, 0f, true);
    spawner.spawnEnemyInMap("UltimateBasic", -4f, 0f, true);
    yield return new WaitForSeconds(5f);
    spawner.AllTriggerEnemiesCleared();
  }

  IEnumerator wave2() {
    int i = 0;
    while (i < 40) {
      i++;
      spawner.spawnEnemyInMap(rank[Random.Range(4, 6)] + basetype[Random.Range(0, 3)], spawner.ranXPos(), Random.Range(-2f, 2f), true);
      yield return new WaitForSeconds(1f);
    }
    spawner.LastWaveEnemiesCleared();
  }









}
