using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class StatBasedSpriteChanger : MonoBehaviour
{
  public Sprite Zero;
  public Sprite One;
  public Sprite Two;
  public Sprite Three;
  public Sprite Four;
  public Sprite Five;

  private Image currentImage;

  // Use this for initialization
  void Start ()
  {
    currentImage = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update ()
  {
		
	}

  public void SetState(int tier)
  {
    if (tier == 0 && Zero != null) currentImage.sprite = Zero;
    else if (tier == 0) currentImage.CrossFadeAlpha(0, 0, true);
    else if (tier == 1 && One != null) currentImage.sprite = One;
    else if (tier == 2 && Two != null) currentImage.sprite = Two;
    else if (tier == 3 && Three != null) currentImage.sprite = Three;
    else if (tier == 4 && Four != null) currentImage.sprite = Four;
    else if (tier == 5 && Five != null) currentImage.sprite = Five;
  }
}
