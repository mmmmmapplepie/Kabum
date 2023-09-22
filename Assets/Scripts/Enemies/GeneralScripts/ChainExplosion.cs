using UnityEngine;
public class ChainExplosion : MonoBehaviour {
	[SerializeField]
	GameObject chainedAnimation;
	[HideInInspector]
	public bool Chained = false;
	[HideInInspector]
	public bool animationAdded = false;
	RectTransform rect;
	AudioManagerEnemy audioManager;
	void Awake() {
		audioManager = transform.Find("AudioManagerEnemy").GetComponent<AudioManagerEnemy>();
		rect = GetComponent<RectTransform>();
	}
	void Update() {
		if (Chained && !animationAdded && gameObject.GetComponent<IDamageable>().currentLife > 0f && !gameObject.GetComponent<IDamageable>().dead) {
			animationAdded = true;
			Transform anchorParent = transform.Find("State").Find("Life").Find("Background");
			GameObject chainedPE = Instantiate(chainedAnimation, anchorParent.position, Quaternion.identity, anchorParent);
			if (transform.Find("State").gameObject.GetComponent<Canvas>().enabled == false) {
				chainedPE.SetActive(false);
			}
		}
	}
	public void Explode() {
		audioManager.PlayAudio("ChainExplosion");
		Collider2D[] Objects = Physics2D.OverlapCircleAll(transform.position, 1.5f);
		foreach (Collider2D coll in Objects) {
			if ((coll.gameObject.tag == "Enemy" || coll.gameObject.tag == "TauntEnemy") && coll.transform.root.gameObject.GetComponent<IDamageable>() != null) {
				if (coll.gameObject == gameObject) {
					continue;
				}
				coll.transform.root.GetComponent<ChainExplosion>().Chained = true;
				if (coll.transform.root.GetComponent<IDamageable>().currentLife <= 0) {
					continue;
				}
				coll.transform.root.GetComponent<IDamageable>().ChainExplosion();
			}
		}
	}
}
