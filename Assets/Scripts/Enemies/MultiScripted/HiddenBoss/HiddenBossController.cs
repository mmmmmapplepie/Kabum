using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HiddenBossController : MonoBehaviour {
	[SerializeField] HiddenBossLife lifeScript;

	[SerializeField] HiddenBoss_Buff BuffSkill;
	[SerializeField] HiddenBoss_Debuff DebuffSkill;
	[SerializeField] HiddenBoss_Invisible InvisibleSkill;
	[SerializeField] HiddenBoss_Summon SummonSkill;
	[SerializeField] HiddenBoss_Vampire VampireSkill;
	[SerializeField] HiddenBoss_DOT DOTSkill;
	[SerializeField] HiddenBoss_Teleport TeleportSkill;
	[SerializeField] HiddenBoss_BlockPierce BlockPierceSkill;
	[SerializeField] HiddenBoss_Pull PullSkill;
	[SerializeField] GameObject buffShade, debuffShade, invisibleShade, summonShade, vampireShade, dotShade, teleportShade, pullShade, blockPierceShade;

	[SerializeField] Slider timer;
	[SerializeField] float abilityRefreshPeriod = 30f;

	List<string> Skills = new List<string> { "Invisible", "Debuff", "Buff", "Summon", "Vampire", "DOT", "Teleport", "Pull", "BlockPierce" };

	float cycleStartTime = 0f;
	int[] SkillsPerState = { 4, 6, 8 };

	void Start() {
		StartCoroutine(SkillCycleRoutine());
	}
	IEnumerator SkillCycleRoutine() {
		cycleStartTime = Time.time;
		disableAllSkills();
		chooseRandomAbilities();
		timer.gameObject.SetActive(true);
		while (true) {
			if (Time.time - cycleStartTime < abilityRefreshPeriod) {
				timer.value = (abilityRefreshPeriod + cycleStartTime - Time.time) / abilityRefreshPeriod;
			} else {
				cycleStartTime = Time.time;
				timer.value = 1f;
				disableAllSkills();
				timer.gameObject.SetActive(true);
				chooseRandomAbilities();
			}
			yield return null;
		}
	}
	void disableAllSkills() {
		BuffSkill.enabled = false;
		DebuffSkill.enabled = false;
		InvisibleSkill.enabled = false;
		SummonSkill.enabled = false;
		VampireSkill.enabled = false;
		DOTSkill.enabled = false;
		TeleportSkill.enabled = false;
		PullSkill.enabled = false;
		BlockPierceSkill.enabled = false;


		//enable all shades;
		buffShade.SetActive(true);
		debuffShade.SetActive(true);
		invisibleShade.SetActive(true);
		summonShade.SetActive(true);
		vampireShade.SetActive(true);
		dotShade.SetActive(true);
		teleportShade.SetActive(true);
		pullShade.SetActive(true);
		blockPierceShade.SetActive(true);

		timer.gameObject.SetActive(false);
	}
	List<string> newList() {
		List<string> skillsCopy = new List<string>();
		foreach (string str in Skills) {
			skillsCopy.Add(str);
		}
		return skillsCopy;
	}
	void chooseRandomAbilities() {
		List<string> tempSkillsCopy = newList();
		int count = 0;
		while (count < SkillsPerState[lifeScript.currentStage]) {
			string skillToAdd = tempSkillsCopy[Random.Range(0, tempSkillsCopy.Count)];
			tempSkillsCopy.Remove(skillToAdd);
			StartCoroutine(skillToAdd);
			count++;
		}
	}
	public void StopSkills() {
		StopAllCoroutines();
		disableAllSkills();
	}
	public void StartSkills() {
		StartCoroutine(SkillCycleRoutine());
	}



	#region skill&respectivecallingCoroutines
	IEnumerator Buff() {
		BuffSkill.enabled = true;
		buffShade.SetActive(false);
		yield return null;
	}
	IEnumerator Debuff() {
		DebuffSkill.enabled = true;
		debuffShade.SetActive(false);
		yield return null;
	}
	IEnumerator Invisible() {
		InvisibleSkill.enabled = true;
		invisibleShade.SetActive(false);
		yield return null;
	}
	IEnumerator Vampire() {
		VampireSkill.enabled = true;
		vampireShade.SetActive(false);
		yield return null;
	}
	IEnumerator Summon() {
		SummonSkill.enabled = true;
		summonShade.SetActive(false);
		yield return null;
	}
	IEnumerator DOT() {
		DOTSkill.enabled = true;
		dotShade.SetActive(false);
		yield return null;
	}
	IEnumerator Teleport() {
		TeleportSkill.enabled = true;
		teleportShade.SetActive(false);
		yield return null;
	}
	IEnumerator Pull() {
		PullSkill.enabled = true;
		pullShade.SetActive(false);
		yield return null;
	}
	IEnumerator BlockPierce() {
		BlockPierceSkill.enabled = true;
		blockPierceShade.SetActive(false);
		yield return null;
	}
	#endregion
}
