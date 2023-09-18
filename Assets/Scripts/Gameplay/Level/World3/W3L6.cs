using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W3L6 : MonoBehaviour, IGetLevelDataInterface {
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

	bool havocSpawned = false;
	IEnumerator havocs() {
		yield return new WaitForSeconds(30f);
		spawner.spawnEnemyInMap("HyperHavoc", -4f, 8f, true, LevelSpawner.addToList.Specific, true);
		spawner.spawnEnemyInMap("HyperHavoc", -0f, 8f, true, LevelSpawner.addToList.Specific, true);
		spawner.spawnEnemyInMap("HyperHavoc", 4f, 8f, true, LevelSpawner.addToList.Specific, true);
		havocSpawned = true;
	}
	IEnumerator wave1() {
		StartCoroutine(havocs());
		yield return new WaitForSeconds(2f);
		float time = Time.time;
		while (spawner.setEnemies.Count > 0 || !havocSpawned) {
			spawner.spawnEnemyInMap("Ticker", spawner.ranXPos(), Random.Range(0f, 10f), true);
			yield return new WaitForSeconds(0.7f);
		};
		spawner.LastWaveEnemiesCleared();
	}
}
