using UnityEngine;
using UnityEngine.UI;

public class DirectionCursor : MonoBehaviour {
  [SerializeField] GameObject Cursor;
  Image img;
  Color color;
  GameObject clickedObject;
  Vector2 clickedObjectPosition;
  float Distance;
  void Awake() {
    GameObject.Find("AudioManagerBGM").GetComponent<AudioManagerBGM>().ChangeBGM("MapTheme");
    Time.timeScale = 1f;
  }
  void Start() {
    img = Cursor.GetComponent<Image>();
    color = img.color;
    hidePointer();
  }
  void Update() {
    clickedObject = FocusLevelUpdater.cameraFocusObject;
    if (clickedObject != null) {
      clickedObjectPosition = clickedObject.GetComponent<RectTransform>().position;
      Distance = clickedObjectPosition.magnitude;
      if (Distance < 0.5f) return;
      pointerSpin();
      CheckOutOfView();
    } else {
      return;
    }
  }
  void CheckOutOfView() {
    if ((clickedObjectPosition.x < -6.1f || clickedObjectPosition.x > 6.1f) || (clickedObjectPosition.y < -10.5f || clickedObjectPosition.y > 10.5f)) {
      renderPointer();
    } else {
      hidePointer();
    }
    img.color = color;
  }
  void hidePointer() {
    color.a = 0f;
    img.color = color;
    Cursor.SetActive(false);
  }
  void pointerSpin() {
    if (clickedObjectPosition.x <= 0) {
      float angle = Mathf.Atan(clickedObjectPosition.y / clickedObjectPosition.x) * 180 / Mathf.PI;
      Cursor.transform.rotation = Quaternion.Euler(0, 0, angle);
    } else {
      if (clickedObjectPosition.y >= 0) {
        float angle = -180 + Mathf.Atan(clickedObjectPosition.y / clickedObjectPosition.x) * 180 / Mathf.PI;
        Cursor.transform.rotation = Quaternion.Euler(0, 0, angle);
      } else {
        float angle = 180 + Mathf.Atan(clickedObjectPosition.y / clickedObjectPosition.x) * 180 / Mathf.PI;
        Cursor.transform.rotation = Quaternion.Euler(0, 0, angle);
      }
    }
  }
  void renderPointer() {
    Cursor.SetActive(true);
    if (Distance > 20f) {
      color.a = 1f;
    } else {
      float value = 0.2f + (Distance - 6f) / 20f;
      color.a = value;
    }
  }
}
