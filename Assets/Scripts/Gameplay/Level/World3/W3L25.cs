using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W3L25 : MonoBehaviour, IGetLevelDataInterface {
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
	string[] rank = new string[6] { "Nano", "Micro", "Kilo", "Mega", "Giga", "Ultimate" };
	string[] basetype = new string[3] { "Basic", "Armored", "Shield" };
	string[] highrank = new string[4] { "", "Meso", "Macro", "Hyper" };
	string[] type = new string[2] { "Outlier", "Ticker" };
	IEnumerator wave1() {
		spawner.spawnEnemy("HyperTicker", 0f, 10f);
		yield return new WaitForSeconds(10f);
		spawner.spawnEnemy("HyperTicker", 5f, 10f);
		yield return new WaitForSeconds(10f);
		spawner.spawnEnemy("HyperTicker", -5f, 10f);
		yield return new WaitForSeconds(10f);
		spawner.AllTriggerEnemiesCleared();
	}
	IEnumerator wave2() {
		spawner.spawnEnemy("Gigantodon", 0f, 10f, LevelSpawner.addToList.Specific, true);
		yield return new WaitForSeconds(20f);
		spawner.waveCleared();
	}

	IEnumerator hspawner(string name, float period) {
		while (spawner.setEnemies.Count > 0) {
			spawner.spawnEnemy(highrank[Random.Range(0, 4)] + name, spawner.ranXPos(), 10f);
			yield return new WaitForSeconds(Random.Range(period, period + period / 4f));
		}
	}
	IEnumerator nspawner() {
		while (spawner.setEnemies.Count > 0) {
			spawner.spawnEnemy(rank[Random.Range(0, 6)] + basetype[Random.Range(0, 3)], spawner.ranXPos(), 10f);
			yield return new WaitForSeconds(Random.Range(3f, 5f));
		}
	}
	IEnumerator wave3() {
		StartCoroutine(hspawner("Outlier", 20f));
		StartCoroutine(hspawner("Ticker", 15f));
		yield return StartCoroutine(nspawner());
		spawner.LastWaveEnemiesCleared();
	}
}
