using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W3L34 : MonoBehaviour, IGetLevelDataInterface {
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
  string[] type = new string[2] { "Engima", "Ticker" };
  void burst(int num, string name) {
    int i = 0;
    float x = spawner.ranXPos();
    while (i < num) {
      i++;
      spawner.spawnEnemy(highrank[Random.Range(0, 4)] + name, x, 10f);
    }
  }
  IEnumerator wave1() {
    for (int i = 0; i < 20; i++) {
      if (i % 2 == 0) {
        burst(i + 10, "Ticker");
      } else {
        burst(i + 15, "Enigma");
      }
      yield return new WaitForSeconds(6f);
    }
    spawner.LastWaveEnemiesCleared();
  }






}
