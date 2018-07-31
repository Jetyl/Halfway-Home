using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFitter : MonoBehaviour
{
  private SpriteRenderer SR;
  private string PrevSprite;
  // Use this for initialization
  void Start()
  {
    SR = (SpriteRenderer)GetComponent("Renderer");

    if (SR.sprite == null) PrevSprite = "None";
    else PrevSprite = SR.sprite.name;

    fitCameraHeight();
  }

  private void Update()
  {
    if (SR.sprite == null) return;

    if (SR.sprite.name != PrevSprite) fitCameraHeight();
  }

  void fitCameraHeight()
  {
    if (SR == null || SR.sprite == null || SR.sprite.texture == null)
      return;

    // Set filterMode
    SR.sprite.texture.filterMode = FilterMode.Point;

    // Get stuff
    double height = SR.sprite.bounds.size.y;
    double worldScreenHeight = Camera.main.orthographicSize * 2.0;

    // Resize
    transform.localScale = new Vector2(1, 1) * (float)(worldScreenHeight / height);

    PrevSprite = SR.sprite.name;
  }
}
