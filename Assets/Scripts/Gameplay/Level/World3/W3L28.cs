using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W3L28 : MonoBehaviour, IGetLevelDataInterface {
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

  bool done = false;
  string[] rank = new string[6] { "Nano", "Micro", "Kilo", "Mega", "Giga", "Ultimate" };
  string[] basetype = new string[3] { "Basic", "Armored", "Shield" };
  IEnumerator nspawner() {
    while (spawner.setEnemies.Count > 0 || !done) {
      spawner.spawnEnemy(rank[Random.Range(0, 6)] + "Basic", spawner.ranXPos(), 10f);
      yield return new WaitForSeconds(Random.Range(1f, 2f));
    }
  }
  IEnumerator wave1() {
    StartCoroutine(nspawner());
    yield return new WaitForSeconds(10f);
    int i = 0;
    while (i < 10) {
      i++;
      spawner.spawnEnemyInMap("GigaBasic", spawner.ranXPos(), 0f, true);
    }
    yield return new WaitForSeconds(20f);
    spawner.spawnEnemyInMap("Colossus", 0f, 9f, true, LevelSpawner.addToList.Specific, true);
    spawner.spawnEnemyInMap("Colossus", 0f, 9f, true, LevelSpawner.addToList.Specific, true);
    yield return new WaitForSeconds(5f);
    done = true;
    spawner.LastWaveEnemiesCleared();
  }

}
