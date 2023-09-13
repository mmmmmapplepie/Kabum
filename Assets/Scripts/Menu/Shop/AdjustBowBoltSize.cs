using UnityEngine;
using UnityEngine.UI;

public class AdjustBowBoltSize : MonoBehaviour
{
	float initialSize;
	RectTransform rect;
	Image image;
	void Start()
	{
		rect = gameObject.GetComponent<RectTransform>();
		initialSize = rect.rect.width;
		image = gameObject.GetComponent<Image>();
	}
	void Update()
	{
		changeSize();
	}
	void changeSize()
	{
		if (image.sprite != null)
		{
			float maxsize = Mathf.Max(image.sprite.rect.width, image.sprite.rect.height);
			float scalingRatio = maxsize / 186f;
			rect.sizeDelta = new Vector2(scalingRatio * initialSize, scalingRatio * initialSize);
		}
	}
}
