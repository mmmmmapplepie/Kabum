using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W3L36 : MonoBehaviour, IGetLevelDataInterface {
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
  string[] highrank = new string[4] { "", "Meso", "Macro", "Hyper" };
  IEnumerator wave1() {
    int i = 0;
    while (i < 30) {
      i++;
      spawner.spawnEnemy(highrank[Random.Range(0, 3)] + "Reflector", spawner.ranXPos(), 10f);
      yield return new WaitForSeconds(1f);
    }
    spawner.AllTriggerEnemiesCleared();
  }
  IEnumerator wave2() {
    int i = 0;
    while (i < 30) {
      i++;
      spawner.spawnEnemy(highrank[Random.Range(0, 4)] + "Reflector", spawner.ranXPos(), 10f);
    }
    yield return new WaitForSeconds(20f);
    i = 0;
    float x = spawner.ranXPos();
    while (i < 30) {
      i++;
      spawner.spawnEnemy("HyperReflector", x, 10f);
    }
    spawner.LastWaveEnemiesCleared();
  }
}
