using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmoryBuff : MonoBehaviour {
  [SerializeField] int armorBuff;
  List<Collider2D> enteredEnemies = new List<Collider2D>();
  void OnTriggerEnter2D(Collider2D coll) {
    if (coll.tag == "Enemy" || coll.tag == "TauntEnemy") {
    } else {
      return;
    }
    enteredEnemies.Add(coll);
    if (coll.transform.root.GetComponent<IDamageable>() != null) {
      BuffArmor(coll.transform.root.gameObject.GetComponent<IDamageable>());
    }
  }
  void BuffArmor(IDamageable script) {
    script.Armor += armorBuff;
  }
  void OnTriggerExit2D(Collider2D coll) {
    if (coll.tag != "Enemy" && coll.tag != "TauntEnemy") {
      return;
    }
    enteredEnemies.Remove(coll);
    if (coll.transform.root.GetComponent<IDamageable>() != null) {
      RemoveArmor(coll.transform.root.gameObject.GetComponent<IDamageable>());
    }
  }
  void RemoveArmor(IDamageable script) {
    script.Armor -= armorBuff;
  }
  void OnDestroy() {
    foreach (Collider2D coll in enteredEnemies) {
      if (coll != null)
        coll.transform.root.gameObject.GetComponent<IDamageable>().Armor -= armorBuff;
    }
  }
}
