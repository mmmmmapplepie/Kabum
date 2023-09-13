using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W3L30 : MonoBehaviour, IGetLevelDataInterface {
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
  string[] highrank = new string[4] { "", "Meso", "Macro", "Hyper" };
  IEnumerator nspawner() {
    while (spawner.setEnemies.Count > 0 || !done) {
      spawner.spawnEnemy("Ultimate" + basetype[Random.Range(0, 3)], spawner.ranXPos(), 10f);
      yield return new WaitForSeconds(Random.Range(1f, 5f));
    }
  }
  IEnumerator hspawner(string name, float period) {
    while (spawner.setEnemies.Count > 0 || !done) {
      spawner.spawnEnemy(highrank[Random.Range(0, 4)] + name, spawner.ranXPos(), 10f);
      yield return new WaitForSeconds(period);
    }
  }
  void bigBurst(bool singlefile, int num) {
    float x = spawner.ranXPos();
    int i = 0;
    while (i < num) {
      i++;
      if (!singlefile) x = spawner.ranXPos();
      spawner.spawnEnemyInMap("Ultimate" + basetype[Random.Range(0, 3)], x, Random.Range(-3f, 10f), true);
    }
  }
  IEnumerator wave1() {
    StartCoroutine(nspawner());
    yield return new WaitForSeconds(10f);
    bigBurst(false, 100);
    yield return new WaitForSeconds(5f);
    bigBurst(true, 100);
    yield return new WaitForSeconds(20f);
    spawner.waveCleared();
  }
  IEnumerator wave2() {
    StartCoroutine(hspawner("Vessel", 5f));
    yield return new WaitForSeconds(30f);
    spawner.spawnEnemyInMap("HyperDisruptor", 0f, 10f, true, LevelSpawner.addToList.Specific, true);
    spawner.spawnEnemyInMap("HyperDisruptor", 0f, 10f, true, LevelSpawner.addToList.Specific, true);
    spawner.spawnEnemyInMap("HyperDisruptor", 0f, 10f, true, LevelSpawner.addToList.Specific, true);
    yield return new WaitForSeconds(30f);
    bigBurst(true, 50);
    yield return new WaitForSeconds(20f);
    bigBurst(false, 50);
    done = true;
    yield return new WaitForSeconds(5f);
    spawner.LastWaveEnemiesCleared();
  }
}
