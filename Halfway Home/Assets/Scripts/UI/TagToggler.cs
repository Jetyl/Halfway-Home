using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TagToggler : MonoBehaviour
{
  public class TutorialDisplayChange : Stratus.Event { public string Tag; public bool Hide; public TutorialDisplayChange(string tag, bool hide) { Tag = tag; Hide = hide; } }
  public string Tag;
  public bool HideOwnChildren;
  private List<Graphic> ActiveGraphics = new List<Graphic>();
  private bool HasGraphic;

	// Use this for initialization
	void Start ()
  {
    Stratus.Scene.Connect<TutorialDisplayChange>(OnTutorialDisplayChange);
    HasGraphic = GetComponent<Graphic>() != null;
	}
	
	void OnTutorialDisplayChange (TutorialDisplayChange e)
  {
		if(e.Tag == Tag.ToLower() || (e.Tag == "all" && HideOwnChildren))
    {
      if(e.Hide)
      {
        Hide();
      }
      else
      {
        Show();
      }
    }
    else if(e.Tag == "all" && HasGraphic)
    {
      gameObject.GetComponent<Graphic>().enabled = !e.Hide;
    }
	}

  public void Hide()
  {
    var gChildImages = gameObject.GetComponentsInChildren<Graphic>();
    var gParentImages = gameObject.GetComponentsInParent<Graphic>();
    foreach (var obj in gChildImages)
    {
      bool ignore = false;
      foreach (GameObject g in FindObjectOfType<HideUIDisplay>().IgnoredObjects)
      {
        if (g == obj.gameObject) ignore = true;
      }
      if (obj.gameObject.activeSelf && obj.enabled && !ignore)
      {
        obj.enabled = false;
        if (obj.GetComponent<Button>() != null) obj.GetComponent<Button>().enabled = false;
        ActiveGraphics.Add(obj);
      }
    }
    foreach (var obj in gParentImages)
    {
      bool ignore = false;
      foreach (GameObject g in FindObjectOfType<HideUIDisplay>().IgnoredObjects)
      {
        if (g == obj.gameObject) ignore = true;
      }
      if (obj.gameObject.activeSelf && obj.enabled && !ignore && obj.gameObject.GetComponent<TagToggler>() != null)
      {
        obj.enabled = false;
        if (obj.GetComponent<Button>() != null) obj.GetComponent<Button>().enabled = false;
        ActiveGraphics.Add(obj);
      }
    }
  }

  public void Show()
  {
    foreach (var obj in ActiveGraphics)
    {
      ActiveGraphics.RemoveAll(item => item == null);
      obj.enabled = true;
      if (obj.GetComponent<Button>() != null) obj.GetComponent<Button>().enabled = true;
    }
    foreach(var obj in GetComponentsInParent<TagToggler>())
    {
      if(obj.HasGraphic) obj.ShowSelf();
    }
  }

  public void ShowSelf()
  {
    gameObject.GetComponent<Graphic>().enabled = true;
  }
}
