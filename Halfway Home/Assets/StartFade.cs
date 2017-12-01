using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartFade : MonoBehaviour
{

    public Color StartColor = Color.black;

    public Color EndColor;

    public float FadeTime = 1;

	// Use this for initialization
	void Start ()
    {
        gameObject.GetComponent<Image>().color = StartColor;

        gameObject.DispatchEvent(Events.Fade, new FadeEvent(EndColor, FadeTime));
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
