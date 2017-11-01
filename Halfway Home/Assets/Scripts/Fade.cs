/*******************************************************************************
filename    Fade.cs
author      Jesse Lozano
project     a-0

Brief Description:


*******************************************************************************/


using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class Fade : MonoBehaviour
{

    public bool FadeOnInitialize;

    public Color InitilazeFadeColor;

    public float InitializeFadeTime = 1f;

    private SpriteRenderer sprite;

    private Text txt;

    private Image imgurd;

    private TextMeshPro pro;
    private TextMeshProUGUI progui;

    private Color FadeColor;

    private float FadeTime;

    [HideInInspector]
    public bool IsFading;
    private Coroutine Fading;


	// Use this for initialization
	void Start ()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
        txt = gameObject.GetComponent<Text>();
        imgurd = gameObject.GetComponent<Image>();
        pro = gameObject.GetComponent<TextMeshPro>();
        progui = gameObject.GetComponent<TextMeshProUGUI>();

        EventSystem.ConnectEvent<FadeEvent>(this.gameObject, Events.Fade, OnFadeEvent);

        if(FadeOnInitialize)
        {
            this.gameObject.DispatchEvent(Events.Fade, new FadeEvent(InitilazeFadeColor, InitializeFadeTime));
        }

	}
	
	// Update is called once per frame
	void Update ()
    {

    }


    void OnFadeEvent (FadeEvent eventdata)
    {
        print("on");

        if(Fading != null)
        {
            StopCoroutine(Fading);
        }
        

        FadeColor = eventdata.newColor;
        FadeTime = eventdata.time;

        if (!gameObject.activeInHierarchy)
            return;
        IsFading = true;

        if (sprite)
            Fading = StartCoroutine(FadeToSprite(FadeColor, FadeTime));
        else if (txt)
            Fading = StartCoroutine(FadeToText(FadeColor, FadeTime));
        else if (imgurd)
            Fading = StartCoroutine(FadeToImage(FadeColor, FadeTime));
        else if (pro)
            Fading = StartCoroutine(FadeToTextMeshPro(FadeColor, FadeTime));
        else if (progui)
            Fading = StartCoroutine(FadeToTextMeshProUI(FadeColor, FadeTime));

    }


    IEnumerator FadeToSprite(Color Value, float aTime)
    {
        float alpha = sprite.color.a;
        float red = sprite.color.r;
        float green = sprite.color.g;
        float blue = sprite.color.b;

        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(Mathf.Lerp(red, Value.r, t), Mathf.Lerp(green, Value.g, t), Mathf.Lerp(blue, Value.b, t), Mathf.Lerp(alpha, Value.a, t));
            sprite.color = newColor;
            yield return null;
        }
        IsFading = false;
        sprite.color = Value;

    }

    IEnumerator FadeToText(Color Value, float aTime)
    {
        float alpha = txt.color.a;
        float red = txt.color.r;
        float green = txt.color.g;
        float blue = txt.color.b;

        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(Mathf.Lerp(red, Value.r, t), Mathf.Lerp(green, Value.g, t), Mathf.Lerp(blue, Value.b, t), Mathf.Lerp(alpha, Value.a, t));
            txt.color = newColor;
            yield return null;
        }
        IsFading = false;
        txt.color = Value;
    }

    IEnumerator FadeToImage(Color Value, float aTime)
    {
        float alpha = imgurd.color.a;
        float red = imgurd.color.r;
        float green = imgurd.color.g;
        float blue = imgurd.color.b;

        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(Mathf.Lerp(red, Value.r, t), Mathf.Lerp(green, Value.g, t), Mathf.Lerp(blue, Value.b, t), Mathf.Lerp(alpha, Value.a, t));
            imgurd.color = newColor;
            yield return null;
        }
        IsFading = false;
        imgurd.color = Value;
    }

    IEnumerator FadeToTextMeshPro(Color Value, float aTime)
    {
        float alpha = pro.color.a;
        float red = pro.color.r;
        float green = pro.color.g;
        float blue = pro.color.b;

        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(Mathf.Lerp(red, Value.r, t), Mathf.Lerp(green, Value.g, t), Mathf.Lerp(blue, Value.b, t), Mathf.Lerp(alpha, Value.a, t));
            pro.color = newColor;
            yield return null;
        }
        IsFading = false;
        pro.color = Value;
    }

    IEnumerator FadeToTextMeshProUI(Color Value, float aTime)
    {
        float alpha = progui.color.a;
        float red = progui.color.r;
        float green = progui.color.g;
        float blue = progui.color.b;

        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(Mathf.Lerp(red, Value.r, t), Mathf.Lerp(green, Value.g, t), Mathf.Lerp(blue, Value.b, t), Mathf.Lerp(alpha, Value.a, t));
            progui.color = newColor;
            yield return null;
        }

        progui.color = Value;
    }
}

public class FadeEvent : DefaultEvent
{
    public Color newColor;
    public float time;

    public FadeEvent(Color FadeColor, float FadeTime = 1f)
    {
        newColor = FadeColor;
        time = FadeTime;
    }

}