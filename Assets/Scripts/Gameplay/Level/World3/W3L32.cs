using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W3L32 : MonoBehaviour, IGetLevelDataInterface {
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
  IEnumerator nspawner() {
    while (spawner.setEnemies.Count > 0) {
      spawner.spawnEnemy(rank[Random.Range(0, 3)] + basetype[Random.Range(0, 3)], spawner.ranXPos(), 10f);
      yield return new WaitForSeconds(Random.Range(0f, 5f));
    }
  }
  IEnumerator wave1() {
    spawner.spawnEnemyInMap("Carrier", 0f, 10f, true, LevelSpawner.addToList.Specific, true);
    yield return new WaitForSeconds(5f);
    spawner.spawnEnemyInMap("Core", 0f, 10f, true, LevelSpawner.addToList.Specific, true);
    yield return new WaitForSeconds(5f);
    yield return StartCoroutine(nspawner());
    spawner.LastWaveEnemiesCleared();
  }





}
