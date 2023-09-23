using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerInvisible : MonoBehaviour, TurnInvisibleBase {
	[SerializeField] List<GameObject> flickerObjects;
	public void ChangeVisibility(bool invisible) {
		if (invisible) {
			foreach (GameObject obj in flickerObjects) {
				obj.GetComponent<GlowImageFlicker>().stopRoutines();
			}
		} else {
			foreach (GameObject obj in flickerObjects) {
				obj.GetComponent<GlowImageFlicker>().startRoutines();
			}
		}
	}
}
