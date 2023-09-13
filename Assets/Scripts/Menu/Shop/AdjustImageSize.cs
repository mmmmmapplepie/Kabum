using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdjustImageSize : MonoBehaviour {
  [SerializeField]
  GameObject mainBody;
  [SerializeField]
  bool preview;

  //preview mode 20 units becomesx 600px so basically multiply by 30.
  [SerializeField]
  float factor = 30f;
  float time = 0f;
  float BowPreviewRotateDirection = 10f;
  void OnEnable() {
    gameObject.transform.rotation = Quaternion.identity;
    time = Time.time;
  }
  void Update() {
    Image img = mainBody.GetComponent<Image>();
    adjustImageCenter(img);
    // adjustImageWidth(img);
  }
  void FixedUpdate() {
    if (preview) {
      gameObject.transform.Rotate(new Vector3(0, 0, BowPreviewRotateDirection * Time.unscaledDeltaTime));
    }
  }
  void adjustImageWidth(Image img) {
    float width = img.sprite.bounds.extents.x * 2f;
    float height = img.sprite.bounds.extents.y * 2f;
    mainBody.GetComponent<RectTransform>().sizeDelta = new Vector2(width * factor, height * factor);
  }
  void adjustImageCenter(Image img) {
    float ypivot = img.sprite.bounds.center.y;
    mainBody.GetComponent<RectTransform>().anchoredPosition = new Vector2(mainBody.GetComponent<RectTransform>().position.x, ypivot * factor);
  }
}
