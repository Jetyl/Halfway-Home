using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class AlphaIgnoreRaycast : MonoBehaviour
{
  public float IgnoreAlphaBelow = 0.1f;

	void Start ()
  {
    GetComponent<Image>().alphaHitTestMinimumThreshold = IgnoreAlphaBelow;
	}
}
