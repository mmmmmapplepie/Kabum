using System;
using UnityEngine;

public class MoneyButtonWithAd : RewardedAdsButton {
	[SerializeField] DailyReward script;
	override public void AdReward() {
		script.LatestClaimDateTime = script.startedWorldTime + (DateTime.Now - script.startedTime);
		script.rewardAvailable = false;
		MoneyManager.addMoney(5000);
		script.RewardButton.SetActive(false);
		script.RemainingTimer.transform.parent.gameObject.SetActive(true);
	}
}
