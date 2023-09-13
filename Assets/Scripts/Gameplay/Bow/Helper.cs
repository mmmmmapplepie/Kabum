using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Helper : MonoBehaviour {
  [SerializeField]
  GameObject LeftString;
  [SerializeField]
  GameObject RightString;
  [SerializeField]
  GameObject HelperBulletPrefab;
  GameObject LockedOnEnemy;
  IDamageable lifeScript;
  bool aiming = false;

  void Update() {
    //get a lock on
    if (LockedOnEnemy == null) {
      enemyDeadNewEnemyFind();
    }
    if (lifeScript != null) {
      if (lifeScript.dead) {
        enemyDeadNewEnemyFind();
      }
    }
    if (LockedOnEnemy != null && findChildBullet() != null && aiming == false) {
      aiming = true;
      StartCoroutine("AimAndShoot");
    }
  }
  void enemyDeadNewEnemyFind() {
    EnemyDed();
    FindNewEnemy(true);
    if (LockedOnEnemy == null) {
      FindNewEnemy(false);
    }
  }
  void FindNewEnemy(bool TauntEnemy) {
    if (TauntEnemy) {
      List<GameObject> enemies = new List<GameObject>(GameObject.
      FindGameObjectsWithTag("TauntEnemy"));
      if (enemies.Count == 0) {
        return;
      }
      LockedOnEnemy = enemies[Random.Range(0, enemies.Count)];

    } else {
      List<GameObject> enemies = new List<GameObject>(GameObject.
      FindGameObjectsWithTag("Enemy"));
      if (enemies.Count == 0) {
        return;
      }
      LockedOnEnemy = enemies[Random.Range(0, enemies.Count)];
    }
    if (LockedOnEnemy != null) {
      lifeScript = LockedOnEnemy.transform.root.GetComponent<IDamageable>();
    }
  }
  void Move(float stage, Vector3 Direction) {
    RotateBase(Direction);
    RotateLeft(stage);
    RotateRight(stage);
    MoveBullet(stage);
  }
  void RotateBase(Vector3 direction) {
    float angle = 0f;
    if (direction[1] >= 0) {
      if (direction[1] == 0) {
        if (direction[0] < 0) {
          angle = 90f;
        } else {
          angle = -90f;
        }
      } else {
        angle = Mathf.Atan(direction[0] / direction[1]) * 180 / Mathf.PI;
      }
      transform.rotation = Quaternion.Euler(0, 0, -angle);
    } else {
      if (direction[0] >= 0) {
        angle = direction[0] == 0 ? 180 : 90 - Mathf.Atan(direction[1] / direction[0]) * 180 / Mathf.PI;
        transform.rotation = Quaternion.Euler(0, 0, -angle);
      } else {
        angle = direction[0] == 0 ? 180 : 90 + Mathf.Atan(direction[1] / direction[0]) * 180 / Mathf.PI;
        transform.rotation = Quaternion.Euler(0, 0, angle);
      }
    }
  }
  Transform findChildBullet() {
    Transform childBullet = null;
    foreach (Transform tra in transform) {
      if (tra.CompareTag("Bullet")) {
        childBullet = tra;
        break;
      }
    }
    return childBullet;
  }
  void RotateLeft(float ratio) {
    if (ratio < 1) {
      float angle = Mathf.Atan(ratio) * 180 / Mathf.PI;
      float length = Mathf.Sqrt(Mathf.Pow(ratio, 2f) + 1);
      Vector3 scale = new Vector3(length, 1f, 1f);
      LeftString.transform.localRotation = Quaternion.Euler(0, 0, -angle);
      LeftString.transform.localScale = scale;
    }
  }
  void RotateRight(float ratio) {
    if (ratio < 1) {
      float angle = Mathf.Atan(ratio) * 180 / Mathf.PI;
      float length = Mathf.Sqrt(Mathf.Pow(ratio, 2f) + 1);
      Vector3 scale = new Vector3(length, 1f, 1f);
      RightString.transform.localRotation = Quaternion.Euler(0, 0, angle);
      RightString.transform.localScale = scale;
    }
  }
  void MoveBullet(float ratio) {
    Transform bullet = findChildBullet();
    if (bullet != null) {
      if (ratio < 1) {
        bullet.localPosition = new Vector3(0f, 3.234f - ratio * 5, 0f);
      } else {
        bullet.localPosition = new Vector3(0f, 3.234f - 5f, 0f);
      }
    }
  }
  void EnemyDed() {
    StopCoroutine("AimAndShoot");
    SnapBack();
    aiming = false;
  }
  IEnumerator AimAndShoot() {
    float AimTime = BowManager.ReloadRate * BowManager.CoolDownRate;
    float bulletLoad = Random.Range(2f, 3f) * BowManager.AmmoRate * BowManager.CoolDownRate;
    float startTime = Time.time;
    SnapBack();
    while (Time.time - startTime <= AimTime) {
      if (LockedOnEnemy != null) {
        Vector3 EnemyPosition = LockedOnEnemy.transform.position;
        Vector3 BowPosition = transform.position;
        Vector3 Direction = EnemyPosition - BowPosition;
        float ratio = ((Time.time - startTime) / AimTime);
        Move(ratio, Direction);
        yield return null;
      } else {
        EnemyDed();
        yield break;
      }
    }
    if (Time.time - startTime >= AimTime) {
      float angularDir = transform.eulerAngles.z;
      Transform bullet = findChildBullet();
      bullet.GetComponent<HelperBullet>().Shoot(angularDir);
      bullet.parent = null;
      SnapBack();
      StartCoroutine("Reload", bulletLoad);
    }
  }
  IEnumerator Reload(float time) {
    yield return new WaitForSeconds(time);
    GameObject bullet = Instantiate(HelperBulletPrefab, new Vector3(0f, -10f, 0f), transform.rotation, transform);
    bullet.transform.localPosition = new Vector3(0f, 3.234f, 0f);
    aiming = false;
  }
  void SnapBack() {
    Transform bullet = findChildBullet();
    if (bullet != null) {
      bullet.localPosition = new Vector3(0f, 3.234f, 0f);
    }
    transform.rotation = Quaternion.Euler(0, 0, 0);
    RightString.transform.localRotation = Quaternion.Euler(0, 0, 0);
    RightString.transform.localScale = new Vector3(1f, 1f, 1f);
    LeftString.transform.localRotation = Quaternion.Euler(0, 0, 0);
    LeftString.transform.localScale = new Vector3(1f, 1f, 1f);
  }
}
