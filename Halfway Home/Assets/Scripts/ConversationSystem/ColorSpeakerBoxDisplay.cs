using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorSpeakerBoxDisplay : MonoBehaviour
{

    public SpeakerDisplay Colors;
    public float FadeTime = 0.5f;

    float Alpha = 0;
    Image Visual;

	// Use this for initialization
	void Start ()
    {
        Visual = GetComponent<Image>();

        Alpha = Visual.color.a;

        Space.Connect<DescriptionEvent>(Events.Description, OnSpeak);
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}


    void OnSpeak(DescriptionEvent eventdata)
    {
        if(Visual.color != GetColor(eventdata.TrueSpeaker))
            gameObject.DispatchEvent(Events.Fade, new FadeEvent(GetColor(eventdata.TrueSpeaker), FadeTime));
        
        
    }

    public Color GetColor(string Speaker)
    {
        Color co = Colors.GetColor(Speaker);

        co.a = Alpha;

        return co;
    }

}
