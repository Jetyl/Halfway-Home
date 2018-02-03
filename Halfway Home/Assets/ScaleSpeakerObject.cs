using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleSpeakerObject : MonoBehaviour
{
    public CharacterList Speaker;

    public float ScaleRatio = 1.2f;

    public float TimeToScale = 0.5f;

    bool Scaled;

	// Use this for initialization
	void Start ()
    {

        Space.Connect<DescriptionEvent>(Events.Description, OnScale);
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnScale(DescriptionEvent eventdata)
    {
        if(Scaled)
        {
            if (eventdata.Speaker != Speaker.Character)
            {
                Scaled = false;
                
                iTween.ScaleTo(gameObject, Vector3.one, TimeToScale);
            }
        }
        else
        {
            if(eventdata.Speaker == Speaker.Character)
            {
                Scaled = true;

                Vector3 newscale = transform.lossyScale * ScaleRatio;
                iTween.ScaleTo(gameObject, newscale, TimeToScale);
            }
        }
    }

}
