using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class SpriteSwapper : MonoBehaviour
{
  public Sprite SwapSprite;
  private Sprite StartSprite;
  private bool Swapped = false;
  private void Start()
  {
    StartSprite = GetComponent<Image>().sprite;
  }
  public void Swap()
  {
    if(Swapped)
    {
      ResetToOld();
    }
    else
    {
      SwapToNew();
    }
  }

  void SwapToNew()
  {
    GetComponent<Image>().sprite = SwapSprite;
    Swapped = true;
  }

  void ResetToOld()
  {
    GetComponent<Image>().sprite = StartSprite;
    Swapped = false;
  }
}
