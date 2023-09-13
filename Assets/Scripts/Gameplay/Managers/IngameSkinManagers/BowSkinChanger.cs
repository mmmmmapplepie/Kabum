using System.Collections.Generic;
using UnityEngine;

public class BowSkinChanger : MonoBehaviour {
  [SerializeField]
  List<Skin> listOfBowSkins = new List<Skin>();
  [SerializeField]
  Transform Bow1, Bow2;
  [SerializeField]
  List<Transform> helpers = new List<Transform>();
  void Awake() {
    Bow1 = Bow1.Find("BaseofBow");
    Bow2 = Bow2.Find("BaseofBow");
    for (int i = 0; i < 6; i++) {
      helpers[i] = helpers[i].Find("BaseofBow");
    }
    changeBowSkins();
  }
  Skin FindBowSkin() {
    return listOfBowSkins.Find(x => x.name == SettingsManager.currBowSkin);
  }
  void changeBowSkins() {
    Skin skin = FindBowSkin();
    changeMainBowSkin(skin);
    changeHelperSkin(skin);
  }
  void changeMainBowSkin(Skin skin) {
    changeMainBowBody(skin);
    changeStrings(Bow1, skin);
    changeBolts(Bow1, skin);
    changeStrings(Bow2, skin);
    changeBolts(Bow2, skin);
  }
  void changeMainBowBody(Skin skin) {

    Bow1.GetComponent<SpriteRenderer>().sprite = skin.mainBody;
    Bow2.GetComponent<SpriteRenderer>().sprite = skin.mainBody;
    if (skin.particleEffect != null) {
      addEffect(Bow1, skin);
      addEffect(Bow2, skin);
    }
  }
  void changeHelperSkin(Skin skin) {
    foreach (Transform helper in helpers) {
      changeHelperBody(helper, skin);
      changeStrings(helper, skin);
      changeBolts(helper, skin);
    }
  }
  void changeHelperBody(Transform tra, Skin skin) {
    tra.GetComponent<SpriteRenderer>().sprite = skin.mainBody;
  }
  void changeStrings(Transform tra, Skin skin) {
    checkSpriteNullAndSet(tra.Find("LeftString").GetComponent<SpriteRenderer>(), skin.LeftString);
    checkSpriteNullAndSet(tra.Find("RightString").GetComponent<SpriteRenderer>(), skin.RightString);
  }
  void changeBolts(Transform tra, Skin skin) {
    checkSpriteNullAndSet(tra.Find("StringHolderL").GetComponent<SpriteRenderer>(), skin.LeftBolt);
    checkSpriteNullAndSet(tra.Find("StringHolderR").GetComponent<SpriteRenderer>(), skin.RightBolt);
  }
  void addEffect(Transform parent, Skin skin) {
    GameObject effect = skin.particleEffect;
    GameObject PS = Instantiate(effect, parent, true);
    PS.transform.localPosition = new Vector3(0f, 0f, 0f);
  }
  void checkSpriteNullAndSet(SpriteRenderer toSet, Sprite skin) {
    if (skin == null) {
      toSet.sprite = null;
      return;
    }
    toSet.sprite = skin;
  }
}
