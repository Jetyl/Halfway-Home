using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Stratus;

public class TagToggler : MonoBehaviour
{
    public class TutorialDisplayChange : Stratus.Event { public string Tag; public bool Hide; public TutorialDisplayChange(string tag, bool hide) { Tag = tag; Hide = hide; } }
    public string Tag;
    public bool HideOwnChildren;
    public string ProgressTag = "";
    public float ScaleInOutDuration = 0.5f;
    private List<Graphic> ActiveGraphics = new List<Graphic>();
    private bool HasGraphic;

    // Use this for initialization
    void Start()
    {
        Stratus.Scene.Connect<TutorialDisplayChange>(OnTutorialDisplayChange);
        HasGraphic = GetComponent<Graphic>() != null;

        Space.Connect<DefaultEvent>(Events.Load, OnLoad);
    }


    void OnLoad(DefaultEvent eventdata)
    {
        //print(ProgressTag + " is " + Game.current.Progress.GetBoolValue(ProgressTag));
        if (Game.current.Progress.GetBoolValue(ProgressTag))
            Hide();
        else
            Show();
    }


    void OnTutorialDisplayChange(TutorialDisplayChange e)
    {
        if (e.Tag == Tag.ToLower())
        {
            if (e.Hide)
            {
                Shrink();
            }
            else
            {
                Grow();
            }

            UpdateProgress(e.Hide);
        }
        else if(e.Tag == "all" && HideOwnChildren)
        { 
            if (e.Hide)
            {
                Hide(); // Hide all only used at the beginning, no transition desired
            }
            else
            {
                Grow();
            }
        }
        else if (e.Tag == "all" && HasGraphic)
        {
            gameObject.GetComponent<Graphic>().enabled = !e.Hide;
            //UpdateProgress(e.Hide);
        }
        //else if(e.Tag == "all")
            //UpdateProgress(e.Hide);
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
        foreach (var obj in GetComponentsInParent<TagToggler>())
        {
            if (obj.HasGraphic) obj.ShowSelf();
        }
        
    }
    
    public void Grow()
    {
      gameObject.GetComponent<RectTransform>().localScale = Vector3.zero;
      var growSeq = Actions.Sequence(this);
      Actions.Call(growSeq, Show);
      Actions.Property(growSeq, () => gameObject.GetComponent<RectTransform>().localScale, new Vector3(1.2f, 1.2f, 1.0f), ScaleInOutDuration*4/5, Ease.QuadOut);
      Actions.Property(growSeq, () => gameObject.GetComponent<RectTransform>().localScale, Vector3.one, ScaleInOutDuration*1/5, Ease.QuadIn);
    }

    public void Shrink()
    {
      gameObject.GetComponent<RectTransform>().localScale = Vector3.one;
      var growSeq = Actions.Sequence(this);
      Actions.Property(growSeq, () => gameObject.GetComponent<RectTransform>().localScale, new Vector3(1.2f, 1.2f, 1.0f), ScaleInOutDuration/5, Ease.QuadOut);
      Actions.Property(growSeq, () => gameObject.GetComponent<RectTransform>().localScale, Vector3.zero, ScaleInOutDuration*4/5, Ease.QuadIn);
      Actions.Call(growSeq, Hide);
    }

    public void ShowSelf()
    {
        gameObject.GetComponent<Graphic>().enabled = true;
    }

    public void UpdateProgress(bool Value)
    {
        if (ProgressTag != null || ProgressTag != "")
        {
            //print(ProgressTag + " is now " + Value);
            Game.current.Progress.SetValue(ProgressTag, Value);
        }
    }
}
