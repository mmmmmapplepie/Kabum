using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W2L23 : MonoBehaviour, IGetLevelDataInterface {
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
  string[] pre = new string[2] { "", "Meso" };
  string[] eneT = new string[4] { "Outlier", "Vessel", "Enigma", "Reflector" };
  IEnumerator wave1() {
    for (int i = 0; i < 30; i++) {
      string type = eneT[Random.Range(0, 4)];
      spawner.spawnEnemy(type, spawner.ranXPos(), 10f);
      if (type == "Outlier") yield return new WaitForSeconds(4f);
      if (type == "Vessel") yield return new WaitForSeconds(1f);
      yield return new WaitForSeconds(2f);
    }
    spawner.AllTriggerEnemiesCleared();
  }
  IEnumerator wave2() {
    for (int i = 0; i < 30; i++) {
      string type = eneT[Random.Range(0, 4)];
      spawner.spawnEnemy("Meso" + type, spawner.ranXPos(), 10f);
      if (type == "Outlier") yield return new WaitForSeconds(3f);
      if (type == "Vessel") yield return new WaitForSeconds(1f);
      yield return new WaitForSeconds(2f);
    }
    spawner.LastWaveEnemiesCleared();
  }
}
