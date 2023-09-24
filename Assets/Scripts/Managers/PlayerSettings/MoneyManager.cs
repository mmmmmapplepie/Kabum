using UnityEngine;
public class MoneyManager {

	public static int money = 2000;
	public static void useMoney(int val) {
		if (money > val) {
			money = (int)Mathf.Floor(money - val);
		}
		SaveSystem.saveSettings();
	}
	public static void addMoney(int val) {
		if (money + val < 10000000) {
			money = (int)Mathf.Floor(money + val);
		} else {
			money = 10000000;
		}
		SaveSystem.saveSettings();
	}
}
