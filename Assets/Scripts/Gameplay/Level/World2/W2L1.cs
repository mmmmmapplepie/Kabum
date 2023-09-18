using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W2L1 : MonoBehaviour, IGetLevelDataInterface {
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
		audio.ChangeBGM("World2");
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

	string[] smallAr = new string[3] { "NanoArmored", "MicroArmored", "KiloArmored" };
	string[] bigAr = new string[3] { "MegaArmored", "GigaArmored", "UltimateArmored" };
	IEnumerator wave1() {
		int waves = 2;
		for (int i = 0; i < waves; i++) {
			for (int ene = 0; ene < 5; ene++) {
				spawner.spawnEnemy(smallAr[Random.Range(0, 3)], 0f, 10f);
			}
			yield return new WaitForSeconds(10f);
			for (int ene = 0; ene < 5; ene++) {
				spawner.spawnEnemy(smallAr[Random.Range(0, 3)], 0f, 10f);
			}
			yield return new WaitForSeconds(10f);
			for (int ene = 0; ene < 5; ene++) {
				spawner.spawnEnemy(smallAr[Random.Range(0, 3)], 0f, 10f);
			}
			yield return new WaitForSeconds(10f);
			for (int ene = 0; ene < 1; ene++) {
				spawner.spawnEnemy(bigAr[Random.Range(0, 3)], 0f, 10f);
			}
		}
		yield return new WaitForSeconds(5f);
		spawner.waveCleared();
	}

	IEnumerator wave2() {
		yield return new WaitForSeconds(10f);
		for (int ene = 0; ene < 4; ene++) {
			spawner.spawnEnemy(bigAr[Random.Range(0, 3)], 5f, 10f);
		}
		yield return new WaitForSeconds(10f);
		for (int ene = 0; ene < 3; ene++) {
			spawner.spawnEnemy(bigAr[Random.Range(0, 3)], -5f, 10f);
		}
		spawner.LastWaveEnemiesCleared();
	}
}