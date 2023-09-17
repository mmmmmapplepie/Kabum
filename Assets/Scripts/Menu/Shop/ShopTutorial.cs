using UnityEngine;

public class ShopTutorial : MonoBehaviour {
	[SerializeField] GameObject tutorialPanel;
	[SerializeField]
	bool hasPlayerPrefs = false;
	[SerializeField]
	string playerPrefKey;  //ShopTutorial
	void Awake() {
		if (!hasPlayerPrefs) return;
		if (PlayerPrefs.HasKey(playerPrefKey)) {
			return;
		} else {
			PlayerPrefs.SetInt(playerPrefKey, 1);
			OpenTutorial();
		}
	}
	public void OpenTutorial() {
		tutorialPanel.SetActive(true);
	}
	public void CloseTutorial() {
		tutorialPanel.SetActive(false);
	}
}
