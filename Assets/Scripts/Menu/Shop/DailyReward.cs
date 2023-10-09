using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Text.RegularExpressions;

public class DailyReward : MonoBehaviour {
	public GameObject RewardButton, RemainingTimer;
	[SerializeField] GameObject TimerAdButtonDisplay, NoNetwork;
	DateTime _LatestClaimDateTime;  //latest claim time in world clock "previous claim time"
	public DateTime LatestClaimDateTime {
		get { return _LatestClaimDateTime; }
		set {
			_LatestClaimDateTime = value;
			PlayerPrefs.SetString("LatestClaimTime", value.ToString());
		}
	}
	public DateTime startedTime; //start time on device time when "shop is opened"
	public DateTime startedWorldTime; //start time on world clock when "shop is opened"
	TimeSpan realWorldTimeSpan;
	[HideInInspector]
	public bool rewardAvailable = false;
	[HideInInspector]
	public bool timerNotChecked = true;
	void Awake() {
		startedTime = DateTime.Now;
		checkForLatestClaimTime();
	}
	void checkForLatestClaimTime() {
		if (PlayerPrefs.HasKey("LatestClaimTime")) {
			LatestClaimDateTime = DateTime.Parse(PlayerPrefs.GetString("LatestClaimTime"));
			StartCoroutine(getWorldClockAPITime());
		} else {
			rewardAvailable = true;
			StartCoroutine(getWorldClockAPITime());
		}
	}
	IEnumerator getWorldClockAPITime() {
		string WorldTimeAPIURL = "http://worldtimeapi.org/api/timezone/Asia/Seoul";
		UnityWebRequest dateTimeRequest = UnityWebRequest.Get(WorldTimeAPIURL);
		yield return dateTimeRequest.SendWebRequest();
		DateTime tempWorldTime;
		if (dateTimeRequest.result == UnityWebRequest.Result.ConnectionError || dateTimeRequest.result == UnityWebRequest.Result.ProtocolError) {
			tempWorldTime = new DateTime();
			NoNetwork.SetActive(true);
			yield break;
		}
		TimeData datetime = JsonUtility.FromJson<TimeData>(dateTimeRequest.downloadHandler.text);
		tempWorldTime = ParseDateTime(datetime.datetime);
		if (tempWorldTime != new DateTime()) {
			startedWorldTime = tempWorldTime;
			realWorldTimeSpan = startedWorldTime - LatestClaimDateTime;
			TimerAdButtonDisplay.SetActive(true);
			checkNewRewardAvailable();
			timerNotChecked = false;
		}
	}
	void checkNewRewardAvailable() {
		TimeSpan timeElapsedSinceClaim = (DateTime.Now - startedTime) + (startedWorldTime - LatestClaimDateTime);
		double totalHours = timeElapsedSinceClaim.TotalHours;
		if (totalHours > 24f || rewardAvailable) {
			rewardAvailable = true;
			RewardButton.SetActive(true);
			RemainingTimer.transform.parent.gameObject.SetActive(false);
			RewardButton.GetComponent<Button>().interactable = true;
		} else {
			updateTimer(timeElapsedSinceClaim);
			RewardButton.SetActive(false);
			RemainingTimer.transform.parent.gameObject.SetActive(true);
		}
	}
	void Update() {
		if (timerNotChecked) return;
		changeTimerTextColor();
		checkNewRewardAvailable();
	}
	void changeTimerTextColor() {
		if (!RewardButton.activeSelf) {
			Color original = RemainingTimer.GetComponent<Text>().color;
			RemainingTimer.GetComponent<Text>().color = new Color(original.r, original.g, original.b, 0.5f * Mathf.Abs(Mathf.Sin(Time.unscaledTime * Mathf.PI * 0.5f)) + 0.5f);
		}
	}
	void updateTimer(TimeSpan timeDiff) {
		Text textBox = RemainingTimer.GetComponent<Text>();
		double totalMinutes = 60 * 24 - timeDiff.TotalMinutes;
		float hours = Mathf.Floor((float)totalMinutes / 60f);
		float minutes = Mathf.Ceil(Mathf.Floor((float)totalMinutes + 1) % 60f);
		if (minutes == 0) hours += 1;
		textBox.text = $"{hours.ToString("00")}:{minutes.ToString("00")}";
	}

	DateTime ParseDateTime(string datetime) {
		string date = Regex.Match(datetime, @"^\d{4}-\d{2}-\d{2}").Value;
		string time = Regex.Match(datetime, @"\d{2}:\d{2}:\d{2}").Value;
		return DateTime.Parse(string.Format("{0} {1}", date, time));
	}
	class TimeData {
		public string datetime;
	}
}
