using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class ReviveButtonWithAd : RewardedAdsButton {
	public DefeatScripts script;
	[SerializeField] GameObject scriptHoldingObject;
	void OnEnable() {
		script = scriptHoldingObject.GetComponent<DefeatScripts>();
	}

	override public void AdReward() {
		StartCoroutine(Resume());
	}
	IEnumerator Resume() {
		transform.GetChild(0).gameObject.SetActive(false);
		yield return new WaitForSecondsRealtime(0.5f);
		LifeManager.CurrentLife = BowManager.MaxLife;
		Time.timeScale = 1f;
		gameObject.SetActive(false);
		script.selfObject.SetActive(false);
	}

	override public void changeButtonTextAlpha(float a) {
		transform.GetChild(0).gameObject.GetComponent<Text>().color = new Color(1f, 1f, 1f, a);
		transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().color = new Color(1f, 1f, 1f, a);
	}
}
