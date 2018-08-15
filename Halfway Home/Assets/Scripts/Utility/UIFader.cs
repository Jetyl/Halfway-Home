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
    ResetChildren();
    if (StartFaded) Hide(0f);
	}

  private void OnEnable()
  {
    if (StartFaded) Hide(0f);
  }

  private void Update()
  {
    // This hotfix was causing Hide to be called per frame.
    //if(GetComponent<Graphic>() && GetComponent<Graphic>().color.a != 0 && IsHidden)
    //{
    //  Hide();
    //}
  }

  public void ResetChildren()
  {
    UIChildren.Clear();
    foreach (Graphic g in GetComponentsInChildren<Graphic>())
    {
      UIChildren.Add(new UIChild(g, g.color.a));
    }
  }

  public void Hide(float fadeTime = DefaultFadeTime)
  {
    //Debug.Log($"Hiding {gameObject.name}");
    ResetChildren();
    if(GetComponent<Graphic>() != null) GetComponent<Graphic>().CrossFadeAlpha(0f, fadeTime, true);
    foreach(UIChild c in UIChildren)
    {
      c.Element.CrossFadeAlpha(0f, fadeTime, true);
    }

    IsHidden = true;
  }

  public void Show(float fadeTime = DefaultFadeTime)
  {
    //Debug.Log($"Showing {gameObject.name}");
    ResetChildren();
    if (GetComponent<Graphic>() != null) GetComponent<Graphic>().CrossFadeAlpha(1f, fadeTime, true);
    foreach (UIChild c in UIChildren)
    {
      c.Element.CrossFadeAlpha(c.InitAlpha, fadeTime, true);
    }

    IsHidden = false;
  }
}
