using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class SecretStage : MonoBehaviour, IGetLevelDataInterface {
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
	string[] baserank = new string[6] { "Nano", "Micro", "Kilo", "Mega", "Giga", "Ultimate" };
	string[] basetype = new string[3] { "Basic", "Armored", "Shield" };
	string[] highrank = new string[4] { "", "Meso", "Macro", "Hyper" };
	string[] type = new string[2] { "Outlier", "Ticker" };

	float ran(float min = -5f, float max = 5f) {
		return Random.Range(min, max);
	}
	bool ranbool() {
		return Random.Range(0, 2) == 0 ? true : false;
	}

	List<Coroutine> infiniteSpawnCoroutines = new List<Coroutine>();

	//periodavg is the avgtime you want per item (even for burst);
	IEnumerator genspawner(string name, float periodAvg, int burstNumMin, int burstNumMax, bool random = true, bool burstsetting = false) {
		yield return new WaitForSeconds(Random.Range(0f, periodAvg));
		float xpos = ran();
		while (spawner.setEnemies.Count > 0 || !done) {
			if (burstsetting) {
				int num = Random.Range(burstNumMin, burstNumMax + 1);
				normburst(name, 10f, num, random);
				float normalizedWait = periodAvg * (float)num;
				yield return new WaitForSeconds(Random.Range(normalizedWait, normalizedWait + normalizedWait / 4f));
			} else {
				if (random) xpos = ran();
				spawner.spawnEnemy(name, xpos, 10f);
				yield return new WaitForSeconds(Random.Range(periodAvg, periodAvg + periodAvg / 4f));
			}
		}
	}
	void normburst(string name, float x, int num, bool random = false) {
		int i = 0;
		float xpos = ran();
		while (i < num) {
			i++;
			if (random) xpos = ran();
			spawner.spawnEnemy(name, xpos, 10f);
		}
	}

	//few nano and micro
	IEnumerator wave1() {
		int i = 0;
		while (i < 5) {
			spawner.spawnEnemy(baserank[Random.Range(0, 2)] + basetype[Random.Range(0, 3)], ran(), 10f);
			i++;
			yield return new WaitForSeconds(1f);
		}
		spawner.AllTriggerEnemiesCleared();
	}

	//a ticker
	IEnumerator wave2() {
		spawner.spawnEnemyInMap("Ticker", 3f, 8f, true);
		yield return new WaitForSeconds(5f);
		spawner.AllTriggerEnemiesCleared();
	}

	//some kilos
	IEnumerator wave3() {
		int i = 0;
		while (i < 10) {
			spawner.spawnEnemy("Kilo" + basetype[Random.Range(0, 3)], ran(), 10f);
			i++;
			yield return new WaitForSeconds(2f);
		}
		spawner.AllTriggerEnemiesCleared();
	}

	//some shifters
	IEnumerator wave4() {
		int i = 0;
		while (i < 8) {
			spawner.spawnEnemy("Shifter", ran(), 10f);
			i++;
			yield return new WaitForSeconds(2f);
		}
		spawner.AllTriggerEnemiesCleared();
	}

	//one outlier
	IEnumerator wave5() {
		spawner.spawnEnemyInMap("Outlier", 0f, 8f, true);
		yield return new WaitForSeconds(5f);
		spawner.AllTriggerEnemiesCleared();
	}

	//bunch of nano/micro
	IEnumerator wave6() {
		int i = 0;
		while (i < 40) {
			spawner.spawnEnemy(baserank[Random.Range(0, 2)] + basetype[Random.Range(0, 3)], ran(), 10f);
			i++;
			yield return new WaitForSeconds(0.5f);
		}
		spawner.AllTriggerEnemiesCleared();
	}

	//continuous spawn of nano's start
	IEnumerator wave7() {
		infiniteSpawnCoroutines.Add(StartCoroutine(genspawner("NanoBasic", 3f, 4, 7, ranbool(), ranbool())));
		infiniteSpawnCoroutines.Add(StartCoroutine(genspawner("NanoArmored", 3f, 4, 7, ranbool(), ranbool())));
		infiniteSpawnCoroutines.Add(StartCoroutine(genspawner("NanoShield", 3f, 4, 7, ranbool(), ranbool())));
		yield return new WaitForSeconds(15f);
		spawner.waveCleared();
	}

	//few vessels
	IEnumerator wave8() {
		spawner.spawnEnemyInMap("MesoVessel", -2f, 8f, true);
		spawner.spawnEnemyInMap("MesoVessel", 2f, 8f, true);
		spawner.spawnEnemyInMap("Vessel", -2f, 6f, true);
		spawner.spawnEnemyInMap("Vessel", 2f, 6f, true);
		yield return new WaitForSeconds(30f);
		spawner.waveCleared();
	}


	//continuous micro
	IEnumerator wave9() {
		infiniteSpawnCoroutines.Add(StartCoroutine(genspawner("MicroBasic", 4f, 3, 6, ranbool(), ranbool())));
		infiniteSpawnCoroutines.Add(StartCoroutine(genspawner("MicroArmored", 4f, 3, 6, ranbool(), ranbool())));
		infiniteSpawnCoroutines.Add(StartCoroutine(genspawner("MicroShield", 4f, 3, 6, ranbool(), ranbool())));
		yield return new WaitForSeconds(15f);
		spawner.waveCleared();
	}


	//carrier ; continuous kilo
	IEnumerator wave10() {
		spawner.spawnEnemyInMap("Carrier", 0f, 9f, true, LevelSpawner.addToList.Specific, true);
		infiniteSpawnCoroutines.Add(StartCoroutine(genspawner("KiloBasic", 6f, 1, 5, ranbool(), ranbool())));
		infiniteSpawnCoroutines.Add(StartCoroutine(genspawner("KiloArmored", 6f, 1, 5, ranbool(), ranbool())));
		infiniteSpawnCoroutines.Add(StartCoroutine(genspawner("KiloShield", 6f, 1, 5, ranbool(), ranbool())));
		yield return new WaitForSeconds(30f);
		spawner.waveCleared();
	}


	//core    continuous shifter
	IEnumerator wave11() {
		spawner.spawnEnemyInMap("Core", 0f, 9f, true, LevelSpawner.addToList.Specific, true);
		infiniteSpawnCoroutines.Add(StartCoroutine(genspawner("Shifter", 4f, 2, 4, ranbool(), ranbool())));
		yield return new WaitForSeconds(30f);
		spawner.waveCleared();
	}


	//mesocarrier   cont teleporter/zipper
	IEnumerator wave12() {
		spawner.spawnEnemyInMap("Colossus", 0f, 9f, true, LevelSpawner.addToList.Specific, true);
		infiniteSpawnCoroutines.Add(StartCoroutine(genspawner("Teleporter", 8f, 2, 6, ranbool(), ranbool())));
		infiniteSpawnCoroutines.Add(StartCoroutine(genspawner("Zipper", 10f, 3, 8, ranbool(), ranbool())));
		yield return new WaitForSeconds(30f);
		spawner.waveCleared();
	}


	//mesocore;    cont enigma
	IEnumerator wave13() {
		spawner.spawnEnemyInMap("MesoCore", 0f, 9f, true, LevelSpawner.addToList.Specific, true);
		infiniteSpawnCoroutines.Add(StartCoroutine(genspawner("Enigma", 10f, 2, 4, ranbool(), ranbool())));
		yield return new WaitForSeconds(30f);
		spawner.waveCleared();
	}

	//continuous meso shifter/teleporter;
	IEnumerator wave14() {
		infiniteSpawnCoroutines.Add(StartCoroutine(genspawner("MesoShifter", 10f, 2, 6, ranbool(), ranbool())));
		infiniteSpawnCoroutines.Add(StartCoroutine(genspawner("MesoTeleporter", 16f, 2, 8, ranbool(), ranbool())));
		yield return new WaitForSeconds(15f);
		spawner.waveCleared();
	}


	//couplad;  continuous meso engima; reflector, outlier
	IEnumerator wave15() {
		spawner.spawnEnemyInMap("CoupladSeeker", 5f, 9f, true, LevelSpawner.addToList.Specific, true);
		spawner.spawnEnemyInMap("CoupladFollower", -5f, 9f, true, LevelSpawner.addToList.Specific, true);
		infiniteSpawnCoroutines.Add(StartCoroutine(genspawner("MesoEnigma", 20f, 2, 10, ranbool(), ranbool())));
		infiniteSpawnCoroutines.Add(StartCoroutine(genspawner("Reflector", 25f, 1, 8, ranbool(), ranbool())));
		infiniteSpawnCoroutines.Add(StartCoroutine(genspawner("Outlier", 30f, 2, 4, ranbool(), ranbool())));
		yield return new WaitForSeconds(30f);
		spawner.waveCleared();
	}

	//continuous meso reflector, ticker/vessel
	IEnumerator wave16() {
		infiniteSpawnCoroutines.Add(StartCoroutine(genspawner("Ticker", 20f, 2, 6, ranbool(), ranbool())));
		infiniteSpawnCoroutines.Add(StartCoroutine(genspawner("Vessel", 20f, 1, 4, ranbool(), ranbool())));
		infiniteSpawnCoroutines.Add(StartCoroutine(genspawner("MesoReflector", 30f, 1, 8, ranbool(), ranbool())));
		yield return new WaitForSeconds(15f);
		spawner.waveCleared();
	}


	//macro carrier ; continuous meso zipper ; macro shifter
	IEnumerator wave17() {
		spawner.spawnEnemyInMap("Leviathan", 0f, 9f, true, LevelSpawner.addToList.Specific, true);
		infiniteSpawnCoroutines.Add(StartCoroutine(genspawner("MesoZipper", 12f, 4, 7, ranbool(), ranbool())));
		infiniteSpawnCoroutines.Add(StartCoroutine(genspawner("MacroShifter", 14f, 1, 5, ranbool(), ranbool())));
		yield return new WaitForSeconds(30f);
		spawner.waveCleared();
	}


	//macrocore; continuous macro enigma ;meso vessel
	IEnumerator wave18() {
		spawner.spawnEnemyInMap("MacroCore", 0f, 9f, true, LevelSpawner.addToList.Specific, true);
		infiniteSpawnCoroutines.Add(StartCoroutine(genspawner("MacroEnigma", 25f, 1, 7, ranbool(), ranbool())));
		infiniteSpawnCoroutines.Add(StartCoroutine(genspawner("MesoVessel", 25f, 1, 4, ranbool(), ranbool())));
		yield return new WaitForSeconds(30f);
		spawner.waveCleared();
	}

	//continuous megas
	IEnumerator wave19() {
		infiniteSpawnCoroutines.Add(StartCoroutine(genspawner("MegaBasic", 13f, 1, 4, ranbool(), ranbool())));
		infiniteSpawnCoroutines.Add(StartCoroutine(genspawner("MegaArmored", 13f, 1, 4, ranbool(), ranbool())));
		infiniteSpawnCoroutines.Add(StartCoroutine(genspawner("MegaShield", 13f, 1, 4, ranbool(), ranbool())));
		yield return new WaitForSeconds(15f);
		spawner.waveCleared();
	}


	//maxcouplad   cotinuous meso ticker/outlier macro teleporter/outlier
	IEnumerator wave20() {
		spawner.spawnEnemyInMap("MaxCoupladSeeker", 0f, 9f, true, LevelSpawner.addToList.Specific, true);
		spawner.spawnEnemyInMap("MaxCoupladFollower", 0f, 9f, true, LevelSpawner.addToList.Specific, true);
		infiniteSpawnCoroutines.Add(StartCoroutine(genspawner("MesoTicker", 22f, 1, 6, ranbool(), ranbool())));
		infiniteSpawnCoroutines.Add(StartCoroutine(genspawner("MesoOutlier", 35f, 1, 4, ranbool(), ranbool())));
		infiniteSpawnCoroutines.Add(StartCoroutine(genspawner("MacroTeleporter", 13f, 1, 4, ranbool(), ranbool())));
		infiniteSpawnCoroutines.Add(StartCoroutine(genspawner("MacroOutlier", 40f, 1, 3, ranbool(), ranbool())));
		yield return new WaitForSeconds(30f);
		spawner.waveCleared();
	}


	//cont macro ticker/zipper/vessel/reflector
	IEnumerator wave21() {
		infiniteSpawnCoroutines.Add(StartCoroutine(genspawner("MacroTicker", 25f, 1, 5, ranbool(), ranbool())));
		infiniteSpawnCoroutines.Add(StartCoroutine(genspawner("MacroZipper", 15f, 2, 6, ranbool(), ranbool())));
		infiniteSpawnCoroutines.Add(StartCoroutine(genspawner("MacroVessel", 30f, 1, 4, ranbool(), ranbool())));
		infiniteSpawnCoroutines.Add(StartCoroutine(genspawner("MacroReflector", 40f, 1, 6, ranbool(), ranbool())));
		yield return new WaitForSeconds(15f);
		spawner.waveCleared();
	}


	//cont hyp shifter/outlier
	IEnumerator wave22() {
		infiniteSpawnCoroutines.Add(StartCoroutine(genspawner("HyperShifter", 20f, 1, 6, ranbool(), ranbool())));
		infiniteSpawnCoroutines.Add(StartCoroutine(genspawner("HyperOutlier", 45f, 1, 3, ranbool(), ranbool())));
		yield return new WaitForSeconds(15f);
		spawner.waveCleared();
	}


	//minima  continuous gigas
	IEnumerator wave23() {
		spawner.spawnEnemyInMap("Minima", 0f, 9f, true, LevelSpawner.addToList.Specific, true);
		infiniteSpawnCoroutines.Add(StartCoroutine(genspawner("GigaBasic", 18f, 1, 3, ranbool(), ranbool())));
		infiniteSpawnCoroutines.Add(StartCoroutine(genspawner("GigaArmored", 18f, 1, 3, ranbool(), ranbool())));
		infiniteSpawnCoroutines.Add(StartCoroutine(genspawner("GigaShield", 18f, 1, 3, ranbool(), ranbool())));
		yield return new WaitForSeconds(30f);
		spawner.waveCleared();
	}


	//hyp carrier  hyp reflector/zipper
	IEnumerator wave24() {
		spawner.spawnEnemyInMap("Behemoth", 0f, 9f, true, LevelSpawner.addToList.Specific, true);
		infiniteSpawnCoroutines.Add(StartCoroutine(genspawner("HyperReflector", 40f, 1, 5, ranbool(), ranbool())));
		infiniteSpawnCoroutines.Add(StartCoroutine(genspawner("HyperZipper", 18f, 1, 6, ranbool(), ranbool())));
		yield return new WaitForSeconds(30f);
		spawner.waveCleared();
	}


	//hyp core cont continuous ultimates
	IEnumerator wave25() {
		spawner.spawnEnemyInMap("HyperCore", 0f, 9f, true, LevelSpawner.addToList.Specific, true);
		infiniteSpawnCoroutines.Add(StartCoroutine(genspawner("UltimateBasic", 25f, 1, 3, ranbool(), ranbool())));
		infiniteSpawnCoroutines.Add(StartCoroutine(genspawner("UltimateArmored", 25f, 1, 3, ranbool(), ranbool())));
		infiniteSpawnCoroutines.Add(StartCoroutine(genspawner("UltimateShield", 25f, 1, 3, ranbool(), ranbool())));
		yield return new WaitForSeconds(30f);
		spawner.waveCleared();
	}

	//cont  hyp teleporter/vessel/ticker/enigma
	IEnumerator wave26() {
		infiniteSpawnCoroutines.Add(StartCoroutine(genspawner("HyperTeleporter", 15f, 1, 3, ranbool(), ranbool())));
		infiniteSpawnCoroutines.Add(StartCoroutine(genspawner("HyperVessel", 30f, 1, 3, ranbool(), ranbool())));
		infiniteSpawnCoroutines.Add(StartCoroutine(genspawner("HyperTicker", 25f, 1, 4, ranbool(), ranbool())));
		infiniteSpawnCoroutines.Add(StartCoroutine(genspawner("HyperEnigma", 25f, 1, 6, ranbool(), ranbool())));
		yield return new WaitForSeconds(15f);
		spawner.waveCleared();
	}


	//gigantodon
	IEnumerator wave27() {
		spawner.spawnEnemyInMap("Gigantodon", 0f, 9f, true, LevelSpawner.addToList.Specific, true);
		yield return new WaitForSeconds(120f);
		spawner.waveCleared();
	}

	//continuous buff spawn
	IEnumerator wave28() {
		infiniteSpawnCoroutines.Add(StartCoroutine(genspawner("Booster", 30f, 1, 3, ranbool(), ranbool())));
		infiniteSpawnCoroutines.Add(StartCoroutine(genspawner("Havoc", 30f, 1, 3, ranbool(), ranbool())));
		infiniteSpawnCoroutines.Add(StartCoroutine(genspawner("Maintainer", 30f, 1, 3, ranbool(), ranbool())));
		infiniteSpawnCoroutines.Add(StartCoroutine(genspawner("Protector", 30f, 1, 3, ranbool(), ranbool())));
		infiniteSpawnCoroutines.Add(StartCoroutine(genspawner("Armory", 30f, 1, 3, ranbool(), ranbool())));
		yield return new WaitForSeconds(15f);
		spawner.waveCleared();
	}

	//continuous debuff spawn
	IEnumerator wave29() {
		infiniteSpawnCoroutines.Add(StartCoroutine(genspawner("Disruptor", 30f, 1, 3, ranbool(), ranbool())));
		infiniteSpawnCoroutines.Add(StartCoroutine(genspawner("Jammer", 30f, 1, 3, ranbool(), ranbool())));
		yield return new WaitForSeconds(15f);
		spawner.waveCleared();
	}


	//maxima
	IEnumerator wave30() {
		spawner.spawnEnemyInMap("Maxima", 0f, 9f, true, LevelSpawner.addToList.Specific, true);
		yield return new WaitForSeconds(120f);
		spawner.waveCleared();
	}

	//continuous hyp buff
	IEnumerator wave31() {
		infiniteSpawnCoroutines.Add(StartCoroutine(genspawner("HyperBooster", 40f, 1, 2, ranbool(), ranbool())));
		infiniteSpawnCoroutines.Add(StartCoroutine(genspawner("HyperHavoc", 40f, 1, 2, ranbool(), ranbool())));
		infiniteSpawnCoroutines.Add(StartCoroutine(genspawner("HyperMaintainer", 40f, 1, 2, ranbool(), ranbool())));
		infiniteSpawnCoroutines.Add(StartCoroutine(genspawner("HyperProtector", 40f, 1, 2, ranbool(), ranbool())));
		infiniteSpawnCoroutines.Add(StartCoroutine(genspawner("HyperArmory", 40f, 1, 2, ranbool(), ranbool())));
		yield return new WaitForSeconds(15f);
		spawner.waveCleared();
	}


	//ernesto
	IEnumerator wave32() {
		spawner.spawnEnemyInMap("Ernesto", 0f, 9f, true, LevelSpawner.addToList.Specific, true);
		yield return new WaitForSeconds(60f);
		spawner.waveCleared();
	}

	//continuous hyp debuffspawn
	IEnumerator wave33() {
		infiniteSpawnCoroutines.Add(StartCoroutine(genspawner("HyperDisruptor", 40f, 1, 2, ranbool(), ranbool())));
		infiniteSpawnCoroutines.Add(StartCoroutine(genspawner("HyperJammer", 40f, 1, 2, ranbool(), ranbool())));
		yield return new WaitForSeconds(45f);
		spawner.waveCleared();
	}


	//hiddenboss - end before one can kill secret boss
	IEnumerator wave34() {
		stopOtherSpawns();
		spawner.spawnEnemyInMap("HiddenBoss", 0f, 9f, true, LevelSpawner.addToList.Specific, true);
		yield return new WaitForSeconds(80f);
		spawner.waveCleared();
	}
	void stopOtherSpawns() {
		int routines = infiniteSpawnCoroutines.Count;
		for (int i = 0; i < routines; i++) {
			Coroutine thisRoutine = infiniteSpawnCoroutines[0];
			infiniteSpawnCoroutines.RemoveAt(0);
			StopCoroutine(thisRoutine);
		}
	}
	//continuous hyp core/carrier spawn
	IEnumerator wave35() {
		StartCoroutine(genspawner("Core", 35f, 1, 2, ranbool(), ranbool()));
		StartCoroutine(genspawner("MesoCore", 40f, 1, 2, ranbool(), ranbool()));
		StartCoroutine(genspawner("MacroCore", 45f, 1, 1, ranbool(), ranbool()));
		StartCoroutine(genspawner("HyperCore", 50f, 1, 1, ranbool(), ranbool()));
		StartCoroutine(genspawner("Carrier", 30f, 1, 2, ranbool(), ranbool()));
		StartCoroutine(genspawner("Colossus", 40f, 1, 2, ranbool(), ranbool()));
		StartCoroutine(genspawner("Leviathan", 45f, 1, 1, ranbool(), ranbool()));
		StartCoroutine(genspawner("Behemoth", 50f, 1, 1, ranbool(), ranbool()));
		yield return new WaitForSeconds(60f);
		done = true;
		spawner.LastWaveEnemiesCleared();
	}

}
