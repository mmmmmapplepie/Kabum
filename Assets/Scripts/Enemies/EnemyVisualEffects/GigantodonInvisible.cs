using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GigantodonInvisible : MonoBehaviour, TurnInvisibleBase {
	[SerializeField] Animator animator;
	public void ChangeVisibility(bool invisible) {
		if (invisible) {
			animator.Play("GigantodonInvisible");
		} else {
			animator.Play("GigantodonSearchLights");
		}
	}
}
