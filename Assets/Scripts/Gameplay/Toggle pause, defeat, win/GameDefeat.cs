using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
public class GameDefeat : MonoBehaviour, DefeatScripts {
	public GameObject AdButton { get; set; }
	public GameObject selfObject { get; set; }
	[SerializeField] List<string> tipsList;
	[SerializeField] Text tipsText, loadPercent;
	[SerializeField] GameObject loadPanel;
	new AudioManagerUI audio;
	void Awake() {
		selfObject = gameObject;
		audio = GameObject.FindObjectOfType<AudioManagerUI>();
	}
	void OnEnable() {
		StartCoroutine(loseTune());
		Time.timeScale = 0f;
		BowManager.GunsReady = false;
	}
	IEnumerator loseTune() {
		yield return new WaitForSecondsRealtime(0.2f);
		audio.PlayAudio("Defeat");
	}
	public void Restart() {
		audio.PlayAudio("Click");
		Time.timeScale = 1f;
		bool levelBaseUsed = false;
		if (SceneManager.GetSceneByName("LevelBase").isLoaded) {
			levelBaseUsed = true;
		}
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
		if (levelBaseUsed) {
			SceneManager.LoadScene("LevelBase", LoadSceneMode.Additive);
		}
	}
	public void WorldMap() {
		Level data = GameObject.FindObjectOfType<LevelSpawner>().level;
		FocusLevelUpdater.currentLevel[0] = data.stageInWorld[0];
		FocusLevelUpdater.currentLevel[1] = data.stageInWorld[1];
		audio.PlayAudio("Click");
		StartCoroutine(loadSceneAsync("Worlds"));
	}
	public void ContinueAfterAd() {
		AdButton.GetComponent<Button>().gameObject.SetActive(false);
		LifeManager.CurrentLife = BowManager.MaxLife;
		Time.timeScale = 1f;
		selfObject.SetActive(false);
	}
	IEnumerator loadSceneAsync(string sceneName) {
		loadPanel.SetActive(true);
		int totalTips = tipsList.Count;
		int tipIndex = Random.Range(0, totalTips);
		tipsText.text = tipsList[tipIndex];
		AsyncOperation asyncScene = SceneManager.LoadSceneAsync(sceneName);
		asyncScene.allowSceneActivation = false;
		float loadedAmount = 0f;
		while (!asyncScene.isDone) {
			float percent = asyncScene.progress * 100f;
			if (loadedAmount < 100f && percent >= 90f) {
				loadPercent.text = loadedAmount.ToString() + "%";
				loadedAmount += 10f;
				yield return null;
			}
			if (loadedAmount >= 99f && percent >= 90f) {
				asyncScene.allowSceneActivation = true;
				loadPercent.text = "100%";
				yield return null;
			}
		}
	}

	void OnDisable() {
		BowManager.GunsReady = true;
	}
}
