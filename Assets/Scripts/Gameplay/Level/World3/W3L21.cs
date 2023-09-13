using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W3L21 : MonoBehaviour, IGetLevelDataInterface {
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

  string[] rank = new string[4] { "", "Meso", "Macro", "Hyper" };
  IEnumerator wave1() {
    int i = 0;
    while (i < 30) {
      i++;
      spawner.spawnEnemyInMap(rank[Random.Range(0, 4)] + "Shifter", spawner.ranXPos(), Random.Range(0, 10f), false);
      yield return new WaitForSeconds(Random.Range(0f, 2f));
    }
    yield return new WaitForSeconds(10f);
    i = 0;
    while (i < 30) {
      i++;
      spawner.spawnEnemyInMap(rank[Random.Range(1, 4)] + "Shifter", spawner.ranXPos(), Random.Range(0, 10f), false);
      yield return new WaitForSeconds(Random.Range(0f, 1.5f));
    }
    yield return new WaitForSeconds(10f);
    i = 0;
    while (i < 30) {
      i++;
      spawner.spawnEnemyInMap(rank[Random.Range(2, 4)] + "Shifter", spawner.ranXPos(), Random.Range(0, 10f), false);
      yield return new WaitForSeconds(Random.Range(0f, 1.2f));
    }
    spawner.LastWaveEnemiesCleared();
  }
}
