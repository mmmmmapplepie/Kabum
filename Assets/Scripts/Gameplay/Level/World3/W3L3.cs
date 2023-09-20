using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W3L3 : MonoBehaviour, IGetLevelDataInterface {
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

	string[] rank = new string[3] { "", "Meso", "Macro" };
	IEnumerator wave1() {
		int summon = 0;
		while (summon < 40) {
			spawner.spawnEnemy(rank[Random.Range(0, 3)] + "Enigma", spawner.ranXPos(), 10f);
			yield return new WaitForSeconds(0.5f);
			summon++;
		}
		spawner.AllTriggerEnemiesCleared();
	}

	IEnumerator tickerSummon() {
		yield return new WaitForSeconds(30f);
		int summon = 0;
		while (summon < 15) {
			spawner.spawnEnemy("MacroTicker", spawner.ranXPos(), 10f);
			yield return new WaitForSeconds(1f);
			summon++;
		}
	}
	IEnumerator wave2() {
		int summon = 0;
		StartCoroutine(tickerSummon());
		while (summon < 80) {
			spawner.spawnEnemy(rank[Random.Range(0, 3)] + "Enigma", spawner.ranXPos(), 10f);
			yield return new WaitForSeconds(0.5f);
			summon++;
		}
		yield return new WaitForSeconds(5f);
		spawner.spawnEnemyInMap("Booster", -5f, 8f, true, LevelSpawner.addToList.Specific, true);
		spawner.spawnEnemyInMap("Booster", -3f, 8f, true, LevelSpawner.addToList.Specific, true);
		spawner.spawnEnemyInMap("Booster", -1f, 8f, true, LevelSpawner.addToList.Specific, true);
		spawner.spawnEnemyInMap("Booster", 1f, 8f, true, LevelSpawner.addToList.Specific, true);
		spawner.spawnEnemyInMap("Booster", 3f, 8f, true, LevelSpawner.addToList.Specific, true);
		spawner.spawnEnemyInMap("Booster", 5f, 8f, true, LevelSpawner.addToList.Specific, true);
		yield return new WaitForSeconds(3f);
		while (spawner.setEnemies.Count > 0) {
			spawner.spawnEnemy(rank[Random.Range(0, 3)] + "Enigma", spawner.ranXPos(), 10f);
			yield return new WaitForSeconds(1f);
		}
		spawner.LastWaveEnemiesCleared();
	}
}
