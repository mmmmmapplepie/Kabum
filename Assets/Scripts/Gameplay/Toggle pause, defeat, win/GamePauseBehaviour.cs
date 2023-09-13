using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GamePauseBehaviour : MonoBehaviour {
	[SerializeField]
	GameObject PauseToggle;
	[SerializeField] Text tipsText, loadPercent;
	[SerializeField] GameObject loadPanel;
	[SerializeField] List<string> tipsList;

	[SerializeField] bool Endless = false;
	Button button;
	public static bool gamePaused = false;
	public static bool Pausable = true;
	bool pausableCheck = true;
	new AudioManagerUI audio;
	void Awake() {
		audio = GameObject.FindObjectOfType<AudioManagerUI>();
		Pausable = true;
		gamePaused = false;
	}
	void Start() {
		button = gameObject.GetComponent<Button>();
		button.onClick.AddListener(Pause);
	}
	void Update() {
		if (pausableCheck != Pausable) {
			pausableCheck = Pausable;
			if (pausableCheck) {
				GetComponent<Button>().interactable = true;
			} else {
				GetComponent<Button>().interactable = false;
			}
		}
	}

	// Update is called once per frame
	void Pause() {
		audio.PlayAudio("Click");
		if (BowManager.UsingCooldown == true || Pausable == false) {
			return;
		}
		Time.timeScale = 0f;
		gamePaused = true;
		PauseToggle.SetActive(true);
	}
	public void Restart() {
		audio.PlayAudio("Click");
		gamePaused = false;
		Time.timeScale = 1f;
		PauseToggle.SetActive(false);
		bool levelBaseUsed = false;
		if (SceneManager.GetSceneByName("LevelBase").isLoaded) {
			levelBaseUsed = true;
		}
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
		if (levelBaseUsed) {
			SceneManager.LoadScene("LevelBase", LoadSceneMode.Additive);
		}
	}
	public void Continue() {
		audio.PlayAudio("Click");
		gamePaused = false;
		Time.timeScale = 1f;
		PauseToggle.SetActive(false);
	}
	public void WorldMap() {
		if (Endless) {
			SceneManager.LoadScene("GameMode");
			return;
		}
		Level data = GameObject.FindObjectOfType<LevelSpawner>().level;
		FocusLevelUpdater.currentLevel[0] = data.stageInWorld[0];
		FocusLevelUpdater.currentLevel[1] = data.stageInWorld[1];
		audio.PlayAudio("Click");
		gamePaused = false;
		StartCoroutine(loadSceneAsync("Worlds"));
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
	void OnDestroy() {
		Time.timeScale = 1f;
	}
}
