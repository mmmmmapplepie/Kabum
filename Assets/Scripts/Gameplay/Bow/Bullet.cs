using UnityEngine;
using System.Collections.Generic;

public class Bullet : MonoBehaviour, IBullet {
	[SerializeField]
	List<GameObject> Effects;
	[SerializeField]
	GameObject PullEffect;
	AudioManagerCannon audioManager;

	int hits;
	float damage;
	bool aoe;
	bool chain;
	float pull = 0;
	float pulltime = 0.4f;
	bool pullStarted = false;
	public int pierce { get; set; }
	float speed;
	bool used = false;


	float selfDestroyTimerStartTime = -1f;


	void Awake() {
		audioManager = GameObject.Find("AudioManagerCannon").GetComponent<AudioManagerCannon>();
		gameObject.GetComponent<CircleCollider2D>().enabled = false;
	}
	void Start() {
		if (GameObject.FindObjectOfType<BulletSkinChanger>() != null) {
			GameObject.FindObjectOfType<BulletSkinChanger>().changeBulletSprite(gameObject);
		}
	}
	void Update() {
		if (selfDestroyTimerStartTime != -1f && Time.time - selfDestroyTimerStartTime > 15f) Destroy(gameObject);
		if (transform.position.x > 7f || transform.position.x < -7f || transform.position.y > 13f || transform.position.y < -13f) {
			Destroy(gameObject);
		}
		if (pull > 0f && pullStarted == false && gameObject.GetComponent<CircleCollider2D>().enabled == true) {
			pullStarted = true;
			pulltime = (0.4f - 0.01f * pull) / speed;
			InvokeRepeating("PullEnemies", 0f, pulltime);
		}
	}
	void PullEnemies() {
		Collider2D[] Enemies = Physics2D.OverlapCircleAll(transform.position, 2.5f);
		CreateEffect(PullEffect, null, transform.position);
		audioManager.PlayAudio("PullSound");
		foreach (Collider2D coll in Enemies) {
			if (coll.gameObject.tag == "Enemy" || coll.gameObject.tag == "TauntEnemy") {
				if (coll.gameObject == gameObject) {
					continue;
				}
				float force = 0f;
				if ((coll.transform.root.position.x - transform.position.x) != 0f) {
					float diff = coll.transform.root.position.x - transform.position.x;
					float forcemag = 1f / Mathf.Pow((4.5f - Mathf.Abs(diff)), 1.5f);
					if (diff > 0f) {
						force = -forcemag;
					} else {
						force = forcemag;
					}
				}

				Rigidbody2D rb = coll.transform.root.GetComponent<Rigidbody2D>();
				rb.AddForce(new Vector2(Mathf.Log(GetComponent<Rigidbody2D>().velocity.magnitude + 1) * force * pull, 0f), ForceMode2D.Impulse);
			}
		}
	}
	public void Shoot(float angle, float ratio) {
		float x = 0;
		float y = 1;
		if (angle >= 0f && angle < 90f) {
			float input = angle * Mathf.PI / 180;
			x = -Mathf.Sin(input);
			y = Mathf.Cos(input);
		} else if (angle >= 90f && angle < 180f) {
			float a = angle - 90f;
			float input = a * Mathf.PI / 180;
			x = -Mathf.Cos(input);
			y = -Mathf.Sin(input);
		} else if (angle >= 180f && angle < 270f) {
			float a = angle - 180f;
			float input = a * Mathf.PI / 180;
			x = Mathf.Sin(input);
			y = -Mathf.Cos(input);
		} else if (angle >= 270f && angle < 360f) {
			float a = angle - 270f;
			float input = a * Mathf.PI / 180;
			x = Mathf.Cos(input);
			y = Mathf.Sin(input);
		}
		Vector3 direction = new Vector3(x, y, 0f);
		SetBulletSettings();
		shootSound(speed * direction.magnitude * ratio);
		GetComponent<Rigidbody2D>().velocity = speed * direction * ratio;
		selfDestroyTimerStartTime = Time.time;
	}
	void SetBulletSettings() {
		hits = BowManager.HitsPerHit;
		damage = BowManager.BulletDmg * BowManager.BulletMultiplier;
		aoe = BowManager.AOE;
		chain = BowManager.ChainExplosion;
		pull = BowManager.PullForce;
		pierce = BowManager.Pierce;
		speed = BowManager.BulletSpeed * BowManager.BulletMultiplier;
		gameObject.GetComponent<CircleCollider2D>().enabled = true;
	}
	void CreateEffect(GameObject prefab, Transform parent, Vector3 pos) {
		GameObject effect = Instantiate(prefab, pos, Quaternion.identity, parent);
	}
	void OnTriggerEnter2D(Collider2D coll) {
		if (used == true) {
			return;
		}
		if (coll.gameObject.tag == "TauntEnemy" || coll.gameObject.tag == "Enemy") {
			if (chain && coll.transform.root.GetComponent<ChainExplosion>().Chained == false) {
				coll.transform.root.GetComponent<ChainExplosion>().Chained = true;
			}
			IDamageable life = coll.transform.root.gameObject.GetComponent<IDamageable>();
			Transform enemyCenter = coll.transform.root;
			life.takeDamage(damage);
			CreateEffect(Effects.Find(x => x.name == "NormalHitEffect"), enemyCenter, enemyCenter.position);
			for (int i = 0; i < hits; i++) {
				life.takeDamage(damage);
				if (life.currentLife >= 0f) {
					CreateEffect(Effects.Find(x => x.name == "NormalHitEffect"), null, enemyCenter.position);
				} else {
					break;
				}
			}
			if (aoe) {
				life.AoeHit(damage);
				CreateEffect(Effects.Find(x => x.name == "AoeHit"), null, enemyCenter.position);
			}
			pierce--;
			if (pierce <= 0) {
				gameObject.GetComponent<Collider2D>().enabled = false;
				used = true;
				Destroy(gameObject);
			}
		}
	}
	void shootSound(float speed) {
		if (speed < 10f) {
			audioManager.PlayAudio("SlowShot");
		} else if (speed < 30f) {
			audioManager.PlayAudio("MidShot");
		} else {
			audioManager.PlayAudio("FastShot");
		}
	}
}
