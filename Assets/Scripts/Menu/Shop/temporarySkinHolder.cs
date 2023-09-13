using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class temporarySkinHolder : MonoBehaviour {
  public GameObject clickedButton;
  [HideInInspector]
  public Skin tempBow, tempBullet, tempFortress;
  [SerializeField]
  public List<Skin> allBowSkins;
  [SerializeField]
  public List<Skin> allBulletSkins;
  [SerializeField]
  public List<Skin> allFortressSkins;
  [SerializeField]
  GameObject bowpanel, bulletpanel, fortresspanel, bulletPreviewToggle;
  [SerializeField]
  GameObject bowPreview, fortressPreview, fortressMask, bowMask;

  new AudioManagerUI audio;
  void Awake() {
    audio = GameObject.FindObjectOfType<AudioManagerUI>();
    changeTempBow();
    changeTempBullet();
    changeTempFortress();
  }
  public void changeTempBow() {
    tempBow = FindSkin(allBowSkins, Skin.skinType.Bow);
  }
  public void changeTempBullet() {
    tempBullet = FindSkin(allBulletSkins, Skin.skinType.Bullet);
  }
  public void changeTempFortress() {
    tempFortress = FindSkin(allFortressSkins, Skin.skinType.Fortress);
  }
  Skin FindSkin(List<Skin> searchList, Skin.skinType type) {
    Skin retskin;
    if (type == Skin.skinType.Bow) {
      retskin = searchList.Find(x => x.name == SettingsManager.currBowSkin);
    } else if (type == Skin.skinType.Bullet) {
      retskin = searchList.Find(x => x.name == SettingsManager.currBulletSkin);
    } else {
      retskin = searchList.Find(x => x.name == SettingsManager.currFortressSkin);
    }
    return retskin;
  }
  public void changeClickedButton(GameObject newBtn) {
    audio.PlayAudio("UpLevel");
    Color original = newBtn.GetComponent<Image>().color;
    Color newColor = new Color(0.9f, 0.2f, 0.1f, 1f);
    clickedButton.GetComponent<Image>().color = original;
    newBtn.GetComponent<Image>().color = newColor;
    clickedButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(clickedButton.GetComponent<RectTransform>().anchoredPosition.x, 0f);
    newBtn.GetComponent<RectTransform>().anchoredPosition = new Vector2(newBtn.GetComponent<RectTransform>().anchoredPosition.x, 30f);
    clickedButton = newBtn;
    changeSkinPanel();
  }
  void changeSkinPanel() {
    bowpanel.SetActive(false);
    bulletpanel.SetActive(false);
    fortresspanel.SetActive(false);
    if (clickedButton.name == "BowBtn") {
      bowpanel.SetActive(true);
      bowPreview.SetActive(true);
      fortressPreview.SetActive(false);
      bowMask.SetActive(true);
      fortressMask.SetActive(false);
      bulletPreviewToggle.SetActive(true);
    }
    if (clickedButton.name == "BulletBtn") {
      bulletpanel.SetActive(true);
      bowPreview.SetActive(true);
      fortressPreview.SetActive(false);
      bowMask.SetActive(true);
      fortressMask.SetActive(false);
      bulletPreviewToggle.SetActive(true);
    }
    if (clickedButton.name == "FortressBtn") {
      fortresspanel.SetActive(true);
      bowPreview.SetActive(false);
      fortressPreview.SetActive(true);
      bowMask.SetActive(false);
      fortressMask.SetActive(true);
      bulletPreviewToggle.SetActive(false);
    }
  }
}
