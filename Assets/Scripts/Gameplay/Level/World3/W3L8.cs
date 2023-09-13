using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W3L8 : MonoBehaviour, IGetLevelDataInterface {
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

  bool stopsummons = false;
  IEnumerator summonBasic(string name, float x) {
    while (!stopsummons) {
      spawner.spawnEnemy(name, x, 10f);
      yield return new WaitForSeconds(Random.Range(2f, 4f));
    }
  }
  IEnumerator wave1() {
    StartCoroutine(summonBasic("GigaBasic", 0f));
    StartCoroutine(summonBasic("GigaArmored", 5f));
    StartCoroutine(summonBasic("GigaShield", -5f));
    yield return new WaitForSeconds(30f);
    spawner.waveCleared();
  }
  string[] rank = new string[3] { "", "Meso", "Macro" };
  IEnumerator wave2() {
    int summon = 0;
    while (summon < 40) {
      summon++;
      spawner.spawnEnemy(rank[Random.Range(0, 3)] + "Reflector", spawner.ranXPos(), 10f, LevelSpawner.addToList.Specific, true);
      yield return new WaitForSeconds(1f);
    }
    while (!stopsummons) {
      if (spawner.setEnemies.Count == 0) {
        stopsummons = true;
      }
      yield return null;
    }
    spawner.LastWaveEnemiesCleared();
  }
}
