using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFader : MonoBehaviour
{
  private class UIChild
  {
    public Graphic Element;
    public float InitAlpha;

    public UIChild(Graphic element, float initAlpha)
    {
      Element = element;
      InitAlpha = initAlpha;
    }
  };

  public bool StartFaded = true;
  public const float DefaultFadeTime = 0f;
  private bool IsHidden;

  List<UIChild> UIChildren = new List<UIChild>();

	void Start ()
  {
    foreach (Graphic g in GetComponentsInChildren<Graphic>())
    {
      UIChildren.Add(new UIChild(g, g.color.a));
    }
    if (StartFaded) Hide(0f);
	}

  private void Update()
  {
    if(GetComponent<Graphic>() && GetComponent<Graphic>().color.a != 0 && IsHidden)
    {
      Hide();
    }
  }

  public void Hide(float fadeTime = DefaultFadeTime)
  {
    if(GetComponent<Graphic>() != null) GetComponent<Graphic>().CrossFadeAlpha(0f, fadeTime, true);
    foreach(UIChild c in UIChildren)
    {
      c.Element.CrossFadeAlpha(0f, fadeTime, true);
    }

    IsHidden = true;
  }

  public void Show(float fadeTime = DefaultFadeTime)
  {
    if (GetComponent<Graphic>() != null) GetComponent<Graphic>().CrossFadeAlpha(1f, fadeTime, true);
    foreach (UIChild c in UIChildren)
    {
      c.Element.CrossFadeAlpha(c.InitAlpha, fadeTime, true);
    }

    IsHidden = false;
  }
}
