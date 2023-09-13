using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W3L23 : MonoBehaviour, IGetLevelDataInterface {
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
    while (i < 10) {
      i++;
      spawner.spawnEnemy(highrank[Random.Range(0, 4)] + "Zipper", spawner.ranXPos(), 10f);
      spawner.spawnEnemy(highrank[Random.Range(0, 4)] + "Shifter", spawner.ranXPos(), 10f);
    }
    i = 0;
    yield return new WaitForSeconds(10f);
    while (i < 10) {
      i++;
      spawner.spawnEnemy(highrank[Random.Range(0, 4)] + "Zipper", spawner.ranXPos(), 10f);
      spawner.spawnEnemy(highrank[Random.Range(0, 4)] + "Shifter", spawner.ranXPos(), 10f);
    }
    i = 0;
    yield return new WaitForSeconds(8f);
    while (i < 10) {
      i++;
      spawner.spawnEnemy(highrank[Random.Range(0, 4)] + "Zipper", spawner.ranXPos(), 10f);
      spawner.spawnEnemy(highrank[Random.Range(0, 4)] + "Shifter", spawner.ranXPos(), 10f);
    }
    spawner.AllTriggerEnemiesCleared();
  }
  void bundles(int num) {
    int i = 0;
    while (i < num) {
      i++;
      spawner.spawnEnemy(highrank[Random.Range(0, 4)] + "Zipper", spawner.ranXPos(), 10f);
      spawner.spawnEnemy(highrank[Random.Range(0, 4)] + "Shifter", spawner.ranXPos(), 10f);
    }
  }
  IEnumerator shifterzipper() {
    bundles(10);
    yield return new WaitForSeconds(10f);
    bundles(10);
    yield return new WaitForSeconds(10f);
    bundles(10);
    yield return new WaitForSeconds(10f);
    bundles(10);
    yield return new WaitForSeconds(10f);
    bundles(10);
    yield return new WaitForSeconds(10f);
    bundles(10);
    yield return new WaitForSeconds(10f);
  }
  IEnumerator wave2() {
    StartCoroutine(shifterzipper());
    yield return new WaitForSeconds(15f);
    for (int i = -5; i <= 5; i++) {
      spawner.spawnEnemy("HyperTicker", (float)i, 10f);
    }
    yield return new WaitForSeconds(5f);
    spawner.spawnEnemyInMap("HyperBooster", 0f, 9f, true);
    spawner.spawnEnemyInMap("HyperBooster", 0f, 9f, true);
    spawner.LastWaveEnemiesCleared();
  }
}
