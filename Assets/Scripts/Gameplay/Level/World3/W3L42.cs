using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W3L42 : MonoBehaviour, IGetLevelDataInterface {
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
  string[] type = new string[3] { "Outlier", "Enigma", "Reflector" };
  string[] btype = new string[7] { "Booster", "Havoc", "Protector", "Maintainer", "Armory", "Disruptor", "Jammer" };
  bool done = false;
  IEnumerator tick() {
    while (!done) {
      spawner.spawnEnemy(highrank[Random.Range(2, 4)] + type[Random.Range(0, 3)], spawner.ranXPos(), 10f);
      yield return new WaitForSeconds(Random.Range(2f, 6f));
    }
  }
  IEnumerator buff() {
    while (!done) {
      spawner.spawnEnemy(btype[Random.Range(0, 7)], spawner.ranXPos(), 10f);
      yield return new WaitForSeconds(Random.Range(5f, 10f));
    }
  }
  IEnumerator wave1() {
    StartCoroutine(tick());
    yield return new WaitForSeconds(20f);
    spawner.waveCleared();
  }

  IEnumerator wave2() {
    StartCoroutine(buff());
    StartCoroutine(tick());
    StartCoroutine(tick());
    yield return new WaitForSeconds(80f);
    done = true;
    spawner.LastWaveEnemiesCleared();
  }





}
