using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFitter : MonoBehaviour
{

	// Use this for initialization
	void Start ()
  {
    fitCameraHeight();
	}

  void fitCameraHeight()
  {
    SpriteRenderer sr = (SpriteRenderer)GetComponent("Renderer");
    if (sr == null)
      return;

    // Set filterMode
    sr.sprite.texture.filterMode = FilterMode.Point;

    // Get stuff
    double height = sr.sprite.bounds.size.y;
    double worldScreenHeight = Camera.main.orthographicSize * 2.0;

    // Resize
    transform.localScale = new Vector2(1, 1) * (float)(worldScreenHeight / height);
  }
}
