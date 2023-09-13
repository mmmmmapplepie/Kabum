using UnityEngine;
using System.Collections;

public class adjustScroll : MonoBehaviour {
  RectTransform rect;
  void Awake() {
    rect = GetComponent<RectTransform>();
  }
  void OnEnable() {
    SetPosition();
  }
  void Start() {
    StartCoroutine(SetPositionRoutine());
  }
  void SetPosition() {
    float width = rect.rect.width;
    rect.localPosition = new Vector3(width, 0f, 0f);
  }
  IEnumerator SetPositionRoutine() {
    yield return null;
    SetPosition();
  }
}
