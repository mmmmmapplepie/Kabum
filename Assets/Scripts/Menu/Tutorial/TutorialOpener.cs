using System.Collections;
using UnityEngine;

public class TutorialOpener : MonoBehaviour {
	[SerializeField]
	bool ProgressTrigger;
	[SerializeField]
	GameObject guidePanel;
	// Start is called before the first frame update
	[SerializeField]
	bool hasPlayerPrefs = false;
	[SerializeField]
	string playerPrefKey;  //UpgradesTutorial
	void Awake() {
		if (!hasPlayerPrefs) return;
		if (PlayerPrefs.HasKey(playerPrefKey)) {
			return;
		} else {
			PlayerPrefs.SetInt(playerPrefKey, 1);
			ActivateGuide();
		}
	}
	void Start() {
		if (ProgressTrigger == true) {
			StartCoroutine(beginningGuide());
		}
	}
	IEnumerator beginningGuide() {
		yield return new WaitForSeconds(1.5f);
		if (SettingsManager.world[0] == 1 && SettingsManager.world[1] == 1) {
			ActivateGuide();
		}
	}
	public void ActivateGuide() {
		guidePanel.SetActive(true);
	}
	public void DeactivateGuide() {
		guidePanel.SetActive(false);
	}
}
