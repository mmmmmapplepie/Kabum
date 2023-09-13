using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W3L16 : MonoBehaviour, IGetLevelDataInterface {
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

  IEnumerator wave1() {
    spawner.spawnEnemy("Carrier", spawner.ranXPos(), 10f);
    spawner.spawnEnemy("Carrier", spawner.ranXPos(), 10f);
    spawner.spawnEnemy("Carrier", spawner.ranXPos(), 10f);
    spawner.spawnEnemy("Carrier", spawner.ranXPos(), 10f);
    spawner.spawnEnemy("Carrier", spawner.ranXPos(), 10f);
    spawner.spawnEnemy("Carrier", spawner.ranXPos(), 10f);
    spawner.spawnEnemy("Carrier", spawner.ranXPos(), 10f);
    spawner.spawnEnemy("Carrier", spawner.ranXPos(), 10f);
    spawner.spawnEnemy("Carrier", spawner.ranXPos(), 10f);
    spawner.spawnEnemy("Carrier", spawner.ranXPos(), 10f);
    yield return new WaitForSeconds(20f);
    spawner.waveCleared();
  }
  IEnumerator wave2() {
    spawner.spawnEnemy("Colossus", spawner.ranXPos(), Random.Range(5f, 10f), LevelSpawner.addToList.Specific, true);
    spawner.spawnEnemy("Colossus", spawner.ranXPos(), Random.Range(5f, 10f), LevelSpawner.addToList.Specific, true);
    spawner.spawnEnemy("Colossus", spawner.ranXPos(), Random.Range(5f, 10f), LevelSpawner.addToList.Specific, true);
    spawner.spawnEnemy("Colossus", spawner.ranXPos(), Random.Range(5f, 10f), LevelSpawner.addToList.Specific, true);
    spawner.spawnEnemy("Colossus", spawner.ranXPos(), Random.Range(5f, 10f), LevelSpawner.addToList.Specific, true);
    spawner.spawnEnemy("Carrier", spawner.ranXPos(), Random.Range(5f, 10f), LevelSpawner.addToList.Specific, true);
    spawner.spawnEnemy("Carrier", spawner.ranXPos(), Random.Range(5f, 10f), LevelSpawner.addToList.Specific, true);
    spawner.spawnEnemy("Carrier", spawner.ranXPos(), Random.Range(5f, 10f), LevelSpawner.addToList.Specific, true);
    spawner.spawnEnemy("Carrier", spawner.ranXPos(), Random.Range(5f, 10f), LevelSpawner.addToList.Specific, true);
    spawner.spawnEnemy("Carrier", spawner.ranXPos(), Random.Range(5f, 10f), LevelSpawner.addToList.Specific, true);
    while (spawner.setEnemies.Count > 0) {
      string ene = level.Enemies[Random.Range(0, level.Enemies.Count)].enemyPrefab.name;
      spawner.spawnEnemyInMap(ene, spawner.ranXPos(), Random.Range(0f, 10f), true);
      yield return new WaitForSeconds(10f);
    }
    spawner.LastWaveEnemiesCleared();
  }
}
