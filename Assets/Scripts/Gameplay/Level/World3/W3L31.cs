using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W3L31 : MonoBehaviour, IGetLevelDataInterface {
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
  string[] highrank = new string[4] { "", "Meso", "Macro", "Hyper" };
  IEnumerator hspawner(string name, float period) {
    while (spawner.setEnemies.Count > 0 || !done) {
      spawner.spawnEnemy(highrank[Random.Range(0, 4)] + name, spawner.ranXPos(), 10f);
      yield return new WaitForSeconds(Random.Range(0f, period));
    }
  }
  IEnumerator wave1() {
    for (int i = 0; i < 10; i++) {
      spawner.spawnEnemyInMap(highrank[0] + "Vessel", spawner.ranXPos(), Random.Range(0, 10f), true);
      spawner.spawnEnemyInMap(highrank[1] + "Vessel", spawner.ranXPos(), Random.Range(0, 10f), true);
      spawner.spawnEnemyInMap(highrank[2] + "Vessel", spawner.ranXPos(), Random.Range(0, 10f), true);
      spawner.spawnEnemyInMap(highrank[3] + "Vessel", spawner.ranXPos(), Random.Range(0, 10f), true);
      yield return new WaitForSeconds(2f);
    }
    spawner.AllTriggerEnemiesCleared();
  }

  IEnumerator wave2() {
    StartCoroutine(hspawner("Vessel", 5f));
    yield return new WaitForSeconds(10f);
    spawner.spawnEnemyInMap("HyperJammer", spawner.ranXPos(), 8f, true);
    spawner.spawnEnemyInMap("HyperDisruptor", spawner.ranXPos(), 8f, true);
    yield return new WaitForSeconds(20f);
    spawner.spawnEnemyInMap("HyperJammer", spawner.ranXPos(), 8f, true);
    spawner.spawnEnemyInMap("HyperDisruptor", spawner.ranXPos(), 8f, true);
    yield return new WaitForSeconds(20f);
    spawner.spawnEnemyInMap("HyperJammer", spawner.ranXPos(), 8f, true);
    spawner.spawnEnemyInMap("HyperDisruptor", spawner.ranXPos(), 8f, true);
    done = true;
    spawner.LastWaveEnemiesCleared();
  }
}
